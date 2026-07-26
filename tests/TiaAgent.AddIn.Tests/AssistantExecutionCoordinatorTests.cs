using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.AddIn.Ui;
using Xunit;

namespace TiaAgent.AddIn.Tests;

public class AssistantExecutionCoordinatorTests
{
    [Fact]
    public async Task ExecuteAsync_ShowsLoadingAndWindowBeforeStartingOperation()
    {
        var sequence = new List<string>();
        var response = new TaskCompletionSource<AssistantExecutionResult>();
        var view = new FakeExecutionView(sequence);
        var coordinator = new AssistantExecutionCoordinator();

        var execution = coordinator.ExecuteAsync(
            view,
            _ =>
            {
                sequence.Add("operation-started");
                return response.Task;
            },
            ex => ex.Message);

        sequence.Should().ContainInOrder("loading", "shown", "operation-started");
        view.LoadingMessage.Should().Be(AssistantExecutionCoordinator.DefaultLoadingMessage);
        execution.IsCompleted.Should().BeFalse();

        response.SetResult(new AssistantExecutionResult("# Completed", "claude"));
        await execution;

        sequence.Should().ContainInOrder("operation-started", "result");
        view.Result?.Markdown.Should().Be("# Completed");
    }

    [Fact]
    public async Task ExecuteAsync_ReplacesLoadingWithResult()
    {
        var view = new FakeExecutionView();
        var coordinator = new AssistantExecutionCoordinator();

        await coordinator.ExecuteAsync(
            view,
            _ => Task.FromResult(new AssistantExecutionResult("**Result**", "codex")),
            ex => ex.Message);

        view.Result.Should().NotBeNull();
        view.Result!.Markdown.Should().Be("**Result**");
        view.Error.Should().BeNull();
    }

    [Fact]
    public async Task ExecuteAsync_DisplaysErrorsInsideTheView()
    {
        var view = new FakeExecutionView();
        var coordinator = new AssistantExecutionCoordinator();

        await coordinator.ExecuteAsync(
            view,
            _ => Task.FromException<AssistantExecutionResult>(new InvalidOperationException("bridge failed")),
            ex => "Friendly error: " + ex.Message);

        view.Error.Should().Be("Friendly error: bridge failed");
        view.Result.Should().BeNull();
    }

    [Fact]
    public async Task ExecuteAsync_DoesNotUpdateAWindowClosedDuringProcessing()
    {
        var response = new TaskCompletionSource<AssistantExecutionResult>();
        var view = new FakeExecutionView();
        var coordinator = new AssistantExecutionCoordinator();

        var execution = coordinator.ExecuteAsync(
            view,
            _ => response.Task,
            ex => ex.Message);

        view.Close();
        response.SetResult(new AssistantExecutionResult("Late response", null));
        await execution;

        view.Result.Should().BeNull();
        view.Error.Should().BeNull();
    }

    [Fact]
    public async Task ExecuteAsync_DoesNotStartOperationWhenWindowClosesBeforeRendering()
    {
        var operationStarted = false;
        var view = new FakeExecutionView { CloseWhenShown = true };
        var coordinator = new AssistantExecutionCoordinator();

        await coordinator.ExecuteAsync(
            view,
            _ =>
            {
                operationStarted = true;
                return Task.FromResult(new AssistantExecutionResult("Unexpected", null));
            },
            ex => ex.Message);

        operationStarted.Should().BeFalse();
        view.Result.Should().BeNull();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsWithoutBlockingWhileOperationIsPending()
    {
        var response = new TaskCompletionSource<AssistantExecutionResult>();
        var view = new FakeExecutionView();
        var coordinator = new AssistantExecutionCoordinator();

        var execution = coordinator.ExecuteAsync(
            view,
            _ => response.Task,
            ex => ex.Message);

        execution.IsCompleted.Should().BeFalse();
        view.WasShown.Should().BeTrue();

        response.SetResult(new AssistantExecutionResult("Completed", null));
        await execution;
    }

    private sealed class FakeExecutionView : IAssistantExecutionView
    {
        private readonly List<string> _sequence;

        public FakeExecutionView(List<string>? sequence = null)
        {
            _sequence = sequence ?? new List<string>();
        }

        public bool IsClosed { get; private set; }

        public CancellationToken CancellationToken => System.Threading.CancellationToken.None;

        public bool CloseWhenShown { get; set; }

        public bool WasShown { get; private set; }

        public string? LoadingMessage { get; private set; }

        public AssistantExecutionResult? Result { get; private set; }

        public string? Error { get; private set; }

        public void ShowLoading(string message)
        {
            LoadingMessage = message;
            _sequence.Add("loading");
        }

        public Task ShowAsync()
        {
            WasShown = true;
            _sequence.Add("shown");
            if (CloseWhenShown)
                Close();
            return Task.CompletedTask;
        }

        public Task ShowResultAsync(AssistantExecutionResult result)
        {
            Result = result;
            _sequence.Add("result");
            return Task.CompletedTask;
        }

        public Task ShowErrorAsync(string message)
        {
            Error = message;
            _sequence.Add("error");
            return Task.CompletedTask;
        }

        public void Close()
        {
            IsClosed = true;
        }
    }
}
