using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            // Process already exited or inaccessible
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
    private readonly IProcessOperations _processOps;
    private readonly ReadinessListener _readinessListener;

    public ResponseCenterProcessManager(BridgeLogger logger)
        : this(logger, new DefaultProcessOperations(), new ReadinessListener(logger))
    {
    }

    public ResponseCenterProcessManager(
        BridgeLogger logger,
        IProcessOperations processOps,
        ReadinessListener readinessListener)
    {
        _logger = logger;
        _processOps = processOps;
        _readinessListener = readinessListener;
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

        // Resolve executable
        string? executablePath;
        try
        {
            executablePath = ResolveExecutablePath();
        }
        catch (Exception ex)
        {
            _logger.Error("Failed to resolve Response Center executable path", ex);
            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.ExecutableNotFound,
                ErrorMessage = $"Could not resolve Response Center executable: {ex.Message}"
            };
        }

        if (string.IsNullOrWhiteSpace(executablePath) || !File.Exists(executablePath))
        {
            _logger.Warn($"Response Center executable not found at '{executablePath}'");
            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.ExecutableNotFound,
                ErrorMessage = $"Response Center executable was not found at '{executablePath}'. Reinstall or update TIA Agent."
            };
        }

        _logger.Info(
            $"Response Center launch request: taskId={request.TaskId}, tiaInstance={request.TiaInstanceId}, " +
            $"action={request.Action}, exe={executablePath}");

        // Check for existing instance
        if (_instances.TryGetValue(request.TiaInstanceId, out var existing))
        {
            return HandleExistingInstance(request, existing, executablePath);
        }

        // Start new process and wait for readiness
        return StartNewProcess(request, executablePath);
    }

    private ResponseCenterLaunchResult HandleExistingInstance(
        LaunchResponseCenterRequest request, ManagedInstance existing, string executablePath)
    {
        if (!_processOps.IsAlive(existing.ProcessId))
        {
            _logger.Info($"Existing Response Center process {existing.ProcessId} is no longer alive; removing stale entry");
            _instances.TryRemove(request.TiaInstanceId, out _);
            return StartNewProcess(request, executablePath);
        }

        _logger.Info(
            $"Existing Response Center found for TIA instance {request.TiaInstanceId}: pid={existing.ProcessId}");

        // Send task notification to the existing instance
        var notified = NotifyExistingInstance(request.TiaInstanceId, request.TaskId);
        if (!notified)
        {
            _logger.Warn("Failed to notify existing instance via pipe; treating as stale and starting new process");
            KillAndRemoveInstance(request.TiaInstanceId, existing.ProcessId);
            return StartNewProcess(request, executablePath);
        }

        _logger.Info($"Notified existing Response Center of new task: taskId={request.TaskId}");

        // Wait for readiness confirmation from the existing instance
        var readiness = WaitForReadiness(request.TiaInstanceId, DefaultReadinessTimeout);
        if (readiness != null)
        {
            _logger.Info(
                $"Existing Response Center confirmed activation: pid={readiness.ProcessId}, " +
                $"hwnd={readiness.WindowHandle}, isVisible={readiness.IsVisible}");

            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.ActivatedAndVisible,
                ProcessId = readiness.ProcessId,
                ActivatedExistingInstance = true,
                WindowHandle = readiness.WindowHandle
            };
        }

        // Existing instance did not confirm readiness — treat as stale
        _logger.Warn(
            $"Existing Response Center pid={existing.ProcessId} did not confirm readiness; " +
            "treating as stale and starting new process");
        KillAndRemoveInstance(request.TiaInstanceId, existing.ProcessId);
        return StartNewProcess(request, executablePath);
    }

    private ResponseCenterLaunchResult StartNewProcess(
        LaunchResponseCenterRequest request, string executablePath)
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

            _instances[request.TiaInstanceId] = new ManagedInstance
            {
                ProcessId = process.Id,
                TiaInstanceId = request.TiaInstanceId,
                StartedAt = DateTime.UtcNow
            };

            _logger.Info(
                $"Response Center process started: pid={process.Id}, taskId={request.TaskId}, " +
                $"tiaInstance={request.TiaInstanceId}");

            // Wait for readiness — the RC sends this after window.Show()
            var readiness = WaitForReadiness(request.TiaInstanceId, DefaultReadinessTimeout);
            if (readiness != null)
            {
                _logger.Info(
                    $"Response Center confirmed visible: pid={readiness.ProcessId}, " +
                    $"hwnd={readiness.WindowHandle}, isVisible={readiness.IsVisible}");

                return new ResponseCenterLaunchResult
                {
                    Status = ResponseCenterLaunchStatus.StartedAndVisible,
                    ProcessId = readiness.ProcessId,
                    ActivatedExistingInstance = false,
                    WindowHandle = readiness.WindowHandle
                };
            }

            // Readiness timeout — the process started but never showed a window
            _logger.Warn(
                $"Response Center pid={process.Id} did not send readiness within " +
                $"{DefaultReadinessTimeout.TotalSeconds}s — treating as startup failure");

            KillAndRemoveInstance(request.TiaInstanceId, process.Id);

            return new ResponseCenterLaunchResult
            {
                Status = ResponseCenterLaunchStatus.StartupFailure,
                ProcessId = process.Id,
                ErrorMessage = "Response Center started but did not confirm window visibility within the timeout period."
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

    private bool NotifyExistingInstance(string tiaInstanceId, string taskId)
    {
        var pipeName = GetPipeName(tiaInstanceId);
        try
        {
            using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            pipeClient.Connect(2000);
            var buffer = Encoding.UTF8.GetBytes(taskId + "\n");
            pipeClient.Write(buffer, 0, buffer.Length);
            pipeClient.Flush();
            return true;
        }
        catch (Exception ex)
        {
            _logger.Debug($"Named pipe notification failed for {tiaInstanceId}: {ex.Message}");
            return false;
        }
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
