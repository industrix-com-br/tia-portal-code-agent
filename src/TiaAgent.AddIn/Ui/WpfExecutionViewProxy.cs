using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

/// <summary>
/// Thread-safe proxy that implements IAssistantExecutionView by delegating
/// all WPF operations to the dedicated STA thread's Dispatcher.
/// The caller (TIA Portal callback thread) interacts with this proxy;
/// all UI work happens on the WPF host thread.
/// </summary>
internal sealed class WpfExecutionViewProxy : IAssistantExecutionView
{
    private readonly WpfThreadHost _host;
    private readonly AssistantExecutionWindow _window;

    public WpfExecutionViewProxy(WpfThreadHost host, AssistantExecutionWindow window)
    {
        _host = host ?? throw new ArgumentNullException(nameof(host));
        _window = window ?? throw new ArgumentNullException(nameof(window));
    }

    public bool IsClosed => _window.IsClosed;

    public CancellationToken CancellationToken => _host.CloseToken;

    public void ShowLoading(string message)
    {
        if (IsClosed)
            return;

        _host.Dispatcher.BeginInvoke(new Action(() =>
        {
            if (!_window.IsClosed)
                _window.ShowLoading(message);
        }), DispatcherPriority.Normal);
    }

    public Task ShowAsync()
    {
        // The window is already shown by WpfThreadHost.CreateAndShowWindow.
        // This is called by the coordinator before starting the operation.
        return Task.CompletedTask;
    }

    public Task ShowResultAsync(AssistantExecutionResult result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        if (IsClosed)
            return Task.CompletedTask;

        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        _host.Dispatcher.BeginInvoke(new Action(() =>
        {
            try
            {
                if (!_window.IsClosed)
                {
                    // ShowResultAsync uses InvokeOnDispatcherAsync internally,
                    // but since we're already on the dispatcher, it runs directly.
                    _window.ShowResultAsync(result).GetAwaiter().GetResult();
                    AddInLogger.Info("WPF result updated via proxy.");
                }
                tcs.TrySetResult(null);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        }), DispatcherPriority.Normal);

        return tcs.Task;
    }

    public Task ShowErrorAsync(string message)
    {
        if (IsClosed)
            return Task.CompletedTask;

        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        _host.Dispatcher.BeginInvoke(new Action(() =>
        {
            try
            {
                if (!_window.IsClosed)
                {
                    _window.ShowErrorAsync(message).GetAwaiter().GetResult();
                    AddInLogger.Info("WPF error state displayed via proxy.");
                }
                tcs.TrySetResult(null);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        }), DispatcherPriority.Normal);

        return tcs.Task;
    }

}
