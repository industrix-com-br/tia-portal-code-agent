using System;
using System.Threading;
using System.Threading.Tasks;

namespace TiaAgent.AddIn.Ui;

internal sealed class AssistantExecutionResult
{
    public AssistantExecutionResult(string markdown, string? runtimeId)
    {
        Markdown = markdown ?? throw new ArgumentNullException(nameof(markdown));
        RuntimeId = runtimeId;
    }

    public string Markdown { get; }

    public string? RuntimeId { get; }
}

internal interface IAssistantExecutionView
{
    bool IsClosed { get; }

    CancellationToken CancellationToken { get; }

    void ShowLoading(string message);

    Task ShowAsync();

    Task ShowResultAsync(AssistantExecutionResult result);

    Task ShowErrorAsync(string message);
}

internal interface IAssistantExecutionLifetime
{
    void CompleteExecution();
}

internal sealed class AssistantExecutionCoordinator
{
    internal const string DefaultLoadingMessage = "Waiting for the Agent Code response...";

    public async Task ExecuteAsync(
        IAssistantExecutionView view,
        Func<CancellationToken, Task<AssistantExecutionResult>> executeAsync,
        Func<Exception, string> formatError,
        Action? onExecutionStarting = null,
        Action<AssistantExecutionResult>? onExecutionCompleted = null,
        Action<Exception>? onExecutionFailed = null)
    {
        if (view == null)
            throw new ArgumentNullException(nameof(view));
        if (executeAsync == null)
            throw new ArgumentNullException(nameof(executeAsync));
        if (formatError == null)
            throw new ArgumentNullException(nameof(formatError));

        try
        {
            view.ShowLoading(DefaultLoadingMessage);
            await view.ShowAsync().ConfigureAwait(false);

            if (view.IsClosed)
                return;

            try
            {
                onExecutionStarting?.Invoke();
                var result = await executeAsync(view.CancellationToken).ConfigureAwait(false);
                onExecutionCompleted?.Invoke(result);

                if (!view.IsClosed)
                    await view.ShowResultAsync(result).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                if (view.IsClosed || view.CancellationToken.IsCancellationRequested)
                    return;

                onExecutionFailed?.Invoke(ex);
                if (!view.IsClosed)
                    await view.ShowErrorAsync("The operation was cancelled.").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onExecutionFailed?.Invoke(ex);
                if (!view.IsClosed)
                    await view.ShowErrorAsync(formatError(ex)).ConfigureAwait(false);
            }
        }
        finally
        {
            (view as IAssistantExecutionLifetime)?.CompleteExecution();
        }
    }
}
