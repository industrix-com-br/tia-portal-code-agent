namespace TiaAgent.Contracts.Bridge;

public sealed class SelectionSnapshot
{
    public string Name { get; set; } = null!;
    public string ObjectType { get; set; } = null!;
    public string RuntimeType { get; set; } = null!;
    public string PlcName { get; set; } = null!;
    public string TiaPath { get; set; } = null!;
    public string Language { get; set; } = null!;

    /// <summary>
    /// PLC source code or XML exported from TIA Portal.
    /// Required for explain/review/propose actions.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Format of the source content (e.g., "xml", "scl", "lad", "fbd").
    /// </summary>
    public string? SourceFormat { get; set; }
}
