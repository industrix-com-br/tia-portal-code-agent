# Rollback Guide

Rollback activates a payload version that is already installed under `%LOCALAPPDATA%\TiaAgent\versions\`.

> [!CAUTION]
> This project is experimental and is not ready for production use.

## Inspect installed versions

```powershell
tia-agent version --verbose
```

The output marks the active payload and lists the other installed versions. There is no separate `tia-agent versions` command.

## Roll back automatically

```powershell
tia-agent rollback
```

The CLI first uses the `previousVersion` recorded in `current.json`. If that value is unavailable, it selects another installed version.

## Roll back to a specific version

```powershell
tia-agent rollback --version 0.3.0-beta.1
```

The target must already be installed. `--force` bypasses the normal target-directory validation and should be reserved for recovery:

```powershell
tia-agent rollback --version 0.3.0-beta.1 --force
```

## Verify the rollback

```powershell
tia-agent version --verbose
tia-agent doctor
tia-agent stop
tia-agent start
```

Restart TIA Portal so it reloads the restored Add-In artifact.

## Remove old versions

Remove a specific installed payload version:

```powershell
tia-agent uninstall --version 0.3.0-beta.1
```

Remove all installed payload versions:

```powershell
tia-agent uninstall --all
```

Removing the active version causes the CLI to select another installed version when one exists. Removing all versions also removes the active-version pointer.

## When to reinstall instead

Use a forced install when the target version directory is missing or corrupted:

```powershell
tia-agent install --force
```

The CLI package must contain the payload version being reinstalled. See [UPDATING.md](UPDATING.md) for changing the CLI package version and [TROUBLESHOOTING.md](TROUBLESHOOTING.md) for diagnostics.