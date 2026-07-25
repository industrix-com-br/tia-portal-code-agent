# Update Guide

The CLI package and its installed payload are updated separately.

> [!CAUTION]
> This project is experimental and is not ready for production use.

## Inspect the current installation

```powershell
tia-agent version
tia-agent version --verbose
tia-agent channel
```

`version --verbose` shows the CLI product version, active payload version, configuration path, and installed payload versions. There is no separate `tia-agent versions` command.

## Update the CLI package

> [!IMPORTANT]
> The project currently publishes only prerelease CLI packages. Without `--prerelease` or an explicit `--version`, `dotnet tool update` searches for a stable version and can report that the package was not found or that no applicable update exists.

Update to the latest prerelease:

```powershell
dotnet tool update --global TiaAgent.Cli --prerelease
```

Update to a specific prerelease version:

```powershell
dotnet tool update --global TiaAgent.Cli --version 0.3.0-beta.5
```

After a stable release is published, update to the latest stable version with:

```powershell
dotnet tool update --global TiaAgent.Cli
```

## Install and activate the new payload

After updating the global tool, install the payload bundled inside that CLI package:

```powershell
tia-agent update
```

The command validates the bundled payload, installs it side-by-side under `%LOCALAPPDATA%\TiaAgent\versions\`, activates it, and redeploys the Add-In.

For a development or diagnostic payload directory:

```powershell
tia-agent update --version 0.3.0-beta.5 --payload-dir C:\path\to\payload
```

## Update channel

```powershell
tia-agent channel show
tia-agent channel set stable
tia-agent channel set rc
tia-agent channel set beta
tia-agent channel set alpha
```

The channel is validated by the CLI during payload update and activation. It does not cause the .NET SDK to include prerelease NuGet packages. Use `--prerelease` or `--version` when updating the CLI package. Use `--force` only when intentionally crossing a payload channel restriction.

## Verify the update

Restart TIA Portal so it reloads the deployed `.addin`, then run:

```powershell
tia-agent doctor
tia-agent status
tia-agent version --verbose
```

If runtime services were already running:

```powershell
tia-agent stop
tia-agent start
```

## Recovery

When the new payload is already installed but should not remain active, use [ROLLBACK.md](ROLLBACK.md). When package extraction or validation fails, use [TROUBLESHOOTING.md](TROUBLESHOOTING.md).
