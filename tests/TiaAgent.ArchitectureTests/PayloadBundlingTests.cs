using FluentAssertions;
using Xunit;

namespace TiaAgent.ArchitectureTests;

public sealed class PayloadBundlingTests
{
    [Fact]
    public void Cli_package_includes_the_complete_installation_payload()
    {
        var root = FindRepositoryRoot();
        var csprojContent = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.Cli", "TiaAgent.Cli.csproj"));
        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));

        csprojContent.Should().Contain("tools/net8.0/any/payload/");
        csprojContent.Should().Contain("payload\\**\\*");
        csprojContent.Should().Contain("Pack=\"true\"");

        buildScriptContent.Should().Contain("payload-manifest.json");
        buildScriptContent.Should().Contain("Bridge\\TiaAgent.Bridge.dll");
        buildScriptContent.Should().Contain("TiaAgent-$ProductVersion.addin");
        buildScriptContent.Should().Contain("THIRD_PARTY_NOTICES.md");
        buildScriptContent.Should().Contain("Siemens.*.dll");
    }

    [Fact]
    public void Pack_verifies_payload_contents_and_tool_installation()
    {
        var root = FindRepositoryRoot();
        var buildScriptContent = File.ReadAllText(Path.Combine(root, "build.ps1"));

        buildScriptContent.Should().Contain("Test-NuGetPayload");
        buildScriptContent.Should().Contain("Test-NuGetInstall");
        buildScriptContent.Should().Contain("dotnet tool install TiaAgent.Cli");
        buildScriptContent.Should().Contain("TiaAgent.Cli.$ProductVersion.nupkg");
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Directory.Build.props")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate the repository root.");
    }
}
