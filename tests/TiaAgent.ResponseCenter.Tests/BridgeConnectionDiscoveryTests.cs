using System;
using System.IO;
using FluentAssertions;
using TiaAgent.ResponseCenter.Services;
using Xunit;

namespace TiaAgent.ResponseCenter.Tests;

public sealed class BridgeConnectionDiscoveryTests : IDisposable
{
    private readonly string _root = Path.Combine(
        Path.GetTempPath(),
        "tia-agent-bridge-discovery-tests",
        Guid.NewGuid().ToString("N"));

    [Fact]
    public void Resolve_ReadsRuntimePortAndToken()
    {
        Directory.CreateDirectory(Path.Combine(_root, "runtime"));
        File.WriteAllText(
            Path.Combine(_root, "runtime", "runtime.json"),
            "{\"bridge\":{\"port\":45231}}");
        File.WriteAllText(Path.Combine(_root, "bridge.token"), " secret-token \r\n");

        var settings = BridgeConnectionDiscovery.Resolve(null, null, _root);

        settings.BridgeUrl.Should().Be("http://127.0.0.1:45231");
        settings.AuthToken.Should().Be("secret-token");
    }

    [Fact]
    public void Resolve_ExplicitValuesTakePrecedence()
    {
        var settings = BridgeConnectionDiscovery.Resolve(
            "http://127.0.0.1:49999/",
            "explicit-token",
            _root);

        settings.BridgeUrl.Should().Be("http://127.0.0.1:49999");
        settings.AuthToken.Should().Be("explicit-token");
    }

    [Fact]
    public void Resolve_UsesDefaultBridgeUrlWhenRuntimeManifestIsMissing()
    {
        var settings = BridgeConnectionDiscovery.Resolve(null, null, _root);

        settings.BridgeUrl.Should().Be("http://127.0.0.1:43119");
        settings.AuthToken.Should().BeNull();
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
        {
            Directory.Delete(_root, recursive: true);
        }
    }
}
