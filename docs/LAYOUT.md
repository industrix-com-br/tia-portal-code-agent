# Installed Filesystem Layout

`TiaAgent.Cli` stores payload and runtime state independently of the Git repository checkout.

## Installation root

Default root:

```text
%LOCALAPPDATA%\TiaAgent\
```

Layout:

```text
%LOCALAPPDATA%\TiaAgent\
├── config.json
├── current.json
├── installations.json
├── versions\
│   └── <product-version>\
│       ├── payload-manifest.json
│       ├── Bridge\
│       ├── AddIn\
│       ├── config\
│       └── notices\
├── logs\
├── runtime\
└── cache\
```

The .NET global tool executable is managed by `dotnet tool` and is not copied into each payload version directory.

## Files

### `config.json`

User configuration for:

- default runtime;
- update channel;
- per-runtime enabled state, executable, mode, server URL, and environment values.

See [RUNTIME.md](RUNTIME.md).

### `current.json`

Pointer to the active payload version:

```json
{
  "schemaVersion": 1,
  "activeVersion": "0.3.0-beta.1",
  "previousVersion": "0.2.0-beta.1",
  "activatedAt": "2026-07-25T20:00:00+00:00",
  "activatedBy": "tia-agent install"
}
```

### `installations.json`

Registry of installed payload versions, installation timestamps, commit metadata, and component hashes.

### `versions\<version>\`

A complete copy of the validated payload embedded in the CLI package or supplied through `--payload-dir`.

### `logs\`

Current log files include:

```text
addin-YYYYMMDD.log
bridge.log
supervisor.log
opencode.log        # when OpenCode server mode is used
```

### `runtime\`

Transient service state, including:

- `runtime.json` discovery manifest;
- process and lock files;
- transient Bridge credentials under `runtime\secrets\`.

The runtime manifest is not proof that a process is still healthy.

### `cache\`

Reserved for downloaded or temporary installation artifacts.

## TIA Portal deployment

The active Add-In artifact is deployed separately to:

```text
%APPDATA%\Siemens\Automation\Portal V21\UserAddIns\
```

When automatic deployment is unavailable, `tia-agent install` reports the unpacked `.addin` path under the version directory.

## NuGet package payload

The `TiaAgent.Cli` package embeds:

```text
tools/net8.0/any/payload/
├── payload-manifest.json
├── Bridge\
├── AddIn\
├── config\
└── notices\
```

Required package entries are validated by `build.ps1` and architecture tests.

`payload-manifest.json` records:

- schema version;
- product version;
- source commit;
- build timestamp;
- V21 compatibility metadata;
- Bridge and Add-In artifact metadata;
- per-file hashes and sizes.

## Persistence behavior

Manifests are written through `ManifestStore.WriteAtomic`. Writes use a temporary file followed by replacement of the destination file. Corrupt manifests are reported by diagnostics or handled by command-specific recovery logic.

## Siemens assembly boundary

The payload must not contain `Siemens.*` runtime assemblies. Those assemblies are supplied by the installed TIA Portal V21 environment and remain subject to Siemens licensing.