using System.Text.RegularExpressions;
using System.Xml.Linq;
using FluentAssertions;
using Xunit;

namespace TiaAgent.ArchitectureTests;

public sealed class ProductVersionConsistencyTests
{
    private static readonly Regex ProductVersionLiteral = new(
        @"(?<![A-Za-z0-9])\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+)?(?![A-Za-z0-9])",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [Fact]
    public void DirectoryBuildProps_defines_one_product_version_and_derives_all_dotnet_versions()
    {
        var root = FindRepositoryRoot();
        var document = XDocument.Load(Path.Combine(root, "Directory.Build.props"));
        var versions = document.Descendants("Version").ToArray();

        versions.Should().ContainSingle();
        versions[0].Value.Should().Be("0.0.0-dev");
        versions[0].Attribute("Condition")?.Value.Should().Contain("$(Version)");

        document.Descendants("PackageVersion").Single().Value.Should().Be("$(Version)");
        document.Descendants("ProductVersion").Single().Value.Should().Be("$(Version)");
        document.Descendants("AssemblyVersion").Single().Value.Should().Be("$(VersionCore).0");
        document.Descendants("FileVersion").Single().Value.Should().Be("$(VersionCore).0");
        document.Descendants("InformationalVersion").Should().Contain(element => element.Value.Contains("$(Version)", StringComparison.Ordinal));
        document.Descendants("IncludeSourceRevisionInInformationalVersion").Single().Value.Should().Be("false");
    }

    [Fact]
    public void Project_files_do_not_override_the_product_version()
    {
        var root = FindRepositoryRoot();
        var projectFiles = Directory.EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories)
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase));

        foreach (var projectFile in projectFiles)
        {
            var document = XDocument.Load(projectFile);
            document.Descendants("Version").Should().BeEmpty($"{projectFile} must inherit Version from Directory.Build.props");
            document.Descendants("PackageVersion").Should().BeEmpty($"{projectFile} must inherit PackageVersion from Directory.Build.props");
            document.Descendants("ProductVersion").Should().BeEmpty($"{projectFile} must inherit ProductVersion from Directory.Build.props");
            document.Descendants("AssemblyVersion").Should().BeEmpty($"{projectFile} must inherit AssemblyVersion from Directory.Build.props");
            document.Descendants("FileVersion").Should().BeEmpty($"{projectFile} must inherit FileVersion from Directory.Build.props");
            document.Descendants("InformationalVersion").Should().BeEmpty($"{projectFile} must inherit InformationalVersion from Directory.Build.props");
        }
    }

    [Fact]
    public void Release_sources_do_not_contain_fixed_zero_assembly_versions()
    {
        var root = FindRepositoryRoot();
        var sourceFiles = Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
            .Where(path => path.EndsWith(".props", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".targets", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));

        foreach (var sourceFile in sourceFiles)
        {
            File.ReadAllText(sourceFile).Should().NotContain("<AssemblyVersion>0.0.0.0</AssemblyVersion>");
            File.ReadAllText(sourceFile).Should().NotContain("<FileVersion>0.0.0.0</FileVersion>");
        }
    }

    [Fact]
    public void Siemens_manifest_version_is_numeric_while_artifact_version_preserves_prerelease()
    {
        var root = FindRepositoryRoot();
        var config = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "Config.xml"));
        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));

        config.Should().Contain("<Version>__ADDIN_MANIFEST_VERSION__</Version>");
        ProductVersionLiteral.IsMatch(config).Should().BeFalse();

        targets.Should().Contain("<AddInManifestVersion>");
        targets.Should().Contain("<ArtifactVersion>$(Version)</ArtifactVersion>");
        targets.Should().Contain("Replace('__ADDIN_MANIFEST_VERSION__', '$(AddInManifestVersion)')");
        targets.Should().Contain("TiaAgent-$(ArtifactVersion).addin");
        targets.Should().NotContain("TiaAgent-$(AddInManifestVersion).addin");
    }

    [Fact]
    public void Beta_rc_and_stable_have_distinct_AddIn_artifact_names()
    {
        var artifactNames = new[]
        {
            "TiaAgent-0.3.0-beta.1.addin",
            "TiaAgent-0.3.0-rc.1.addin",
            "TiaAgent-0.3.0.addin"
        };

        artifactNames.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void PackAddIn_is_atomic_and_never_deploys_to_AppData()
    {
        var root = FindRepositoryRoot();
        var targets = File.ReadAllText(Path.Combine(root, "src", "TiaAgent.AddIn", "PackageAddIn.targets"));
        var packStart = targets.IndexOf("<Target Name=\"PackAddIn\">", StringComparison.Ordinal);
        var packEnd = targets.IndexOf("</Target>", packStart, StringComparison.Ordinal);
        var packTarget = targets.Substring(packStart, packEnd - packStart);

        packTarget.Should().NotContain("AddInDeployDir");
        packTarget.Should().NotContain("APPDATA");
        packTarget.Should().Contain("AddInTempPackagePath");
        packTarget.Should().Contain("Move-Item");
        targets.Should().Contain(".addin.tmp");
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
