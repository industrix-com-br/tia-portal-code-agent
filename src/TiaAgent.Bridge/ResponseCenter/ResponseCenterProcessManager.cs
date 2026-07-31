using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Contracts.Bridge;

namespace TiaAgent.Bridge.ResponseCenter;

/// <summary>
/// Abstraction over process operations for testability.
/// </summary>
public interface IProcessOperations
{
    Process? Start(ProcessStartInfo startInfo);
    bool IsAlive(int processId);
    void Kill(int processId);
}

/// <summary>
/// Default process operations using System.Diagnostics.
/// </summary>
public sealed class DefaultProcessOperations : IProcessOperations
{
    public Process? Start(ProcessStartInfo startInfo) => Process.Start(startInfo);

    public bool IsAlive(int processId)
    {
        try
        {
            var process = Process.GetProcessById(processId);
            return !process.HasExited;
        }
        catch
        {
            return false;
        }
    }

    public void Kill(int processId)
    {
        try
        {
            var process = Process.GetProcessById(processId);
            if (!process.HasExited)
                process.Kill();
        }
        catch
        {
            // Process already exited or inaccessible.
        }
    }
}

/// <summary>
/// Sends activation requests to an already running Response Center instance.
/// </summary>
public interface IResponseCenterActivationClient
{
    bool Notify(LaunchResponseCenterRequest request);
}

/// <summary>
/// Named-pipe implementation used by the Bridge in production.
/// </summary>
public sealed class NamedPipeResponseCenterActivationClient : IResponseCenterActivationClient
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly BridgeLogger _logger;

    public NamedPipeResponseCenterActivationClient(BridgeLogger logger)
    {
        _logger = logger;
    }

    public bool Notify(LaunchResponseCenterRequest request)
    {
        var pipeName = ResponseCenterProcessManager.GetPipeName(request.TiaInstanceId);
        try
        {
            using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            pipeClient.Connect(2000);

            var payload = JsonSerializer.Serialize(request, s_jsonOptions) + "\n";
            var buffer = Encoding.UTF8.GetBytes(payload);
            pipeClient.Write(buffer, 0, buffer.Length);
            pipeClient.Flush();
            return true;
        }
        catch (Exception ex)
        {
            _logger.Debug(
                $"Named pipe activation failed for {request.TiaInstanceId}: {ex.Message}");
            return false;
        }
    }
}

public sealed class ResponseCenterProcessManager : IDisposable
{
    private const string ExecutableName = "TiaAgent.ResponseCenter.exe";
    internal static readonly TimeSpan DefaultReadinessTimeout = TimeSpan.FromSeconds(15);

    private static readonly Regex s_activeVersionRegex = new(
        "\\\"activeVersion\\\"\\s*:\\s*\\\"(?<version>[^\\\"]+)\\\"",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private readonly BridgeLogger _logger;
    private readonly ConcurrentDictionary<string, ManagedInstance> _instances = new();
    private readonly ConcurrentDictionary<string, object> _instanceLocks = new();
    private readonly IProcessOperations _processOps;
    private readonly ReadinessListener _readinessListener;
    private readonly IResponseCenterActivationClient _activationClient;
    private readonly string? _installationRoot;

    public ResponseCenterProcessManager(BridgeLogger logger)
        : this(
            logger,
            new DefaultProcessOperations(),
            new ReadinessListener(logger),
            new NamedPipeResponseCenterActivationClient(logger),
            null)
    {
    }

    public ResponseCenterProcessManager(
        BridgeLogger logger,
        IProcessOperations processOps,
        ReadinessListener readinessListener,
        IResponseCenterActivationClient? activationClient = null,
        string? installationRoot = null)
    {
        _logger = logger;
        _processOps = processOps;
        _readinessListener = readinessListener;
        _activationClient = activationClient ?? new NamedPipeResponseCenterActivationClient(logger);
        _installationRoot = installationRoot;
    }

    public ResponseCenterLaunchResult LaunchOrActivate(LaunchResponseCenterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.TaskId))
            return InvalidRequest("TaskId is required.");

        if (string.IsNullOrWhiteSpace(request.TiaInstanceId))
            return InvalidRequest("TiaInstanceId is required.");

        if (string.IsNullOrWhiteSpace(request.Action))
            return InvalidRequest("Action is required.");

        var instanceLock = _instanceLocks.GetOrAdd(request.TiaInstanceId, _ => new object());
        lock (instanceLock)
        {
            return LaunchOrActivateCore(request);
        }
    }

    private ResponseCenterLaunchResult LaunchOrActivateCore(LaunchResponseCenterRequest request)
    {
        _logger.Info(
            $"Response Center launch request: taskId={request.TaskId}, tiaInstance={request.TiaInstanceId}, " +
            $"action={request.Action}");

        var restartingStaleInstance = false;

        if (_instances.TryGetValue(request.TiaInstanceId, out var existing))
        {
            if (_processOps.IsAlive(existing.ProcessId))
            {
                var activation = TryActivateExistingInstance(request, existing.ProcessId);
                if (activation != null)
                    return activation;

                _logger.Warn(
                    $"Tracked Response Center pid={existing.ProcessId} did not activate correctly; restarting it");
                KillAndRemoveInstance(request.TiaInstanceId, existing.ProcessId);
                restartingStaleInstance = true;
            }
            else
            {
                _logger.Info(
                    $"Tracked Response Center process {existing.ProcessId} is no longer alive; removing stale entry");
                _instances.TryRemove(request.TiaInstanceId, out _);
            }
        }

        // The Bridge may have restarted while the Response Center stayed alive. Probe the
        // stable TIA-instance pipe before starting another process so the in-memory registry
        // can be reconstructed without creating a duplicate window.
        if (!restartingStaleInstance)
        {
            var recovered = TryActivateExistingInstance(request, expectedProcessId: null);
            if (recovered != null)
                return recovered;
        }

        var executableResult = ResolveExecutableForLaunch();
        if (executableResult.Error != null)
            return executableResult.Error;

        return StartNewProcess(
            request,
            executableResult.Path!,
            restartingStaleInstance);
    }

    private ResponseCenterLaunchResult? TryActivateExistingInstance(
        LaunchResponseCenterRequest request,
        int? expectedProcessId)
    {
        if (!_activationClient.Notify(request))
            return null;

        _logger.Info(
            $"Activation request sent to existing Response Center: taskId={request.TaskId}, " +
            $"tiaInstance={request.TiaInstanceId}");

        var readiness = WaitForReadiness(request.TiaInstanceId, DefaultReadinessTimeout);
        if (!IsValidReadiness(request, readiness, expectedProcessId))
        {
            _logger.Warn(
                $"Existing Response Center did not confirm the requested task: taskId={request.TaskId}, " +
                $"tiaInstance={request.TiaInstanceId}");
            return null;
        }

        _instances[request.TiaInstanceId] = new ManagedInstance
        {
            ProcessId = readiness!.ProcessId,
            TiaInstanceId = request.TiaInstanceId,
            StartedAt = DateTime.UtcNow
        };

        _logger.Info(
            $"Existing Response Center confirmed activation: pid={readiness.ProcessId}, " +
            $"hwnd={readiness.WindowHandle}, taskId={readiness.TaskId}");

        return new ResponseCenterLaunchResult
        {
            Status = ResponseCenterLaunchStatus.ActivatedAndVisible,
            ProcessId = readiness.ProcessId,
            ActivatedExistingInstance = true,
            WindowHandle = readiness.WindowHandle
        };
    }

    private ResponseCenterLaunchResult StartNewProcess(
        LaunchResponseCenterRequest request,
        string executablePath,
        bool restartingStaleInstance)
    {
        try
        {
            var arguments = BuildArguments(request);
            var startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(executablePath) ?? string.Empty,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = _processOps.Start(startInfo);
            if (process == null)
            {
                return new ResponseCenterLaunchResult
                {
                    Status = ResponseCenterLaunchStatus.StartupFailure,
                    ErrorMessage = "Windows did not start the Response Center process."
                };
            }

            _logger.Info(
                $"Response Center process started: pid={process.Id}, taskId={request.TaskId}, " +
                $"tiaInstance={request.TiaInstanceId}");

            var readiness = WaitForReadiness(request.TiaInstanceId, DefaultReadinessTimeout);
            if (IsValidReadiness(request, readiness, process.Id))
            {
                _instances[request.TiaInstanceId] = new ManagedInstance
                {
                    ProcessId = process.Id,
                    TiaInstanceId = request.TiaInstanceId,
                    StartedAt = DateTime.UtcNow
                };

                _logger.Info(
                    $"Response Center confirmed visible: pid={readiness!.ProcessId}, " +
                    $"hwnd={readiness.WindowHandle}, taskId={readiness.TaskId}");

                return new ResponseCenterLaunchResult
                {
                    Status = restartingStaleInstance
                        ? ResponseCenterLaunchStatus.StaleInstanceRestarted
                        : ResponseCenterLaunchStatus.StartedAndVisible,
                    ProcessId = readiness.ProcessId,
                    ActivatedExistingInstance = false,
                    WindowHandle = readiness.WindowHandle
                };
            }

            _logger.Warn(
                $"Response Center pid={process.Id} did not confirm the requested visible window within " +
                $"{DefaultReadinessTimeout.TotalSeconds}s; treating startup as failed");

            KillAndRemoveInstance(request.TiaInstanceId, process.Id);

            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.StartupFailure,
                ProcessId = process.Id,
                ErrorMessage = "Response Center started but did not confirm the requested task and window visibility within the timeout period."
            };
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to start Response Center process", ex);
            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.StartupFailure,
                ErrorMessage = $"Failed to start Response Center: {ex.Message}"
            };
        }
    }

    private (string? Path, ResponseCenterLaunchResult? Error) ResolveExecutableForLaunch()
    {
        string? executablePath;
        try
        {
            executablePath = ResolveExecutablePath(_installationRoot);
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to resolve Response Center executable path", ex);
            return (
                null,
                new ResponseCenterLaunchResult
                {
                    Status = ResponseCenterLaunchStatus.ExecutableNotFound,
                    ErrorMessage = $"Could not resolve Response Center executable: {ex.Message}"
                });
        }

        if (string.IsNullOrWhiteSpace(executablePath) || !File.Exists(executablePath))
        {
            _logger.Warn($"Response Center executable not found at '{executablePath}'");
            return (
                null,
                new ResponseCenterLaunchResult
                {
                    Status = ResponseCenterLaunchStatus.ExecutableNotFound,
                    ErrorMessage = $"Response Center executable was not found at '{executablePath}'. Reinstall or update TIA Agent."
                });
        }

        return (executablePath, null);
    }

    private ResponseCenterReadinessInfo? WaitForReadiness(string tiaInstanceId, TimeSpan timeout)
    {
        try
        {
            return _readinessListener.WaitForReadinessAsync(tiaInstanceId, timeout)
                .GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            _logger.Warn($"Error waiting for readiness: {ex.Message}");
            return null;
        }
    }

    private static bool IsValidReadiness(
        LaunchResponseCenterRequest request,
        ResponseCenterReadinessInfo? readiness,
        int? expectedProcessId)
    {
        if (readiness == null || !readiness.IsVisible || readiness.ProcessId <= 0)
            return false;

        if (expectedProcessId.HasValue && readiness.ProcessId != expectedProcessId.Value)
            return false;

        return string.Equals(readiness.TaskId, request.TaskId, StringComparison.Ordinal)
            && string.Equals(readiness.TiaInstanceId, request.TiaInstanceId, StringComparison.Ordinal);
    }

    private void KillAndRemoveInstance(string tiaInstanceId, int processId)
    {
        _logger.Info($"Killing stale Response Center process: pid={processId}");
        _processOps.Kill(processId);
        _instances.TryRemove(tiaInstanceId, out _);
    }

    private static ResponseCenterLaunchResult InvalidRequest(string message)
    {
        return new ResponseCenterLaunchResult
        {
            Status = ResponseCenterLaunchStatus.InvalidRequest,
            ErrorMessage = message
        };
    }

    internal static string? ResolveExecutablePath(string? installationRoot = null)
    {
        var root = installationRoot;
        if (string.IsNullOrWhiteSpace(root))
        {
            root = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TiaAgent");
        }

        var currentManifestPath = Path.Combine(root, "current.json");
        if (!File.Exists(currentManifestPath))
            return null;

        var currentManifest = File.ReadAllText(currentManifestPath);
        var activeVersion = ParseActiveVersion(currentManifest);
        if (string.IsNullOrWhiteSpace(activeVersion))
            return null;

        return Path.Combine(root, "versions", activeVersion, "ResponseCenter", ExecutableName);
    }

    internal static string? ParseActiveVersion(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        var match = s_activeVersionRegex.Match(json);
        return match.Success ? match.Groups["version"].Value : null;
    }

    internal static string BuildArguments(LaunchResponseCenterRequest request)
    {
        var sb = new StringBuilder();
        AppendArgument(sb, "--task-id", request.TaskId);
        AppendArgument(sb, "--action", request.Action);
        AppendArgument(sb, "--tia-instance-id", request.TiaInstanceId);
        return sb.ToString();
    }

    private static void AppendArgument(StringBuilder sb, string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (sb.Length > 0)
            sb.Append(' ');

        sb.Append(name);
        sb.Append(' ');
        sb.Append(QuoteArgument(value!));
    }

    internal static string QuoteArgument(string value)
    {
        if (value.Length == 0)
            return "\"\"";

        var result = new StringBuilder(value.Length + 2);
        result.Append('"');
        var backslashCount = 0;

        foreach (var character in value)
        {
            if (character == '\\')
            {
                backslashCount++;
                continue;
            }

            if (character == '"')
            {
                result.Append('\\', backslashCount * 2 + 1);
                result.Append('"');
                backslashCount = 0;
                continue;
            }

            if (backslashCount > 0)
            {
                result.Append('\\', backslashCount);
                backslashCount = 0;
            }

            result.Append(character);
        }

        if (backslashCount > 0)
            result.Append('\\', backslashCount * 2);

        result.Append('"');
        return result.ToString();
    }

    internal static string GetPipeName(string tiaInstanceId)
    {
        var sanitized = SanitizeForPipeName(tiaInstanceId);
        return $"TiaAgent_RC_{sanitized}";
    }

    private static string SanitizeForPipeName(string id)
    {
        var sb = new StringBuilder(id.Length);
        foreach (var c in id)
        {
            if (char.IsLetterOrDigit(c) || c == '_' || c == '-')
                sb.Append(c);
        }
        return sb.ToString();
    }

    public void Dispose()
    {
        _instances.Clear();
        _instanceLocks.Clear();
        _readinessListener.Dispose();
    }

    private sealed class ManagedInstance
    {
        public int ProcessId { get; set; }
        public string TiaInstanceId { get; set; } = null!;
        public DateTime StartedAt { get; set; }
    }
}

public sealed class ResponseCenterLaunchResult
{
    public string Status { get; set; } = null!;
    public int ProcessId { get; set; }
    public bool ActivatedExistingInstance { get; set; }
    public string? ErrorMessage { get; set; }
    public long WindowHandle { get; set; }
}
