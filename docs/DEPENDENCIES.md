# Dependency Management Policy

This document records the dependency and SDK configuration implemented by the repository.

## .NET SDK

`global.json` pins the baseline SDK:

```json
{
  "sdk": {
    "version": "8.0.400",
    "rollForward": "latestFeature",
    "allowPrerelease": false
  }
}
```

The CLI and Bridge target .NET 8. The TIA Portal Add-In targets .NET Framework 4.8 and uses the reference-assemblies package during builds.

## Central package management

`Directory.Packages.props` is the source of truth for NuGet package versions. Project files must reference packages without local `Version` attributes.

Current centrally managed packages:

| Package | Version |
|---|---|
| `FluentAssertions` | `8.10.0` |
| `Microsoft.CodeAnalysis.CSharp` | `4.11.0` |
| `Microsoft.NET.Test.Sdk` | `18.8.1` |
| `Microsoft.NETFramework.ReferenceAssemblies` | `1.0.3` |
| `Moq` | `4.20.72` |
| `PolySharp` | `1.15.0` |
| `xunit` | `2.9.3` |
| `xunit.runner.visualstudio` | `3.1.5` |

Transitive dependency pinning is enabled through `CentralPackageTransitivePinningEnabled`.

## Locked restore

`Directory.Build.props` enables package lock files for all projects:

```xml
<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>
<RestoreLockedMode Condition="'$(CI)' == 'true'">true</RestoreLockedMode>
```

Local restore may update `packages.lock.json`. CI fails when committed lock files are missing or inconsistent.

## Dependabot

`.github/dependabot.yml` monitors NuGet and GitHub Actions dependencies. Changes produced by Dependabot must pass the normal pull-request validation.

## Adding or upgrading a package

1. Add or update the package version in `Directory.Packages.props`.
2. Add the versionless `PackageReference` to the required project.
3. Run `dotnet restore TiaAgent.sln`.
4. Review and commit the changed lock files.
5. Run `.\build.ps1 test`.

## External runtime dependencies

Mimo CLI, OpenCode, Claude Code CLI, TiaMcpServer, TIA Portal, and Siemens Public API assemblies are external prerequisites. They are not NuGet dependencies bundled by this repository.

Do not document a minimum external-tool version unless the repository enforces or tests that version.