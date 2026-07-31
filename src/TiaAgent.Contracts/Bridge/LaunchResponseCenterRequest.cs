namespace TiaAgent.Contracts.Bridge;

public sealed class LaunchResponseCenterRequest
{
    public string TaskId { get; set; } = null!;
    public string TiaInstanceId { get; set; } = null!;
    public string Action { get; set; } = null!;
    public int BridgePort { get; set; }
}
