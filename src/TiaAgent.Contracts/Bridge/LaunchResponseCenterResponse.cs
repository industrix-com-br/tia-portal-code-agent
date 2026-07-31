namespace TiaAgent.Contracts.Bridge;

public static class ResponseCenterLaunchStatus
{
    // Legacy statuses — kept for backward compatibility but no longer emitted.
    public const string Started = "started";
    public const string ActivatedExisting = "activated_existing";

    // Visibility-confirmed statuses (new).
    public const string StartedAndVisible = "started_and_visible";
    public const string ActivatedAndVisible = "activated_and_visible";
    public const string StaleInstanceRestarted = "stale_instance_restarted";

    // Failure statuses.
    public const string ExecutableNotFound = "executable_not_found";
    public const string InvalidRequest = "invalid_request";
    public const string TaskNotFound = "task_not_found";
    public const string StartupFailure = "startup_failure";
    public const string ActivationFailure = "activation_failure";
}

public sealed class LaunchResponseCenterResponse
{
    public string Status { get; set; } = null!;
    public int ProcessId { get; set; }
    public bool ActivatedExistingInstance { get; set; }
    public string? ErrorMessage { get; set; }

    /// <summary>Window handle reported by the Response Center after readiness confirmation.</summary>
    public long WindowHandle { get; set; }
}

/// <summary>
/// Readiness information sent by the Response Center to the Bridge
/// after the WPF window has been created, loaded, and shown.
/// </summary>
public sealed class ResponseCenterReadinessInfo
{
    public int ProcessId { get; set; }
    public long WindowHandle { get; set; }
    public string TaskId { get; set; } = null!;
    public string? TiaInstanceId { get; set; }
    public bool IsVisible { get; set; }
    public string WindowState { get; set; } = null!;
    public double WindowLeft { get; set; }
    public double WindowTop { get; set; }
    public double ScreenWidth { get; set; }
    public double ScreenHeight { get; set; }
}
