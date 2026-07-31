using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TiaAgent.ResponseCenter.Diagnostics;
using TiaAgent.ResponseCenter.Models;
using TiaAgent.ResponseCenter.Services;
using TiaAgent.ResponseCenter.ViewModels;
using TiaAgent.ResponseCenter.Views;

namespace TiaAgent.ResponseCenter;

/// <summary>
/// Entry point for the Agent Response Center.
/// Launched by the TIA Portal Add-In after the Bridge accepts a task.
/// </summary>
public static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        ResponseCenterLogger.Startup();

        try
        {
            ResponseCenterLogger.Info("Response Center process starting");

            var context = ParseArguments(args);
            if (context == null)
            {
                ResponseCenterLogger.Warn("Arguments invalid or missing required fields");
                ShowUsage();
                return 1;
            }

            ResponseCenterLogger.Info(
                $"Arguments parsed: taskId={context.TaskId}, action={context.Action}, " +
                $"tiaInstance={context.TiaInstanceId}");

            var mutexName = $"TiaAgent_ResponseCenter_{context.TaskId}";
            using var mutex = new Mutex(true, mutexName, out var createdNew);

            if (!createdNew)
            {
                ResponseCenterLogger.Info("Another instance already owns this task — exiting");
                return 0;
            }

            ResponseCenterLogger.Info("Application created");
            var app = new Application();
            RegisterGlobalExceptionHandlers();
            var monitor = new BridgeTaskMonitor(context);
            var viewModel = new AgentResponseViewModel(context, monitor);
            ResponseCenterLogger.Info("ViewModel created");

            ResponseCenterLogger.Info("AgentResponseWindow constructor started");
            var window = new AgentResponseWindow(viewModel)
            {
                Title = $"TIA Agent - {context.ActionDisplay}"
            };
            ResponseCenterLogger.Info("AgentResponseWindow constructor completed");

            // Set up named pipe listener for new task notifications from the Bridge.
            // Must be after window creation so the lambda can capture it.
            ResponseCenterPipeListener? pipeListener = null;
            if (!string.IsNullOrEmpty(context.TiaInstanceId))
            {
                pipeListener = new ResponseCenterPipeListener(context.TiaInstanceId);
                pipeListener.NewTaskRequested += taskId =>
                {
                    ResponseCenterLogger.Info($"Activation request received via pipe: taskId={taskId}");
                    window.Dispatcher.Invoke(() =>
                    {
                        ResponseCenterLogger.Info("Existing window restored");
                        window.Show();

                        if (window.WindowState == WindowState.Minimized)
                        {
                            window.WindowState = WindowState.Normal;
                            ResponseCenterLogger.Info("Window restored from minimized state");
                        }

                        window.Activate();
                        window.Focus();
                        ResponseCenterLogger.Info("Existing window activated");
                    });

                    // Send readiness for the activation too, so the Bridge can confirm visibility.
                    var activationContext = context with { TaskId = taskId };
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await ReadinessReporter.SendReadinessAsync(activationContext, window, TimeSpan.FromSeconds(5))
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            ResponseCenterLogger.Warn($"Activation readiness send failed: {ex.Message}");
                        }
                    });
                };
                pipeListener.Start();
            }

            viewModel.StartMonitoring();
            ResponseCenterLogger.Info("Window Show called");
            window.Show();
            window.Activate();
            window.Focus();
            ResponseCenterLogger.Info("Window activated and focused");

            // Send readiness to the Bridge after the window is visible.
            // Fire-and-forget: this must not block the WPF message pump.
            _ = Task.Run(async () =>
            {
                try
                {
                    await ReadinessReporter.SendReadinessAsync(context, window, TimeSpan.FromSeconds(5))
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ResponseCenterLogger.Warn($"Readiness reporter failed: {ex.Message}");
                }
            });

            ResponseCenterLogger.Info("Application.Run entered");
            app.Run(window);

            ResponseCenterLogger.Info("Application shutting down");
            pipeListener?.Dispose();
            return 0;
        }
        catch (Exception ex)
        {
            ResponseCenterLogger.Error("Fatal exception in Main", ex);
            MessageBox.Show(
                $"Failed to start TIA Agent Response Center:\n\n{ex.Message}",
                "TIA Agent - Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return 1;
        }
    }

    private static void RegisterGlobalExceptionHandlers()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            ResponseCenterLogger.Error("AppDomain.UnhandledException", e.ExceptionObject as Exception);
        };

        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher != null)
        {
            dispatcher.UnhandledException += (_, e) =>
            {
                ResponseCenterLogger.Error("Dispatcher.UnhandledException", e.Exception);
                e.Handled = true;
            };
        }

        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            ResponseCenterLogger.Error("TaskScheduler.UnobservedTaskException", e.Exception);
            e.SetObserved();
        };
    }

    internal static AgentResponseContext? ParseArguments(string[] args)
    {
        string? taskId = null;
        string? bridgeUrl = null;
        string? token = null;
        string? action = null;
        string? objectName = null;
        string? objectType = null;
        string? plcName = null;
        string? projectName = null;
        string? correlationId = null;
        string? initialStatus = null;
        string? initialStage = null;
        string? tiaInstanceId = null;

        for (var index = 0; index < args.Length; index++)
        {
            var key = args[index].ToLowerInvariant();
            if (!key.StartsWith("--", StringComparison.Ordinal) || index + 1 >= args.Length)
            {
                continue;
            }

            var value = args[++index];
            switch (key)
            {
                case "--task-id":
                    taskId = value;
                    break;
                case "--bridge-url":
                    bridgeUrl = value;
                    break;
                case "--token":
                    token = value;
                    break;
                case "--action":
                    action = value;
                    break;
                case "--object-name":
                    objectName = value;
                    break;
                case "--object-type":
                    objectType = value;
                    break;
                case "--plc-name":
                    plcName = value;
                    break;
                case "--project-name":
                    projectName = value;
                    break;
                case "--correlation-id":
                    correlationId = value;
                    break;
                case "--initial-status":
                    initialStatus = value;
                    break;
                case "--initial-stage":
                    initialStage = value;
                    break;
                case "--tia-instance-id":
                    tiaInstanceId = value;
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(taskId) || string.IsNullOrWhiteSpace(action))
        {
            return null;
        }

        var connection = BridgeConnectionDiscovery.Resolve(bridgeUrl, token);

        return new AgentResponseContext
        {
            TaskId = taskId,
            BridgeUrl = connection.BridgeUrl,
            AuthToken = connection.AuthToken,
            Action = action,
            ObjectName = objectName ?? string.Empty,
            ObjectType = objectType ?? string.Empty,
            PlcName = plcName,
            ProjectName = projectName,
            CorrelationId = correlationId,
            InitialStatus = initialStatus,
            InitialStage = initialStage,
            TiaInstanceId = tiaInstanceId
        };
    }

    private static void ShowUsage()
    {
        MessageBox.Show(
            "Usage: TiaAgent.ResponseCenter.exe\n" +
            "  --task-id <id>\n" +
            "  --action <explain|review|propose>\n" +
            "  [--bridge-url <url>]\n" +
            "  [--token <bearer-token>]\n" +
            "  [--object-name <name>]\n" +
            "  [--object-type <type>]\n" +
            "  [--plc-name <name>]\n" +
            "  [--project-name <name>]\n" +
            "  [--correlation-id <id>]\n" +
            "  [--initial-status <status>]\n" +
            "  [--initial-stage <stage>]\n" +
            "  [--tia-instance-id <id>]\n\n" +
            "The Bridge URL and token are discovered automatically when omitted.",
            "TIA Agent - Usage",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}
