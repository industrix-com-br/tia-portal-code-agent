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

Latest stable release:

```powershell
dotnet tool update --global TiaAgent.Cli
```

Latest prerelease:

```powershell
dotnet tool update --global TiaAgent.Cli --prerelease
```

Specific version:

```powershell
dotnet tool update --global TiaAgent.Cli --version 0.3.0-beta.1
```

## Install and activate the new payload

After updating the global tool, install the payload bundled inside that CLI package:

```powershell
tia-agent update
```

The command validates the bundled payload, installs it side-by-side under `%LOCALAPPDATA%\TiaAgent\versions\`, activates it, and redeploys the Add-In.

For a development or diagnostic payload directory:

```powershell
tia-agent update --version 0.3.0-beta.1 --payload-dir C:\path\to\payload
```

## Update channel

```powershell
tia-agent channel show
tia-agent channel set stable
tia-agent channel set rc
tia-agent channel set beta
tia-agent channel set alpha
```

The channel is validated by the CLI during update and activation. Use `--force` only when intentionally crossing a channel restriction.

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