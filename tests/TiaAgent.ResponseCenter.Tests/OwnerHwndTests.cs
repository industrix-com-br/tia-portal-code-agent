using System.Threading.Tasks;
using FluentAssertions;
using TiaAgent.ResponseCenter.Services;
using Xunit;

namespace TiaAgent.ResponseCenter.Tests;

public sealed class OwnerHwndTests
{
    [Fact]
    public void AgentResponseContext_PreservesTiaInstanceId()
    {
        var context = new Models.AgentResponseContext
        {
            TaskId = "task-1",
            BridgeUrl = "http://127.0.0.1:43119",
            Action = "explain",
            TiaInstanceId = "tia-abc-123"
        };

        context.TiaInstanceId.Should().Be("tia-abc-123");
    }

    [Fact]
    public void GetPipeName_SanitizesInstanceId()
    {
        var pipeName = ResponseCenterPipeListener.GetPipeName("tia-123-abc");
        pipeName.Should().Be("TiaAgent_RC_tia-123-abc");
    }

    [Fact]
    public void GetPipeName_RemovesSpecialCharacters()
    {
        var pipeName = ResponseCenterPipeListener.GetPipeName("tia@#$%123!def");
        pipeName.Should().Be("TiaAgent_RC_tia123def");
    }

    [Fact]
    public void GetPipeName_PreservesUnderscores()
    {
        var pipeName = ResponseCenterPipeListener.GetPipeName("tia_instance_1");
        pipeName.Should().Be("TiaAgent_RC_tia_instance_1");
    }

    [Fact]
    public async Task PipeListener_RaisesEvent_WhenMessageReceived()
    {
        using var listener = new ResponseCenterPipeListener("test-instance");
        string? receivedTaskId = null;
        listener.NewTaskRequested += id => receivedTaskId = id;
        listener.Start();

        // Give the listener time to create the pipe server
        await Task.Delay(500);

        // Connect and send a task ID
        using var client = new System.IO.Pipes.NamedPipeClientStream(
            ".", "TiaAgent_RC_test-instance", System.IO.Pipes.PipeDirection.Out);
        client.Connect(5000);
        var bytes = System.Text.Encoding.UTF8.GetBytes("new-task-id\n");
        client.Write(bytes, 0, bytes.Length);
        client.Flush();

        // Wait for the event to fire
        await Task.Delay(500);

        receivedTaskId.Should().Be("new-task-id");
    }
}
