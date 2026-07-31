using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TiaAgent.ResponseCenter.Services;

/// <summary>
/// Listens for new task notifications from the Bridge via a named pipe.
/// When the Bridge detects an existing Response Center instance for a TIA session,
/// it sends a new task ID through this pipe instead of starting another process.
/// </summary>
public sealed class ResponseCenterPipeListener : IDisposable
{
    private readonly string _pipeName;
    private CancellationTokenSource? _cts;

    /// <summary>Raised when the Bridge sends a new task ID.</summary>
    public event Action<string>? NewTaskRequested;

    public ResponseCenterPipeListener(string tiaInstanceId)
    {
        _pipeName = GetPipeName(tiaInstanceId);
    }

    public void Start()
    {
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
                if (!string.IsNullOrWhiteSpace(line))
                {
                    NewTaskRequested?.Invoke(line.Trim());
                }

                pipeServer.Disconnect();
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch
            {
                // Brief delay before retrying to avoid tight loop on persistent errors
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
    }

    public static string GetPipeName(string tiaInstanceId)
    {
        var sanitized = new string(tiaInstanceId.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-').ToArray());
        return $"TiaAgent_RC_{sanitized}";
    }
}
