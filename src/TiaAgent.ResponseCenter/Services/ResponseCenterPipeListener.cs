using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiaAgent.Contracts.Bridge;
using TiaAgent.ResponseCenter.Diagnostics;

namespace TiaAgent.ResponseCenter.Services;

/// <summary>
/// Listens for task activation requests from the Bridge via a named pipe.
/// </summary>
public sealed class ResponseCenterPipeListener : IDisposable
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly string _pipeName;
    private CancellationTokenSource? _cts;

    /// <summary>Raised when the Bridge asks this window to display another task.</summary>
    public event Action<LaunchResponseCenterRequest>? NewTaskRequested;

    public ResponseCenterPipeListener(string tiaInstanceId)
    {
        _pipeName = GetPipeName(tiaInstanceId);
    }

    public void Start()
    {
        if (_cts != null)
            throw new InvalidOperationException("Pipe listener already started.");

        _cts = new CancellationTokenSource();
        _ = ListenLoopAsync(_cts.Token);
    }

    private async Task ListenLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var pipeServer = new NamedPipeServerStream(
                    _pipeName,
                    PipeDirection.In,
                    1,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous);

                await pipeServer.WaitForConnectionAsync(ct).ConfigureAwait(false);

                using var reader = new StreamReader(pipeServer, Encoding.UTF8);
                var line = await reader.ReadLineAsync(ct).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(line))
                {
                    ResponseCenterLogger.Warn("Activation pipe received an empty payload");
                    continue;
                }

                LaunchResponseCenterRequest? request;
                try
                {
                    request = JsonSerializer.Deserialize<LaunchResponseCenterRequest>(line, s_jsonOptions);
                }
                catch (JsonException ex)
                {
                    ResponseCenterLogger.Warn($"Activation pipe received invalid JSON: {ex.Message}");
                    continue;
                }

                if (request == null
                    || string.IsNullOrWhiteSpace(request.TaskId)
                    || string.IsNullOrWhiteSpace(request.Action)
                    || string.IsNullOrWhiteSpace(request.TiaInstanceId))
                {
                    ResponseCenterLogger.Warn("Activation pipe received an incomplete request");
                    continue;
                }

                NewTaskRequested?.Invoke(request);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                ResponseCenterLogger.Warn($"Activation pipe listener failed: {ex.Message}");
                try
                {
                    await Task.Delay(1000, ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public static string GetPipeName(string tiaInstanceId)
    {
        var sanitized = new string(tiaInstanceId
            .Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-')
            .ToArray());
        return $"TiaAgent_RC_{sanitized}";
    }
}
