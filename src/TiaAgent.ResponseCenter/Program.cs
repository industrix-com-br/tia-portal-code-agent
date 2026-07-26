using System;
using System.Threading;
using System.Windows;
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
        try
        {
            var context = ParseArguments(args);
            if (context == null)
            {
                ShowUsage();
                return 1;
            }

            var mutexName = $"TiaAgent_ResponseCenter_{context.TaskId}";
            using var mutex = new Mutex(true, mutexName, out var createdNew);

            if (!createdNew)
            {
                return 0;
            }

            var app = new Application();
            using var monitor = new BridgeTaskMonitor(context);
            using var viewModel = new AgentResponseViewModel(context, monitor);

            var window = new AgentResponseWindow(viewModel)
            {
                Title = $"TIA Agent - {context.ActionDisplay}"
            };

            viewModel.StartMonitoring();
            window.Show();
            app.Run(window);
            return 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Failed to start TIA Agent Response Center:\n\n{ex.Message}",
                "TIA Agent - Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return 1;
        }
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
            InitialStage = initialStage
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
            "  [--initial-stage <stage>]\n\n" +
            "The Bridge URL and token are discovered automatically when omitted.",
            "TIA Agent - Usage",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}
