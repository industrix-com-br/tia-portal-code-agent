using System;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

internal static class AssistantExecutionWindowFactory
{
    private static readonly TimeSpan s_defaultWindowReadyTimeout = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Creates a WPF window on a dedicated STA thread with an active Dispatcher.
    /// Returns a proxy that marshals all operations to the WPF thread.
    /// The TIA Portal callback thread returns immediately without blocking.
    /// </summary>
    public static async Task<(IAssistantExecutionView view, WpfThreadHost host)?> TryCreateAsync(
        string action,
        string correlationId,
        string targetObject,
        TimeSpan? windowReadyTimeout = null,
        CancellationToken ct = default)
    {
        var timeout = windowReadyTimeout ?? s_defaultWindowReadyTimeout;

        WpfThreadHost? host = null;
        try
        {
            host = new WpfThreadHost();
            if (!host.Start(timeout))
            {
                AddInLogger.Error("WPF host thread failed to start within timeout.");
                host.Dispose();
                return null;
            }

            AssistantExecutionWindow? window = null;

            try
            {
                window = host.CreateAndShowWindow(_ =>
                    new AssistantExecutionWindow(action, correlationId, targetObject));
            }
            catch (Exception ex)
            {
                AddInLogger.Error($"WPF window creation failed: {ex.GetType().FullName}: {ex.Message}", ex);
                host.Dispose();
                return null;
            }

            if (window == null)
            {
                AddInLogger.Warn("WPF window creation returned no window instance.");
                host.Dispose();
                return null;
            }

            // Wait for Loaded event with timeout — never block forever.
            // .NET Framework 4.8 doesn't have Task.WaitAsync, so use Task.WhenAny.
            var readyTask = host.WindowReady;
            var timeoutTask = Task.Delay(timeout, ct);
            var completed = await Task.WhenAny(readyTask, timeoutTask).ConfigureAwait(false);

            if (completed == timeoutTask)
            {
                // Timeout — proceed anyway. The window may still be functional.
                host.SignalWindowReadyFallback();
            }

            var proxy = new WpfExecutionViewProxy(host, window);
            AddInLogger.Info($"WPF window created via dedicated STA thread. (host thread={host.Dispatcher.Thread.ManagedThreadId})");
            return (proxy, host);
        }
        catch (OperationCanceledException)
        {
            AddInLogger.Warn("WPF window creation cancelled.");
            host?.Dispose();
            return null;
        }
        catch (Exception ex)
        {
            AddInLogger.Error($"WPF window creation failed: {ex.GetType().FullName}: {ex.Message}", ex);
            host?.Dispose();
            return null;
        }
    }

    [Obsolete("Use TryCreateAsync instead. Kept for backward compatibility with tests.")]
    public static bool TryCreate(
        string action,
        string correlationId,
        string targetObject,
        out IAssistantExecutionView? view)
    {
        view = null;

        try
        {
            AssistantExecutionWindow? window = null;

            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                window = new AssistantExecutionWindow(action, correlationId, targetObject);
            }
            else
            {
                var dispatcher = System.Windows.Application.Current?.Dispatcher;
                if (dispatcher == null || dispatcher.HasShutdownStarted || dispatcher.HasShutdownFinished)
                {
                    AddInLogger.Warn("WPF window creation failed: no active STA dispatcher is available.");
                    return false;
                }

                dispatcher.Invoke(() =>
                {
                    window = new AssistantExecutionWindow(action, correlationId, targetObject);
                });
            }

            if (window == null)
            {
                AddInLogger.Warn("WPF window creation returned no window instance.");
                return false;
            }

            view = window;
            AddInLogger.Info("WPF window created.");
            return true;
        }
        catch (Exception ex)
        {
            AddInLogger.Error($"WPF window creation failed: {ex.GetType().FullName}: {ex.Message}", ex);
            return false;
        }
    }
}
