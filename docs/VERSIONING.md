# Versioning

TIA Portal Code Agent is one product. The CLI, Bridge, Add-In, Contracts, Application, installation payload, diagnostics, and NuGet package always use the same product version.

## Public source of truth

The Git tag is the only public source of truth:

```text
v0.3.0-beta.1
v0.3.0-rc.1
v0.3.0
```

The tag includes `v`; the internal version does not. `build.ps1` extracts or receives the version and passes it to MSBuild through `Version`.

Local builds without a release tag use:

```text
0.0.0-dev
```

No `VERSION`, `version.json`, or equivalent product-version file is used.

## Supported format

```text
MAJOR.MINOR.PATCH
MAJOR.MINOR.PATCH-alpha.N
MAJOR.MINOR.PATCH-beta.N
MAJOR.MINOR.PATCH-rc.N
```

A version is never reused. A failed publication is corrected with a new tag and version.

## .NET version properties

`Directory.Build.props` derives all .NET version properties from `Version`:

- `PackageVersion` and `ProductVersion`: complete product version;
- `AssemblyVersion` and `FileVersion`: numeric `MAJOR.MINOR.PATCH.0`;
- `InformationalVersion`: complete product version plus a short commit SHA when available.

Example diagnostic version:

```text
0.3.0-beta.1+sha.abcdef0
```

The commit metadata does not change the NuGet package identity.

## Siemens Add-In version

The Siemens publisher requires a numeric manifest version. Packaging therefore separates:

```text
ProductVersion       = 0.3.0-beta.1
AddInManifestVersion = 0.3.0
ArtifactVersion      = 0.3.0-beta.1
```

The internal Siemens manifest receives `0.3.0`, while the artifact keeps the complete product version:

```text
TiaAgent-0.3.0-beta.1.addin
```

This prevents beta, RC, and stable artifacts from overwriting one another.

## Technical schema versions

Protocol and schema versions remain independent when compatibility requires it, for example:

```text
productVersion: 0.3.0-beta.1
schemaVersion: 1
bridgeApiVersion: 1
configSchemaVersion: 1
```

Technical schema versions do not replace or compete with the product version.
