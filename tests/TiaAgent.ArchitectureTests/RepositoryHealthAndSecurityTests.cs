using System.Xml.Linq;
using FluentAssertions;
using Xunit;

namespace TiaAgent.ArchitectureTests;

public sealed class RepositoryHealthAndSecurityTests
{
    [Fact]
    public void Security_policy_and_authoritative_model_exist()
    {
        var root = FindRepositoryRoot();
        var securityMdPath = Path.Combine(root, "SECURITY.md");
        var securityModelPath = Path.Combine(root, "docs", "spec", "SECURITY_MODEL.md");

        File.Exists(securityMdPath).Should().BeTrue();
        File.Exists(securityModelPath).Should().BeTrue();

        var content = File.ReadAllText(securityMdPath);
        content.Should().Contain("docs/spec/SECURITY_MODEL.md");
        content.Should().Contain("Reporting a Vulnerability");
    }

    [Fact]
    public void Repository_security_ownership_and_private_reporting_are_configured()
    {
        var root = FindRepositoryRoot();
        var codeowners = File.ReadAllText(Path.Combine(root, ".github", "CODEOWNERS"));
        var issueConfig = File.ReadAllText(Path.Combine(root, ".github", "ISSUE_TEMPLATE", "config.yml"));

        codeowners.Should().Contain("/SECURITY.md");
        codeowners.Should().Contain("/.github/");
        issueConfig.Should().Contain("Security Vulnerability Report");
    }

    [Fact]
    public void AddIn_manifest_maintains_least_privilege()
    {
        var root = FindRepositoryRoot();
        var document = XDocument.Load(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));

        document.Descendants().Where(element => element.Name.LocalName == "UnrestrictedAccess").Should().BeEmpty();

        var tiaPermissions = document.Descendants()
            .FirstOrDefault(element => element.Name.LocalName == "TIAPermissions")
            ?.Elements().Select(element => element.Name.LocalName).ToList() ?? new List<string>();
        tiaPermissions.Should().NotBeEmpty();

        var securityPermissions = document.Descendants()
            .FirstOrDefault(element => element.Name.LocalName == "SecurityPermissions")
            ?.Elements().Select(element => element.Name.LocalName).ToList() ?? new List<string>();

        securityPermissions.Should().Contain("System.Security.Permissions.UIPermission");
        securityPermissions.Should().Contain("System.Security.Permissions.FileIOPermission");
        securityPermissions.Should().Contain("System.Security.Permissions.EnvironmentPermission");
        securityPermissions.Should().Contain("System.Security.Permissions.SecurityPermission.UnmanagedCode");
        securityPermissions.Should().Contain("System.Net.WebPermission");
        securityPermissions.Should().NotContain("System.Security.Permissions.SecurityPermission.SkipVerification");
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
