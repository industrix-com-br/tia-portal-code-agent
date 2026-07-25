# Compatibility Policy

Product version and Siemens TIA Portal compatibility are separate dimensions.

```text
Product version: TIA Portal Code Agent release, for example 0.3.0-beta.1
TIA compatibility: validated Siemens environment, for example TIA Portal V21 / Openness V21
```

The SemVer product version must not encode the TIA Portal version.

## Current baseline

| Dimension | Baseline |
|---|---|
| TIA Portal | V21 |
| Openness | V21 Public API |
| Add-In host | TIA Portal V21, .NET Framework 4.8, x64 |
| Bridge and CLI | .NET 8, Windows x64 |
| Operating system | Windows versions supported by the validated V21 installation |

This baseline does not prove compatibility with every V21 edition, update, language pack, hardware catalog, license combination, or project type.

## Release compatibility evidence

Each public release should record:

- product version and channel;
- exact TIA Portal V21 update/build;
- Openness and Add-In Publisher assembly versions;
- Windows version;
- installation mode;
- TiaMcpServer version;
- tested coding-agent runtime and version;
- tested object and language coverage;
- known limitations;
- validation status.

Use:

- `supported` for a repeatably validated combination;
- `experimental` for an incomplete validation boundary;
- `unsupported` for intentionally blocked, known-incompatible, or untested behavior that must not be implied as available.

The repository does not currently generate this matrix automatically. Missing evidence is tracked in [KNOWN_UNKNOWNS.md](spec/KNOWN_UNKNOWNS.md).

## First-party component compatibility

The CLI, Bridge, Add-In, contracts, application library, and installation payload from one product release share one product version and form the first-party compatibility unit.

- Mixing first-party artifacts from different product versions is unsupported unless explicitly validated.
- Protocol and manifest schema versions may evolve independently, but each product release must declare the revisions it implements.
- Diagnostics should report product version, component version, protocol or schema revision, TIA Portal version, and Openness version separately.

`TiaMcpServer`, Mimo, OpenCode, and Claude Code are external dependencies. They do not share the product version and require a separately tested compatibility record.

## TIA Portal support changes

Adding another TIA Portal major version is a compatibility feature and normally requires a MINOR release. Removing a supported major version is a breaking compatibility change.

A newer TIA Portal release is not supported merely because assemblies load or a basic action succeeds. It requires build, package, installation, selection, UI, runtime, and source-extraction validation.

## Update levels

Compatibility should be recorded against an exact TIA Portal update level when Siemens updates can affect Add-In loading, permissions, packaging, or Openness behavior. When only V21 is stated, documentation must describe the result as a baseline rather than a complete support matrix.

## Project-feature compatibility

Compatibility with the V21 host does not imply compatibility with every project feature. Release evidence should identify limitations involving:

- supported selection types and PLC languages;
- protected or know-how-protected blocks;
- WinCC and WinCC Unified objects;
- Safety projects;
- multiuser, VCI, or Teamcenter projects;
- project upgrades;
- hardware catalogs and licensed option packages.

Direct writes, downloads, Safety changes, hardware/network changes, and other operations outside the current product workflow remain unsupported even if the underlying Siemens API exposes them.

## Upgrade compatibility

A supported product upgrade requires:

1. product-version compatibility as defined in [RELEASING.md](RELEASING.md); and
2. compatibility with the installed TIA Portal and Openness environment.

Changing the product version and TIA Portal major version in one migration is higher risk and must be validated as a separate scenario.

## Claim rule

Compatibility claims must distinguish:

- behavior validated by this repository;
- API capability inherited from Siemens documentation;
- experimental observations;
- unsupported assumptions.

Do not convert API availability or an upstream dependency claim into a supported product claim without repeatable end-to-end evidence.