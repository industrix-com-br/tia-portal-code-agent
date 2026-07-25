#if SIEMENS
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Siemens.Engineering;
using Siemens.Engineering.AddIn;
using Siemens.Engineering.AddIn.Menu;
using TiaAgent.AddIn.Bridge;
using TiaAgent.AddIn.Diagnostics;
using TiaAgent.AddIn.Ui;
using TiaAgent.Contracts.Bridge;
using TiaAgent.Contracts.Diagnostics;

namespace TiaAgent.AddIn.Providers;

public sealed class ProjectTreeProvider : ProjectTreeAddInProvider
{
    private readonly TiaPortal _tiaPortal;

    public ProjectTreeProvider(TiaPortal tiaPortal)
    {
        _tiaPortal = tiaPortal;

        // Logger startup must never prevent Add-In loading.
        // AddInLogger.Startup() is itself best-effort, but this is an additional
        // safety boundary in case of TypeInitializationException or unexpected failures.
        try
        {
            AddInLogger.Startup();
        }
        catch
        {
            // Intentionally empty: logger failure must not break the Add-In.
        }

        try
        {
            AddInLogger.Info("ProjectTreeProvider initialized.");
        }
        catch
        {
            // Same principle: logging is best-effort.
        }
    }

    protected override System.Collections.Generic.IEnumerable<ContextMenuAddIn> GetContextMenuAddIns()
    {
        yield return new TiaAgentContextMenu(_tiaPortal);
    }
}

public sealed class TiaAgentContextMenu : ContextMenuAddIn
{
    private readonly TiaPortal _tiaPortal;

    public TiaAgentContextMenu(TiaPortal tiaPortal) : base("AI Code Agent")
    {
        _tiaPortal = tiaPortal;
    }

    protected override void BuildContextMenuItems(ContextMenuAddInRoot addInRoot)
    {
        addInRoot.Items.AddActionItem<IEngineeringObject>(
            "Explain selected object",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("explain", selection));

        addInRoot.Items.AddActionItem<IEngineeringObject>(
            "Review selected object",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("review", selection));

        addInRoot.Items.AddActionItem<IEngineeringObject>(
            "Propose change",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleAction("propose", selection));
    }

    private void HandleAction(string action, MenuSelectionProvider<IEngineeringObject> selection)
    {
        AddInLogger.Info($"Menu action triggered: {action}");
        AddInLogger.Info($"Current thread: {Environment.CurrentManagedThreadId}, " +
                         $"apartment: {Thread.CurrentThread.GetApartmentState()}");

        try
        {
            var objects = selection.GetSelection();
            var enumerator = objects.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                AddInLogger.Warn("No object selected.");
                AssistantPanelFactory.ShowWarning("No object selected.");
                return;
            }

            var selectedObj = enumerator.Current as IEngineeringObject;
            if (selectedObj == null)
            {
                AddInLogger.Warn("Selected object is not a TIA engineering object.");
                AssistantPanelFactory.ShowWarning("Selected object is not a TIA engineering object.");
                return;
            }

            // Capture selection using ToString() — no reflection (avoids VerificationException)
            var selectionInfo = selectedObj.ToString() ?? "Unknown";
            var typeName = selectedObj.GetType().Name;
            var correlationId = $"tia-{Guid.NewGuid():N}";

            AddInLogger.Info($"Selection captured: {selectionInfo} (type: {typeName}, correlation: {correlationId})");

            // Extract source code from the selected object
            var selectionSnapshot = SelectionSnapshotFactory.Create(selectedObj);
            if (selectionSnapshot?.Source != null)
            {
                AddInLogger.Info($"Source extracted: {selectionSnapshot.Source.Length} chars (format: {selectionSnapshot.SourceFormat})");
            }
            else
            {
                AddInLogger.Warn($"No source extracted for {selectionInfo}");
            }

            // Fire-and-forget: Bridge call on background thread, UI on completion
            Task.Run(() => ExecuteViaBridgeAsync(action, selectionInfo, typeName, correlationId, selectionSnapshot));
        }
        catch (Exception ex)
        {
            AddInLogger.Error($"Error handling menu action '{action}'", ex);
            AssistantPanelFactory.ShowError($"Error: {ex.Message}");
        }
    }

    private async Task ExecuteViaBridgeAsync(string action, string selectionInfo, string typeName, string correlationId, SelectionSnapshot? selectionSnapshot = null)
    {
        AddInLogger.Info($"Bridge execution started for '{action}' on thread " +
                         $"{Environment.CurrentManagedThreadId}");

        try
        {
            var agentId = action switch
            {
                "explain" => "tia-explain",
                "review" => "tia-review",
                "propose" => "tia-change",
                _ => "tia-explain"
            };

            var actionDescription = action switch
            {
                "explain" => "explain this object",
                "review" => "review this object for issues and improvements",
                "propose" => "propose improvements to this object",
                _ => "analyze this object"
            };

            // Use the pre-extracted selection snapshot if provided, otherwise create a basic one
            var selection = selectionSnapshot ?? new SelectionSnapshot
            {
                Name = selectionInfo,
                ObjectType = typeName,
                RuntimeType = "",
                PlcName = "",
                TiaPath = selectionInfo,
                Language = ""
            };

            var request = new BridgeTaskRequest
            {
                ContractVersion = "1.0",
                CorrelationId = correlationId,
                Action = action,
                AgentId = agentId,
                TiaInstance = new TiaInstanceSnapshot
                {
                    ProcessId = 0,
                    SessionId = $"addin-{correlationId}",
                    Version = "21.0"
                },
                Project = new ProjectSnapshot
                {
                    Id = "current",
                    Name = "Current Project",
                    Path = ""
                },
                Selection = selection,
                UserMessage = $"The user selected object \"{selectionInfo}\" of type \"{typeName}\" in TIA Portal. Please {actionDescription}."
            };

            // Log source code diagnostics
            if (selection.Source != null)
            {
                AddInLogger.Info($"Request includes source: {selection.Source.Length} chars, format: {selection.SourceFormat}");
                var previewLength = Math.Min(selection.Source.Length, 200);
                AddInLogger.Info($"Source preview: {selection.Source.Substring(0, previewLength)}...");
            }
            else
            {
                AddInLogger.Warn($"Request does NOT include source code!");
            }

            AddInLogger.Info($"Starting Bridge task: agentId={agentId}, action={action}");

            var accepted = await AddInServices.BridgeClient.StartTaskAsync(request, CancellationToken.None).ConfigureAwait(false);

            AddInLogger.Info($"Bridge task accepted: taskId={accepted.TaskId}");

            // Poll for completion
            var config = AddInServices.Config;
            var timeout = TimeSpan.FromSeconds(config.TaskTimeoutSeconds);
            var startTime = DateTime.UtcNow;

            while (true)
            {
                if (DateTime.UtcNow - startTime > timeout)
                {
                    AddInLogger.Warn($"Task timed out after {config.TaskTimeoutSeconds}s");
                    AssistantPanelFactory.ShowWarning("Task timed out waiting for response.");
                    return;
                }

                await Task.Delay(config.PollingIntervalMilliseconds).ConfigureAwait(false);

                var status = await AddInServices.BridgeClient.GetTaskAsync(accepted.TaskId, CancellationToken.None).ConfigureAwait(false);

                if (status.Status == BridgeTaskStatusValues.Completed)
                {
                    var response = status.Response ?? "No response received.";
                    AddInLogger.Info(TextPayloadDiagnostics.DescribeText(
                        "8-9.addin.response-and-renderer-input", response));
                    AddInLogger.Info($"Task completed. Response length: {response.Length} chars");
                    AssistantPanelFactory.ShowResult(action, response,
                        correlationId: correlationId,
                        runtimeId: status.RuntimeId,
                        targetObject: selectionInfo);
                    return;
                }

                if (status.Status == BridgeTaskStatusValues.Failed)
                {
                    var errorMsg = status.Error?.Message ?? status.Message ?? "Unknown error";
                    AddInLogger.Error($"Task failed: {errorMsg}");
                    AssistantPanelFactory.ShowError(errorMsg);
                    return;
                }

                if (status.Status == BridgeTaskStatusValues.Cancelled)
                {
                    AddInLogger.Info("Task was cancelled.");
                    AssistantPanelFactory.ShowWarning("Task was cancelled.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            // Log full diagnostic details: exception type, message, stack trace,
            // inner exceptions, assembly info, and correlation ID.
            var diagnostics = FormatExceptionDiagnostics(ex, action, correlationId);
            AddInLogger.Error(diagnostics, null);

            // Show a clear, user-friendly message while preserving the technical
            // cause in logs for future diagnosis.
            var userMessage = FormatUserErrorMessage(ex);
            AssistantPanelFactory.ShowError(userMessage);
        }
    }

    /// <summary>
    /// Formats comprehensive diagnostic information for an exception.
    /// Includes exception type, message, stack trace, inner exceptions,
    /// assembly info, and correlation ID for post-mortem analysis.
    /// </summary>
    private static string FormatExceptionDiagnostics(Exception ex, string action, string correlationId)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Bridge execution failed for '{action}' (correlation: {correlationId})");
        sb.AppendLine($"Exception type: {ex.GetType().FullName}");
        sb.AppendLine($"Message: {ex.Message}");
        sb.AppendLine($"Thread: {Environment.CurrentManagedThreadId}, apartment: {Thread.CurrentThread.GetApartmentState()}");
        sb.AppendLine($".NET Runtime: {System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion()}");
        sb.AppendLine($"CLR Version: {Environment.Version}");

        // Log all inner exceptions recursively
        var inner = ex.InnerException;
        var depth = 0;
        while (inner != null && depth < 5)
        {
            sb.AppendLine($"Inner exception [{depth}]: {inner.GetType().FullName}: {inner.Message}");
            inner = inner.InnerException;
            depth++;
        }

        // Log the full stack trace
        if (!string.IsNullOrEmpty(ex.StackTrace))
        {
            sb.AppendLine($"Stack trace:\n{ex.StackTrace}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Formats a user-friendly error message. Preserves the technical cause
    /// for known exception types while showing a clear message for unknowns.
    /// </summary>
    private static string FormatUserErrorMessage(Exception ex)
    {
        // For VerificationException (CAS/sandbox issues), provide actionable guidance
        if (ex is System.Security.VerificationException)
        {
            return "The Add-In encountered a security sandbox restriction. "
                 + "This usually means a dependency requires permissions not available "
                 + "in TIA Portal's partial-trust environment. Check the Add-In logs for details.";
        }

        // For SecurityException, show the permission type that failed
        if (ex is System.Security.SecurityException secEx)
        {
            return $"A security permission was denied: {secEx.Message}. "
                 + "The Add-In may need additional permissions in Config.xml.";
        }

        // For HTTP/bridge errors, show the message directly
        return "Failed to communicate with AI assistant: " + ex.Message;
    }
}
#endif
