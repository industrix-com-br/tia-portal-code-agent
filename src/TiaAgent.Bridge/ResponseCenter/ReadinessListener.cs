using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.Bridge.Diagnostics;
using TiaAgent.Contracts.Bridge;

namespace TiaAgent.Bridge.ResponseCenter;

/// <summary>
/// Listens for a readiness message from the Response Center via a named pipe.
/// The RC connects and sends a JSON readiness payload after its WPF window is shown.
/// </summary>
public class ReadinessListener : IDisposable
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly BridgeLogger _logger;

    public ReadinessListener(BridgeLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Waits for the Response Center to send a readiness message.
    /// Returns the readiness info, or null if the timeout expires.
    /// </summary>
    public virtual async Task<ResponseCenterReadinessInfo?> WaitForReadinessAsync(
        string instanceId, TimeSpan timeout)
    {
        var pipeName = GetPipeName(instanceId);
        _logger.Debug($"Waiting for readiness on pipe '{pipeName}' (timeout={timeout.TotalSeconds}s)");

        try
        {
            using var pipeServer = new NamedPipeServerStream(
                pipeName,
                PipeDirection.In,
                1,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous);

            using var cts = new CancellationTokenSource(timeout);

            await pipeServer.WaitForConnectionAsync(cts.Token).ConfigureAwait(false);

            _logger.Debug("Readiness pipe connected — reading payload");

            using var reader = new StreamReader(pipeServer, Encoding.UTF8);
            var line = await reader.ReadLineAsync(cts.Token).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(line))
            {
                _logger.Warn("Readiness pipe sent empty payload");
                return null;
            }

            var info = JsonSerializer.Deserialize<ResponseCenterReadinessInfo>(line, s_jsonOptions);
            if (info == null)
            {
                _logger.Warn("Readiness payload deserialized to null");
                return null;
            }

            _logger.Info(
                $"Readiness received: pid={info.ProcessId}, hwnd={info.WindowHandle}, " +
                $"isVisible={info.IsVisible}, state={info.WindowState}, taskId={info.TaskId}");

            return info;
        }
        catch (OperationCanceledException)
        {
            _logger.Warn($"Readiness wait timed out after {timeout.TotalSeconds}s for '{instanceId}'");
            return null;
        }
        catch (Exception ex)
        {
            _logger.Warn($"Readiness listener error: {ex.Message}");
            return null;
        }
    }

    internal static string GetPipeName(string instanceId)
    {
        var sanitized = new string(instanceId.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-').ToArray());
        return $"TiaAgent_RCReady_{sanitized}";
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
