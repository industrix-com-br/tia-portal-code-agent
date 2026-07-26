#if SIEMENS
using System;
using System.Threading;
using System.Windows;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

internal static class AssistantExecutionWindowFactory
{
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
                var dispatcher = Application.Current?.Dispatcher;
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
#endif
