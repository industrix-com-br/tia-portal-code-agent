---
title: TIA Portal V21 Add-In Technical Specification
document_type: technical-reference
status: current-implementation
audience:
  - contributors
  - maintainers
  - reviewers
language: en-US
---

# TIA Portal V21 Add-In Technical Specification

This document describes the Add-In implemented by `src/TiaAgent.AddIn` and its current packaging process.

## Scope

The product currently targets only:

- Siemens TIA Portal V21;
- the V21 modular Add-In and Openness assemblies;
- Windows x64;
- .NET Framework 4.8 for the Add-In host.

No compatibility claim is made for V18, V19, or V20.

## Project and framework

`TiaAgent.AddIn.csproj` is an SDK-style project targeting `net48`. The CLI and Bridge are separate .NET 8 processes and must not be used as a reason to retarget the Add-In.

The Add-In references Siemens assemblies from the installed TIA Portal V21 Public API. Project references must use `Private=false` so Siemens binaries are not copied into build or package output.

The active paths are rooted at:

```text
C:\Program Files\Siemens\Automation\Portal V21\PublicAPI\V21\
```

`Directory.Build.props` defaults `TiaPublicApiDir` to the `net48` directory. `build.ps1` detects both the Openness assemblies and the Add-In assemblies installed with V21.

Do not use the removed monolithic `Siemens.Engineering.AddIn.dll` or the historical `PublicAPI\V21.AddIn` path.

## Provider

`ProjectTreeProvider` derives from `ProjectTreeAddInProvider` and registers one context menu named **AI Code Agent**.

Actions:

- **Explain selected object**;
- **Review selected object**;
- **Propose change**.

The provider captures the current selection, creates a serializable snapshot, and submits a task to the Bridge on a background task. Long-running Bridge work must not block the TIA UI thread.

Live `IEngineeringObject` instances must remain local to the operation that resolves them. They must not be stored in fields, caches, DTOs, or cross-process contracts.

## Result UI

The shipped Add-In contains a WPF response window implemented under `src/TiaAgent.AddIn/Ui/`, including XAML and the Markdown-to-FlowDocument renderer.

`AssistantPanelFactory` uses a WPF-first flow and falls back to a MessageBox when the WPF window cannot be created. UI work is dispatched to the appropriate STA/WPF dispatcher.

Add-In logs are written best-effort to:

```text
%LOCALAPPDATA%\TiaAgent\logs\addin-YYYYMMDD.log
```

A logging failure must never prevent Add-In loading.

## `Config.xml`

The manifest uses the V21 publisher namespace and declares:

- product name and stable product ID;
- `TiaAgent.AddIn.dll` as the feature assembly;
- `TiaAgent.Contracts.dll` as an additional assembly;
- the package version placeholder replaced during packaging;
- TIA and .NET sandbox permissions required by the current implementation.

The current manifest requests `TIA.ReadWrite`, plus UI, file, environment, unmanaged-code, and loopback web permissions. The product workflow remains read-only despite the broader TIA permission. Reducing the manifest permission requires code and host validation and is not a documentation-only change.

Do not describe direct project writes as supported until an approval and apply workflow exists in the Add-In and is validated end-to-end.

## Packaging

The repository build entry point is `build.ps1`:

```powershell
.\build.ps1 build
.\build.ps1 test
.\build.ps1 pack
.\build.ps1 install-dev
```

The `PackAddIn` and `VerifyAddIn` MSBuild targets are defined by `PackageAddIn.targets`. Packaging uses the Siemens Add-In Publisher available in the V21 installation and the repository `OpcSigner` tool.

Development output:

```text
artifacts\TiaAgent-0.0.0-dev.addin
```

Release output:

```text
artifacts\TiaAgent-<product-version>.addin
```

The internal Siemens manifest version is numeric. The artifact name retains the full product version, including prerelease suffixes.

## Deployment

The normal user deployment directory is:

```text
%APPDATA%\Siemens\Automation\Portal V21\UserAddIns\
```

`tia-agent install` deploys the packaged Add-In when TIA Portal and the UserAddIns directory can be resolved. When deployment is not possible, it preserves the unpacked Add-In and reports its location for manual installation.

TIA Portal must be restarted after deploying a different `.addin` artifact.

## Build and release requirements

A valid release must:

- build on the V21 Windows release runner;
- produce a valid and verified `.addin` package;
- exclude Siemens assemblies;
- include `TiaAgent.Contracts.dll`;
- use the same product version as the CLI and Bridge;
- be embedded in the `TiaAgent.Cli` NuGet payload;
- pass payload-content and local tool-installation checks.

## Troubleshooting sources

Use:

- `docs/TROUBLESHOOTING.md` for user diagnostics;
- the dated Add-In log for load, permission, UI, and Bridge failures;
- `PackageAddIn.targets` and `build.ps1` for packaging behavior;
- `src/TiaAgent.AddIn/Config.xml` for the exact current permission set;
- `docs/spec/tia-openness-v21/02-addin-framework.md` for background reference on the V21 framework.

Historical V18–V20 conversion guidance and scripts are not part of the current repository workflow.