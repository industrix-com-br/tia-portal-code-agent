using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using FluentAssertions;
using TiaAgent.AddIn.Ui;
using Xunit;

namespace TiaAgent.AddIn.Tests;

public class WpfThreadHostTests
{
    [Fact]
    public void Start_CreatesDispatcherAndThread()
    {
        using var host = new WpfThreadHost();
        var started = host.Start(TimeSpan.FromSeconds(5));

        started.Should().BeTrue();
        host.Dispatcher.Should().NotBeNull();
        host.Dispatcher.Thread.Should().NotBeNull();
        host.Dispatcher.Thread.GetApartmentState().Should().Be(ApartmentState.STA);
    }

    [Fact]
    public async Task WindowReady_CompletesAfterWindowLoads()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "ready-test", "test-object"));

        var completed = await Task.WhenAny(host.WindowReady, Task.Delay(TimeSpan.FromSeconds(5)));
        completed.Should().Be(host.WindowReady);
        (await host.WindowReady).Should().BeTrue();
    }

    [Fact]
    public async Task WindowReady_CompletesWithFallbackOnTimeout()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        // Don't let the window load — signal fallback directly
        host.SignalWindowReadyFallback();

        await Task.WhenAny(host.WindowReady, Task.Delay(TimeSpan.FromSeconds(1)));
        host.WindowReady.IsCompleted.Should().BeTrue();
        var ready = await host.WindowReady;
        ready.Should().BeFalse();
    }

    [Fact]
    public void RequestShutdown_StopsDispatcher()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        host.RequestShutdown();

        var stopped = host.WaitForShutdown(TimeSpan.FromSeconds(3));
        stopped.Should().BeTrue();
    }

    [Fact]
    public void RequestShutdown_IsIdempotent()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        host.RequestShutdown();
        host.RequestShutdown(); // Second call should not throw

        var stopped = host.WaitForShutdown(TimeSpan.FromSeconds(3));
        stopped.Should().BeTrue();
    }

    [Fact]
    public void CloseToken_IsCancelledAfterRequestShutdown()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        host.CloseToken.IsCancellationRequested.Should().BeFalse();

        host.RequestShutdown();

        host.CloseToken.IsCancellationRequested.Should().BeTrue();
    }

    [Fact]
    public void CreateAndShowWindow_ReturnsWindowOnWpfThread()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        int? creationThreadId = null;

        var window = host.CreateAndShowWindow(ct =>
        {
            creationThreadId = Environment.CurrentManagedThreadId;
            return new AssistantExecutionWindow("explain", "test-id", "test-object");
        });

        window.Should().NotBeNull();
        creationThreadId.Should().Be(host.Dispatcher.Thread.ManagedThreadId);

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }

    [Fact]
    public void MultipleExecutions_CreateFreshHosts()
    {
        var host1 = new WpfThreadHost();
        host1.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();
        var thread1Id = host1.Dispatcher.Thread.ManagedThreadId;
        host1.RequestShutdown();
        host1.WaitForShutdown(TimeSpan.FromSeconds(3));
        host1.Dispose();

        var host2 = new WpfThreadHost();
        host2.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();
        var thread2Id = host2.Dispatcher.Thread.ManagedThreadId;

        thread2Id.Should().NotBe(thread1Id);

        host2.RequestShutdown();
        host2.WaitForShutdown(TimeSpan.FromSeconds(3));
        host2.Dispose();
    }

    [Fact]
    public void Dispose_StopsHostThread()
    {
        var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var thread = host.Dispatcher.Thread;
        thread.IsAlive.Should().BeTrue();

        host.Dispose();

        // Background thread should stop shortly after Dispose
        var stopped = thread.Join(TimeSpan.FromSeconds(3));
        stopped.Should().BeTrue();
    }

    [Fact]
    public void Dispatcher_ThrowsBeforeStart()
    {
        using var host = new WpfThreadHost();
        Action act = () => { var _ = host.Dispatcher; };
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Start_DoesNotThrowOnTimeout()
    {
        // Start with very short timeout — may or may not succeed depending on scheduling
        using var host = new WpfThreadHost();
        var started = host.Start(TimeSpan.FromMilliseconds(1));
        // Just verify no exception is thrown
        host.Dispose();
    }

    [Fact]
    public async Task WpfExecutionViewProxy_ShowLoading_DispatchesToWpfThread()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "proxy-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        proxy.ShowLoading("test message");

        // Give the dispatcher time to process
        await Task.Delay(200);

        proxy.IsClosed.Should().BeFalse();

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }

    [Fact]
    public async Task WpfExecutionViewProxy_ShowResultAsync_UpdatesWindow()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "result-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        var result = new AssistantExecutionResult("# Test Result", "test-runtime");
        await proxy.ShowResultAsync(result);

        proxy.IsClosed.Should().BeFalse();

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }

    [Fact]
    public async Task WpfExecutionViewProxy_ShowErrorAsync_UpdatesWindow()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "error-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        await proxy.ShowErrorAsync("Something went wrong");

        proxy.IsClosed.Should().BeFalse();

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }

    [Fact]
    public void WpfExecutionViewProxy_CompleteExecution_ShutsDownHost()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "complete-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        proxy.CompleteExecution();

        var stopped = host.WaitForShutdown(TimeSpan.FromSeconds(3));
        stopped.Should().BeTrue();
    }

    [Fact]
    public void WpfExecutionViewProxy_CancellationToken_IsHostCloseToken()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "token-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        proxy.CancellationToken.Should().Be(host.CloseToken);

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }

    [Fact]
    public async Task BridgeRunsOffWpfThread_DoesNotBlockDispatcher()
    {
        using var host = new WpfThreadHost();
        host.Start(TimeSpan.FromSeconds(5)).Should().BeTrue();

        var window = host.CreateAndShowWindow(ct =>
            new AssistantExecutionWindow("explain", "bridge-test", "test-object"));

        var proxy = new WpfExecutionViewProxy(host, window);

        // Simulate a long-running Bridge call on a background thread
        var bridgeTask = Task.Run(async () =>
        {
            await Task.Delay(500);
            return new AssistantExecutionResult("# Bridge Response", null);
        });

        // The WPF thread should remain responsive during the Bridge call
        var responsivenessCheck = Task.Run(async () =>
        {
            var checks = 0;
            while (!bridgeTask.IsCompleted)
            {
                var tcs = new TaskCompletionSource<bool>();
                // Use BeginInvoke (fire-and-forget) to post to the WPF dispatcher
#pragma warning disable CS4014 // BeginInvoke is fire-and-forget by design
                host.Dispatcher.BeginInvoke(new Action(() => tcs.TrySetResult(true)),
                    DispatcherPriority.Normal);
#pragma warning restore CS4014
                await tcs.Task;
                checks++;
                await Task.Delay(50);
            }
            return checks;
        });

        var result = await bridgeTask;
        var responsivenessChecks = await responsivenessCheck;

        result.Markdown.Should().Be("# Bridge Response");
        responsivenessChecks.Should().BeGreaterThan(5); // Should have been responsive

        proxy.IsClosed.Should().BeFalse();

        host.RequestShutdown();
        host.WaitForShutdown(TimeSpan.FromSeconds(3));
    }
}
