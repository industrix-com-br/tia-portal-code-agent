using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

internal sealed class ResponseCenterLaunchRequest
{
    public ResponseCenterLaunchRequest(
        string taskId,
        string action,
        string objectName,
        string objectType,
        string? plcName,
        string? projectName,
        string correlationId,
        string? initialStatus,
        string bridgeUrl)
    {
        TaskId = taskId ?? throw new ArgumentNullException(nameof(taskId));
        Action = action ?? throw new ArgumentNullException(nameof(action));
        ObjectName = objectName ?? throw new ArgumentNullException(nameof(objectName));
        ObjectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
        PlcName = plcName;
        ProjectName = projectName;
        CorrelationId = correlationId ?? throw new ArgumentNullException(nameof(correlationId));
        InitialStatus = initialStatus;
        BridgeUrl = bridgeUrl ?? throw new ArgumentNullException(nameof(bridgeUrl));
    }

    public string TaskId { get; }
    public string Action { get; }
    public string ObjectName { get; }
    public string ObjectType { get; }
    public string? PlcName { get; }
    public string? ProjectName { get; }
    public string CorrelationId { get; }
    public string? InitialStatus { get; }
    public string BridgeUrl { get; }
}

internal sealed class ResponseCenterLaunchResult
{
    public ResponseCenterLaunchResult(bool success, string? executablePath, string? errorMessage)
    {
        Success = success;
        ExecutablePath = executablePath;
        ErrorMessage = errorMessage;
    }

    public bool Success { get; }
    public string? ExecutablePath { get; }
    public string? ErrorMessage { get; }
}

/// <summary>
/// Locates and starts the out-of-process Response Center installed with the active product version.
/// The Bridge bearer token is intentionally not passed on the command line; the Response Center
/// discovers it from the local runtime files.
/// </summary>
internal static class ResponseCenterLauncher
{
    internal const string ExecutableName = "TiaAgent.ResponseCenter.exe";

    private static readonly Regex s_activeVersionRegex = new(
        "\\\"activeVersion\\\"\\s*:\\s*\\\"(?<version>[^\\\"]+)\\\"",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static ResponseCenterLaunchResult Launch(
        ResponseCenterLaunchRequest request,
        string? installationRoot = null,
        Func<ProcessStartInfo, Process?>? processStarter = null)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var executablePath = ResolveExecutablePath(installationRoot);
            if (!File.Exists(executablePath))
            {
                return new ResponseCenterLaunchResult(
                    false,
                    executablePath,
                    $"Response Center executable was not found at '{executablePath}'. Reinstall or update TIA Agent.");
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = BuildArguments(request),
                WorkingDirectory = Path.GetDirectoryName(executablePath) ?? string.Empty,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            var process = (processStarter ?? Process.Start)(startInfo);
            if (process == null)
            {
                return new ResponseCenterLaunchResult(
                    false,
                    executablePath,
                    "Windows did not start the Response Center process.");
            }

            AddInLogger.Info(
                $"Response Center started. pid={process.Id}, taskId={request.TaskId}, path={executablePath}");

            return new ResponseCenterLaunchResult(true, executablePath, null);
        }
        catch (Exception ex)
        {
            AddInLogger.Error("Failed to start the Response Center.", ex);
            return new ResponseCenterLaunchResult(false, null, ex.Message);
        }
    }

    internal static string ResolveExecutablePath(string? installationRoot = null)
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
        {
            throw new FileNotFoundException(
                "TIA Agent active-version manifest was not found. Run 'tia-agent install' or 'tia-agent update'.",
                currentManifestPath);
        }

        var currentManifest = File.ReadAllText(currentManifestPath);
        var activeVersion = ParseActiveVersion(currentManifest);
        if (string.IsNullOrWhiteSpace(activeVersion))
        {
            throw new InvalidDataException($"Active version is missing from '{currentManifestPath}'.");
        }

        return Path.Combine(
            root,
            "versions",
            activeVersion,
            "ResponseCenter",
            ExecutableName);
    }

    internal static string? ParseActiveVersion(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        var match = s_activeVersionRegex.Match(json);
        return match.Success ? match.Groups["version"].Value : null;
    }

    internal static string BuildArguments(ResponseCenterLaunchRequest request)
    {
        var arguments = new StringBuilder();
        AppendArgument(arguments, "--task-id", request.TaskId);
        AppendArgument(arguments, "--bridge-url", request.BridgeUrl);
        AppendArgument(arguments, "--action", request.Action);
        AppendArgument(arguments, "--object-name", request.ObjectName);
        AppendArgument(arguments, "--object-type", request.ObjectType);
        AppendArgument(arguments, "--plc-name", request.PlcName);
        AppendArgument(arguments, "--project-name", request.ProjectName);
        AppendArgument(arguments, "--correlation-id", request.CorrelationId);
        AppendArgument(arguments, "--initial-status", request.InitialStatus);
        return arguments.ToString();
    }

    private static void AppendArgument(StringBuilder arguments, string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (arguments.Length > 0)
        {
            arguments.Append(' ');
        }

        arguments.Append(name);
        arguments.Append(' ');
        arguments.Append(QuoteArgument(value));
    }

    /// <summary>
    /// Applies the Windows command-line quoting rules used by CommandLineToArgvW.
    /// </summary>
    internal static string QuoteArgument(string value)
    {
        if (value.Length == 0)
        {
            return "\"\"";
        }

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
        {
            result.Append('\\', backslashCount * 2);
        }

        result.Append('"');
        return result.ToString();
    }
}
