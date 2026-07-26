using System;
using System.Threading;
using System.Windows.Threading;
using TiaAgent.AddIn.Diagnostics;

namespace TiaAgent.AddIn.Ui;

/// <summary>
/// Hosts a WPF window on a dedicated STA thread with an active Dispatcher pump.
/// The TIA Portal callback thread creates this, starts it, and returns immediately.
/// The WPF thread runs Dispatcher.Run() until shutdown is requested.
/// </summary>
internal sealed class WpfThreadHost : IDisposable
{
    private readonly ManualResetEventSlim _dispatcherReady = new(false);
    private readonly TaskCompletionSource<bool> _windowReady = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private readonly CancellationTokenSource _closeCts = new();
    private Thread? _hostThread;
    private Dispatcher? _dispatcher;
    private int _isDisposed;
    private bool _shutdownRequested;

    /// <summary>
    /// Gets a Task that completes when the WPF window has fired its Loaded event
    /// or when the timeout expires, whichever comes first.
    /// </summary>
    public Task<bool> WindowReady => _windowReady.Task;

    /// <summary>
    /// Gets the CancellationToken that is cancelled when the window is closed.
    /// </summary>
    public CancellationToken CloseToken => _closeCts.Token;

    /// <summary>
    /// Starts the dedicated STA thread and waits for the Dispatcher to be ready.
    /// Returns false if the thread could not be started within the timeout.
    /// </summary>
    public bool Start(TimeSpan? timeout = null)
    {
        var effectiveTimeout = timeout ?? TimeSpan.FromSeconds(5);

        _hostThread = new Thread(ThreadEntry)
        {
            IsBackground = true,
            Name = "TiaAgent-WpfHost"
        };

        AddInLogger.Info($"WPF host thread starting. (thread={_hostThread.ManagedThreadId})");

        _hostThread.SetApartmentState(ApartmentState.STA);
        _hostThread.Start();

        if (!_dispatcherReady.Wait(effectiveTimeout))
        {
            AddInLogger.Warn($"WPF dispatcher not ready within {effectiveTimeout.TotalSeconds}s. Aborting host thread.");
            return false;
        }

        AddInLogger.Info($"WPF host thread apartment: STA.");
        return true;
    }

    /// <summary>
    /// Gets the Dispatcher for this host's STA thread. Throws if not started.
    /// </summary>
    public Dispatcher Dispatcher
    {
        get
        {
            if (_dispatcher == null)
                throw new InvalidOperationException("WpfThreadHost has not been started.");
            return _dispatcher;
        }
    }

    /// <summary>
    /// Creates a window on the WPF thread, hooks Loaded to signal readiness,
    /// and shows it. Must be called after Start().
    /// The windowFactory and onReady callback are invoked on the WPF thread.
    /// </summary>
    public AssistantExecutionWindow CreateAndShowWindow(
        Func<CancellationToken, AssistantExecutionWindow> windowFactory)
    {
        if (_dispatcher == null)
            throw new InvalidOperationException("WpfThreadHost has not been started.");

        AssistantExecutionWindow? window = null;

        // All work runs on the WPF thread in the correct order:
        // 1. Create window
        // 2. Hook Loaded event (before Show, so we catch it)
        // 3. Show window
        _dispatcher.Invoke(() =>
        {
            window = windowFactory(_closeCts.Token);
            AddInLogger.Info($"WPF window created. (thread={Environment.CurrentManagedThreadId})");

            window!.HookLoaded(() =>
            {
                AddInLogger.Info($"WPF window loaded. (thread={Environment.CurrentManagedThreadId})");
                _windowReady.TrySetResult(true);
            });

            window.ShowWindow();
            AddInLogger.Info($"WPF window Show() returned. (thread={Environment.CurrentManagedThreadId})");
        });

        return window!;
    }

    /// <summary>
    /// Signals that the window is ready with a fallback (timeout expired).
    /// </summary>
    public void SignalWindowReadyFallback()
    {
        AddInLogger.Warn($"WPF window ready timeout — proceeding without Loaded confirmation. (thread={Environment.CurrentManagedThreadId})");
        _windowReady.TrySetResult(false);
    }

    /// <summary>
    /// Requests shutdown of the WPF Dispatcher and signals cancellation.
    /// Non-blocking: enqueues shutdown on the Dispatcher.
    /// </summary>
    public void RequestShutdown()
    {
        if (_shutdownRequested)
            return;

        _shutdownRequested = true;
        AddInLogger.Info($"WPF dispatcher shutdown requested. (thread={_hostThread?.ManagedThreadId})");

        if (!_closeCts.IsCancellationRequested)
            _closeCts.Cancel();

        try
        {
            _dispatcher?.BeginInvokeShutdown(DispatcherPriority.Send);
        }
        catch (ObjectDisposedException)
        {
            // Dispatcher already shut down — fine.
        }
    }

    /// <summary>
    /// Waits for the host thread to stop. Does not block the WPF thread.
    /// </summary>
    public bool WaitForShutdown(TimeSpan? timeout = null)
    {
        var effectiveTimeout = timeout ?? TimeSpan.FromSeconds(3);
        if (_hostThread == null)
            return true;

        _hostThread.Join(effectiveTimeout);
        var stopped = !_hostThread.IsAlive;
        if (stopped)
            AddInLogger.Info($"WPF host thread stopped. (thread={_hostThread.ManagedThreadId})");
        return stopped;
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _isDisposed, 1) != 0)
            return;

        RequestShutdown();

        // Give the thread a moment to exit, then let it die as a background thread.
        WaitForShutdown(TimeSpan.FromSeconds(1));

        _closeCts.Dispose();
        _dispatcherReady.Dispose();
    }

    private void ThreadEntry()
    {
        try
        {
            AddInLogger.Info($"WPF host thread apartment: {Thread.CurrentThread.GetApartmentState()}.");

            _dispatcher = Dispatcher.CurrentDispatcher;
            AddInLogger.Info($"WPF dispatcher created. (thread={Environment.CurrentManagedThreadId}, dispatcher={_dispatcher.GetHashCode()})");

            _dispatcherReady.Set();

            Dispatcher.Run();

            AddInLogger.Info($"WPF dispatcher exited. (thread={Environment.CurrentManagedThreadId})");
        }
        catch (Exception ex)
        {
            AddInLogger.Error($"WPF host thread crashed: {ex.GetType().FullName}: {ex.Message}", ex);
            _windowReady.TrySetResult(false);
        }
    }
}
