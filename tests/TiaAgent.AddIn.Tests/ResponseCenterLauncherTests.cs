using System;
using System.IO;
using FluentAssertions;
using TiaAgent.AddIn.Ui;
using Xunit;

namespace TiaAgent.AddIn.Tests;

public sealed class ResponseCenterLauncherTests : IDisposable
{
    private readonly string _root = Path.Combine(
        Path.GetTempPath(),
        "tia-agent-response-center-tests",
        Guid.NewGuid().ToString("N"));

    [Fact]
    public void ResolveExecutablePath_UsesActiveInstalledVersion()
    {
        Directory.CreateDirectory(_root);
        File.WriteAllText(
            Path.Combine(_root, "current.json"),
            "{\"schemaVersion\":1,\"activeVersion\":\"0.4.0-beta.2\"}");

        var path = ResponseCenterLauncher.ResolveExecutablePath(_root);

        path.Should().Be(Path.Combine(
            _root,
            "versions",
            "0.4.0-beta.2",
            "ResponseCenter",
            ResponseCenterLauncher.ExecutableName));
    }

    [Fact]
    public void ParseActiveVersion_IsCaseInsensitive()
    {
        var version = ResponseCenterLauncher.ParseActiveVersion(
            "{\"ActiveVersion\":\"0.3.2\"}");

        version.Should().Be("0.3.2");
    }

    [Fact]
    public void BuildArguments_QuotesMetadata_AndDoesNotExposeToken()
    {
        var request = new ResponseCenterLaunchRequest(
            "task-1",
            "review",
            "Main block \"A\"",
            "Organization Block",
            "PLC 1",
            "Demo Project",
            "tia-correlation",
            "pending",
            "http://127.0.0.1:43119");

        var arguments = ResponseCenterLauncher.BuildArguments(request);

        arguments.Should().Contain("--task-id \"task-1\"");
        arguments.Should().Contain("--object-name \"Main block \\\"A\\\"\"");
        arguments.Should().Contain("--bridge-url \"http://127.0.0.1:43119\"");
        arguments.ToLowerInvariant().Should().NotContain("token");
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
        {
            Directory.Delete(_root, recursive: true);
        }
    }
}
