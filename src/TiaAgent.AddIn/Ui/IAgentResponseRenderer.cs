#if SIEMENS
using System.Windows.Documents;

namespace TiaAgent.AddIn.Ui;

/// <summary>
/// Renders agent response content into a WPF FlowDocument for display
/// in the Add-In's response window.
/// </summary>
public interface IAgentResponseRenderer
{
    /// <summary>
    /// Renders the given content string into a FlowDocument.
    /// Returns null if the content is empty.
    /// Throws an exception if rendering fails.
    /// </summary>
    FlowDocument? Render(string content);
}
#endif
