using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using TiaAgent.ResponseCenter.Diagnostics;
using TiaAgent.ResponseCenter.Models;

namespace TiaAgent.ResponseCenter.Services;

/// <summary>
/// Reports UI readiness from the Response Center to the Bridge.
/// After the WPF window is shown, this sends a JSON readiness message
/// through a named pipe so the Bridge can confirm the window is visible.
/// </summary>
public static class ReadinessReporter
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Sends a readiness message to the Bridge via named pipe.
    /// Best-effort: never throws, never crashes the application.
    /// </summary>
    public static async Task SendReadinessAsync(
        AgentResponseContext context,
        Window window,
        TimeSpan? timeout = null)
    {
        try
        {
            var pipeName = GetPipeName(context.TiaInstanceId ?? context.TaskId);
            var effectiveTimeout = timeout ?? TimeSpan.FromSeconds(5);

            // BuildReadinessInfo accesses WPF Window properties (IsVisible, WindowState,
            // WindowInteropHelper) which must be called from the UI thread.
            ReadinessInfo info;
            if (window.Dispatcher.CheckAccess())
            {
                info = BuildReadinessInfo(context, window);
            }
            else
            {
                info = await window.Dispatcher.InvokeAsync(
                    () => BuildReadinessInfo(context, window)).Task
                    .ConfigureAwait(false);
            }

            var json = JsonSerializer.Serialize(info, s_jsonOptions);

            ResponseCenterLogger.Info($"Sending readiness to Bridge via pipe '{pipeName}': {json}");

            using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            using var cts = new CancellationTokenSource(effectiveTimeout);

            await pipeClient.ConnectAsync(cts.Token).ConfigureAwait(false);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json + "\n");
            await pipeClient.WriteAsync(buffer, cts.Token).ConfigureAwait(false);
            await pipeClient.FlushAsync(cts.Token).ConfigureAwait(false);

            ResponseCenterLogger.Info("Readiness sent successfully");
        }
        catch (OperationCanceledException)
        {
            ResponseCenterLogger.Warn("Readiness send timed out — Bridge may not be listening");
        }
        catch (Exception ex)
        {
            ResponseCenterLogger.Warn($"Failed to send readiness: {ex.Message}");
        }
    }

    internal static ReadinessInfo BuildReadinessInfo(AgentResponseContext context, Window window)
    {
        var process = Process.GetCurrentProcess();
        var helper = new WindowInteropHelper(window);
        var hwnd = helper.Handle;

        return new ReadinessInfo
        {
            ProcessId = process.Id,
            WindowHandle = hwnd.ToInt64(),
            TaskId = context.TaskId,
            TiaInstanceId = context.TiaInstanceId,
            IsVisible = window.IsVisible,
            WindowState = window.WindowState.ToString(),
            WindowLeft = window.Left,
            WindowTop = window.Top,
            ScreenWidth = SystemParameters.PrimaryScreenWidth,
            ScreenHeight = SystemParameters.PrimaryScreenHeight
        };
    }

    internal static string GetPipeName(string instanceId)
    {
        var sanitized = new string(instanceId.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-').ToArray());
        return $"TiaAgent_RCReady_{sanitized}";
    }

    internal sealed class ReadinessInfo
    {
        public int ProcessId { get; set; }
        public long WindowHandle { get; set; }
        public string TaskId { get; set; } = null!;
        public string? TiaInstanceId { get; set; }
        public bool IsVisible { get; set; }
        public string WindowState { get; set; } = null!;
        public double WindowLeft { get; set; }
        public double WindowTop { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
    }
}
