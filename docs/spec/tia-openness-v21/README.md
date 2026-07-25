# Siemens TIA Portal Openness API V21 — specification index

**Status:** V21 API reference  
**Target:** TIA Portal V21  
**Audience:** developers, code agents, reviewers, and maintainers  
**Source:** Siemens XML API documentation supplied with the V21 engineering assemblies

## Purpose and scope

This directory explains the Siemens TIA Portal V21 engineering API surface and the constraints that apply when this project consumes it.

> [!IMPORTANT]
> These files document API capabilities, including operations that TIA Portal Openness can perform. They are not a list of features supported by TIA Portal Code Agent. The current product workflow supports explanations, reviews, change proposals, and read-oriented context gathering only. Direct project writes, downloads, safety changes, hardware or network changes, and deletion are not implemented product workflows.

The documentation separates two related surfaces:

- **TIA Portal Openness:** external or attached automation through `Siemens.Engineering.*` assemblies;
- **TIA Portal Add-In API:** extensions loaded into TIA Portal through `Siemens.Engineering.AddIn.*` assemblies.

Both surfaces expose the same engineering domain but have different entry points, lifecycles, and security constraints. The supplied XML snapshot contains **30,479 documented members across 13 assemblies**.

## Document map

| File | Purpose |
|---|---|
| [`01-core-object-model.md`](./01-core-object-model.md) | TIA process, projects, Engineering Object Model, services, transactions, and lifecycle |
| [`02-addin-framework.md`](./02-addin-framework.md) | Add-In providers, context menus, typed selection, progress, dialogs, and workflow extensions |
| [`03-step7-plc-and-hardware.md`](./03-step7-plc-and-hardware.md) | Hardware navigation, `PlcSoftware`, blocks, tags, types, external sources, and PLC services |
| [`04-wincc-and-unified.md`](./04-wincc-and-unified.md) | WinCC classic and WinCC Unified object models |
| [`05-safety-and-validation.md`](./05-safety-and-validation.md) | Safety engineering, signatures, settings, compile hooks, and Safety Validation |
| [`06-engineering-operations.md`](./06-engineering-operations.md) | Compile, compare, cross-reference, import/export, CAx, and download operations |
| [`07-teamcenter-and-version-control.md`](./07-teamcenter-and-version-control.md) | Teamcenter Gateway and VCI/Add-In version-control extension points |
| [`08-agent-integration-contracts.md`](./08-agent-integration-contracts.md) | Current product boundary between Add-In snapshots, Bridge tasks, runtimes, and external MCP integration |
| [`09-api-surface-catalog.md`](./09-api-surface-catalog.md) | Generated assembly and namespace inventory |
| [`10-source-manifest.md`](./10-source-manifest.md) | Input files, checksums, and extraction metadata |

## Conceptual model

```text
TIA Portal process
└── TiaPortal
    ├── Projects
    │   └── Project / ProjectBase
    │       ├── Devices
    │       │   └── DeviceItems
    │       │       └── SoftwareContainer
    │       │           ├── PlcSoftware
    │       │           ├── HmiTarget       (WinCC classic)
    │       │           └── HmiSoftware     (WinCC Unified)
    │       ├── DeviceGroups
    │       ├── Subnets
    │       └── ProjectLibrary
    ├── GlobalLibraries
    ├── HardwareCatalog
    └── Engineering services
```

The API is a local .NET object model whose objects represent live TIA Portal engineering entities. A caller navigates compositions, obtains optional services, invokes operations, and disposes session-scoped resources.

## Reading order

1. Read this index.
2. Read `01-core-object-model.md` before implementing API access.
3. Read the domain-specific file for the requested engineering capability.
4. Read `06-engineering-operations.md` before evaluating compile, import, export, or download APIs.
5. Read `08-agent-integration-contracts.md` before connecting an API capability to this product.
6. Confirm that the intended operation is inside the current product boundary before implementing or documenting it.

## Version and compatibility rule

The XML files describe the API surface delivered with the supplied V21 installation. The installed V21 assemblies remain the runtime authority. The implementation must not assume that an API available in another TIA Portal release exists or behaves identically in V21.

When adding a new API call:

1. verify the symbol in the V21 XML catalog;
2. verify the referenced V21 assembly;
3. isolate the call behind an appropriate boundary;
4. add an integration test against a controlled V21 environment;
5. document product or device-specific limitations;
6. update the product documentation only after the workflow is implemented and validated.

## Project rules

- Keep Siemens objects inside the TIA integration boundary.
- Convert Siemens objects to serializable DTOs before returning data to an agent runtime.
- Dispose attached sessions, exclusive-access scopes, transactions, and disposable providers.
- Use read-only operations for the current product workflow.
- Do not expose API capability as product capability without an implemented Add-In and Bridge workflow.
- Treat download, safety changes, and destructive operations as unsupported and high risk.
- Do not cache live `IEngineeringObject` instances across project or session changes.
- Do not identify mutable objects only by display name.