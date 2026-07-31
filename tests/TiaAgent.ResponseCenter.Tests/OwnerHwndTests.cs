using System;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.Contracts.Bridge;
using TiaAgent.ResponseCenter.Models;
using TiaAgent.ResponseCenter.Services;
using Xunit;

namespace TiaAgent.ResponseCenter.Tests;

public sealed class OwnerHwndTests
{
    [Fact]
    public void AgentResponseContext_PreservesTiaInstanceId()
    {
        var context = CreateContext("task-1", "tia-abc-123");

        context.TiaInstanceId.Should().Be("tia-abc-123");
    }

    [Fact]
    public void BuildMutexName_UsesStableTiaInstanceId()
    {
        var firstTask = CreateContext("task-1", "tia-abc-123");
        var secondTask = CreateContext("task-2", "tia-abc-123");

        Program.BuildMutexName(firstTask)
            .Should().Be("TiaAgent_ResponseCenter_tia-abc-123");
        Program.BuildMutexName(secondTask)
            .Should().Be(Program.BuildMutexName(firstTask));
    }

    [Fact]
    public void BuildMutexName_FallsBackToTaskIdForLegacyLaunches()
    {
        var context = CreateContext("task-legacy", null);

        Program.BuildMutexName(context)
            .Should().Be("TiaAgent_ResponseCenter_task-legacy");
    }

    [Fact]
    public void GetPipeName_SanitizesInstanceId()
    {
        ResponseCenterPipeListener.GetPipeName("tia-123-abc")
            .Should().Be("TiaAgent_RC_tia-123-abc");
    }

    [Fact]
    public void GetPipeName_RemovesSpecialCharacters()
    {
        ResponseCenterPipeListener.GetPipeName("tia@#$%123!def")
            .Should().Be("TiaAgent_RC_tia123def");
    }

    [Fact]
    public void GetPipeName_PreservesUnderscores()
    {
        ResponseCenterPipeListener.GetPipeName("tia_instance_1")
            .Should().Be("TiaAgent_RC_tia_instance_1");
    }

    [Fact]
    public async Task PipeListener_RaisesEventWithCompleteActivationRequest()
    {
        using var listener = new ResponseCenterPipeListener("test-instance");
        var completion = new TaskCompletionSource<LaunchResponseCenterRequest>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        listener.NewTaskRequested += request => completion.TrySetResult(request);
        listener.Start();

        var request = new LaunchResponseCenterRequest
        {
            TaskId = "new-task-id",
            TiaInstanceId = "test-instance",
            Action = "review"
        };

        using var client = new NamedPipeClientStream(
            ".",
            "TiaAgent_RC_test-instance",
            PipeDirection.Out);
        client.Connect(5000);

        var json = JsonSerializer.Serialize(request) + "\n";
        var bytes = Encoding.UTF8.GetBytes(json);
        await client.WriteAsync(bytes);
        await client.FlushAsync();

        var completed = await Task.WhenAny(completion.Task, Task.Delay(5000));
        completed.Should().Be(completion.Task);

        var received = await completion.Task;
        received.TaskId.Should().Be("new-task-id");
        received.TiaInstanceId.Should().Be("test-instance");
        received.Action.Should().Be("review");
    }

    private static AgentResponseContext CreateContext(string taskId, string? tiaInstanceId)
    {
        return new AgentResponseContext
        {
            TaskId = taskId,
            BridgeUrl = "http://127.0.0.1:43119",
            Action = "explain",
            TiaInstanceId = tiaInstanceId
        };
    }
}
