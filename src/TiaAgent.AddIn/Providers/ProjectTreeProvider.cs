#if SIEMENS
using System;
using System.Threading;
using System.Threading.Tasks;
using Siemens.Engineering;
using Siemens.Engineering.AddIn;
using Siemens.Engineering.AddIn.Menu;
using TiaAgent.AddIn.Diagnostics;
using TiaAgent.AddIn.Ui;
using TiaAgent.Contracts.Bridge;

namespace TiaAgent.AddIn.Providers;

public sealed class ProjectTreeProvider : ProjectTreeAddInProvider
{
    private readonly TiaPortal _tiaPortal;

    public ProjectTreeProvider(TiaPortal tiaPortal)
    {
        _tiaPortal = tiaPortal;

        try
        {
            AddInLogger.Startup();
            AddInLogger.Info("ProjectTreeProvider initialized.");
        }
        catch
        {
            // Diagnostics must never prevent Add-In loading.
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
                HandleActionAsync("explain", selection));

        addInRoot.Items.AddActionItem<IEngineeringObject>(
            "Review selected object",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleActionAsync("review", selection));

        addInRoot.Items.AddActionItem<IEngineeringObject>(
            "Propose change",
            (MenuSelectionProvider<IEngineeringObject> selection) =>
                HandleActionAsync("propose", selection));
    }

    private async void HandleActionAsync(string action, MenuSelectionProvider<IEngineeringObject> selection)
    {
        AddInLogger.Info($"Action triggered: {action}");
        AddInLogger.Info(
            $"Current thread: {Environment.CurrentManagedThreadId}, apartment: {Thread.CurrentThread.GetApartmentState()}");

        string? acceptedTaskId = null;

        try
        {
            var selectedObject = GetSelectedObject(selection);
            if (selectedObject == null)
            {
                return;
            }

            var selectionInfo = selectedObject.ToString() ?? "Unknown";
            var typeName = selectedObject.GetType().Name;
            var correlationId = $"tia-{Guid.NewGuid():N}";

            AddInLogger.Info(
                $"Selection captured: {selectionInfo} (type: {typeName}, correlation: {correlationId})");

            var selectionSnapshot = SelectionSnapshotFactory.Create(selectedObject);
            LogSourceExtraction(selectionInfo, selectionSnapshot);

            var request = CreateBridgeRequest(
                action,
                selectionInfo,
                typeName,
                correlationId,
                selectionSnapshot);

            AddInLogger.Info($"Starting Bridge task: agentId={request.AgentId}, action={action}");
            var accepted = await AddInServices.BridgeClient
                .StartTaskAsync(request, CancellationToken.None)
                .ConfigureAwait(false);

            acceptedTaskId = accepted.TaskId;
            AddInLogger.Info(
                $"Bridge task accepted: taskId={accepted.TaskId}, status={accepted.Status}");

            var launchResult = await ResponseCenterLauncher.RequestResponseCenterLaunchAsync(
                accepted.TaskId,
                action,
                CancellationToken.None).ConfigureAwait(false);

            if (launchResult.Success)
            {
                return;
            }

            AddInLogger.Warn($"Response Center launch failed: status={launchResult.Status}, error={launchResult.ErrorMessage}");
            AddInDialogService.ShowError(
                "The Agent task is running, but the Response Center could not be opened. " +
                (launchResult.ErrorMessage ?? "Check the Agent logs for details."));
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrWhiteSpace(acceptedTaskId))
            {
                await CancelAcceptedTaskAsync(acceptedTaskId!).ConfigureAwait(false);
            }

            AddInLogger.Error($"Error handling menu action '{action}'", ex);
            AddInDialogService.ShowError(FormatUserErrorMessage(ex));
        }
    }

    private static IEngineeringObject? GetSelectedObject(
        MenuSelectionProvider<IEngineeringObject> selection)
    {
        var objects = selection.GetSelection();
        var enumerator = objects.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            AddInLogger.Warn("No object selected.");
            AddInDialogService.ShowWarning("No object selected.");
            return null;
        }

        if (enumerator.Current is not IEngineeringObject selectedObject)
        {
            AddInLogger.Warn("Selected object is not a TIA engineering object.");
            AddInDialogService.ShowWarning("Selected object is not a TIA engineering object.");
            return null;
        }

        return selectedObject;
    }

    private static BridgeTaskRequest CreateBridgeRequest(
        string action,
        string selectionInfo,
        string typeName,
        string correlationId,
        SelectionSnapshot? selectionSnapshot)
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

        var capturedSelection = selectionSnapshot ?? new SelectionSnapshot
        {
            Name = selectionInfo,
            ObjectType = typeName,
            RuntimeType = string.Empty,
            PlcName = string.Empty,
            TiaPath = selectionInfo,
            Language = string.Empty
        };

        return new BridgeTaskRequest
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
                Path = string.Empty
            },
            Selection = capturedSelection,
            UserMessage =
                $"The user selected object \"{selectionInfo}\" of type \"{typeName}\" in TIA Portal. " +
                $"Please {actionDescription}."
        };
    }

    private static void LogSourceExtraction(string selectionInfo, SelectionSnapshot? selectionSnapshot)
    {
        if (selectionSnapshot?.Source != null)
        {
            AddInLogger.Info(
                $"Source extracted: {selectionSnapshot.Source.Length} chars " +
                $"(format: {selectionSnapshot.SourceFormat})");
            return;
        }

        AddInLogger.Warn($"No source extracted for {selectionInfo}");
    }

    private static async Task CancelAcceptedTaskAsync(string taskId)
    {
        try
        {
            await AddInServices.BridgeClient
                .CancelTaskAsync(taskId, CancellationToken.None)
                .ConfigureAwait(false);
            AddInLogger.Info($"Cancelled Bridge task after Response Center startup failure: {taskId}");
        }
        catch (Exception cancelException)
        {
            AddInLogger.Error(
                $"Failed to cancel Bridge task after Response Center startup failure: {taskId}",
                cancelException);
        }
    }

    private static string FormatUserErrorMessage(Exception ex)
    {
        if (ex is System.Security.VerificationException)
        {
            return "The Add-In encountered a TIA Portal security sandbox restriction. " +
                   "Check the Add-In logs for details.";
        }

        if (ex is System.Security.SecurityException securityException)
        {
            return "A security permission was denied: " + securityException.Message;
        }

        return "Failed to start the AI assistant: " + ex.Message;
    }
}
#endif
