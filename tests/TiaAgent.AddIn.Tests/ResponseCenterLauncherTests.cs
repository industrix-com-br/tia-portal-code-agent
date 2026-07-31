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
    public void ParseActiveVersion_ReturnsNull_WhenEmpty()
    {
        ResponseCenterLauncher.ParseActiveVersion("").Should().BeNull();
        ResponseCenterLauncher.ParseActiveVersion(null!).Should().BeNull();
    }

    [Fact]
    public void ParseActiveVersion_ReturnsNull_WhenMissing()
    {
        ResponseCenterLauncher.ParseActiveVersion("{\"schemaVersion\":1}").Should().BeNull();
    }

    [Fact]
    public void ResolveExecutablePath_Throws_WhenManifestMissing()
    {
        var nonExistent = Path.Combine(_root, "no-such-dir");
        Action act = () => ResponseCenterLauncher.ResolveExecutablePath(nonExistent);
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void ResolveExecutablePath_Throws_WhenActiveVersionMissing()
    {
        Directory.CreateDirectory(_root);
        File.WriteAllText(
            Path.Combine(_root, "current.json"),
            "{\"schemaVersion\":1}");

        Action act = () => ResponseCenterLauncher.ResolveExecutablePath(_root);
        act.Should().Throw<InvalidDataException>();
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
        {
            Directory.Delete(_root, recursive: true);
        }
    }
}
