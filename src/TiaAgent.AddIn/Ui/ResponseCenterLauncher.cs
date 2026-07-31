using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.AddIn.Diagnostics;
using TiaAgent.Contracts.Bridge;

namespace TiaAgent.AddIn.Ui;

internal sealed class ResponseCenterLaunchResult
{
    public ResponseCenterLaunchResult(bool success, string? status, string? errorMessage, bool activatedExistingInstance)
    {
        Success = success;
        Status = status;
        ErrorMessage = errorMessage;
        ActivatedExistingInstance = activatedExistingInstance;
    }

    public bool Success { get; }
    public string? Status { get; }
    public string? ErrorMessage { get; }
    public bool ActivatedExistingInstance { get; }
}

/// <summary>
/// Requests the Bridge to start or activate the Response Center.
/// The Add-In does not launch processes directly — that responsibility
/// belongs to the Bridge, which runs outside the TIA Portal sandbox.
/// </summary>
internal static class ResponseCenterLauncher
{
    internal const string ExecutableName = "TiaAgent.ResponseCenter.exe";

    private static readonly Regex s_activeVersionRegex = new(
        "\\\"activeVersion\\\"\\s*:\\s*\\\"(?<version>[^\\\"]+)\\\"",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static async Task<ResponseCenterLaunchResult> RequestResponseCenterLaunchAsync(
        string taskId,
        string action,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(taskId))
            throw new ArgumentNullException(nameof(taskId));
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentNullException(nameof(action));

        try
        {
            var request = new LaunchResponseCenterRequest
            {
                TaskId = taskId,
                TiaInstanceId = GetCurrentTiaInstanceId(),
                Action = action
            };

            AddInLogger.Info(
                $"Requesting Bridge to launch Response Center: taskId={taskId}, action={action}");

            var response = await AddInServices.BridgeClient
                .LaunchResponseCenterAsync(request, cancellationToken)
                .ConfigureAwait(false);

            var success = response.Status is ResponseCenterLaunchStatus.StartedAndVisible
                or ResponseCenterLaunchStatus.ActivatedAndVisible
                or ResponseCenterLaunchStatus.StaleInstanceRestarted;

            if (success)
            {
                AddInLogger.Info(
                    $"Response Center launch succeeded: status={response.Status}, pid={response.ProcessId}, " +
                    $"windowHandle={response.WindowHandle}, activatedExisting={response.ActivatedExistingInstance}");
            }
            else
            {
                AddInLogger.Warn(
                    $"Response Center launch failed: status={response.Status}, error={response.ErrorMessage}");
            }

            return new ResponseCenterLaunchResult(
                success,
                response.Status,
                response.ErrorMessage,
                response.ActivatedExistingInstance);
        }
        catch (Exception ex)
        {
            AddInLogger.Error("Failed to request Response Center launch from Bridge.", ex);
            return new ResponseCenterLaunchResult(
                false,
                ResponseCenterLaunchStatus.StartupFailure,
                ex.Message,
                false);
        }
    }

    private static string GetCurrentTiaInstanceId()
    {
        return AddInServices.TiaInstanceId;
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
}
