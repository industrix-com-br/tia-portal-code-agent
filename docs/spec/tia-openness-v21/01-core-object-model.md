# TIA Portal V21 core object model

## Scope

This file summarizes the common V21 engineering object model exposed primarily by `Siemens.Engineering.Base`.

It is an API reference, not a statement that TIA Portal Code Agent supports project creation, save, archive, mutation, compile, compare, or download. The current product uses selected-object snapshots and read-oriented external MCP context only.

## Session entry points

`Siemens.Engineering.TiaPortal` represents a TIA Portal session. V21 supports starting a process with `TiaPortalMode` and discovering or attaching to existing processes through APIs such as:

- `TiaPortal.GetProcesses()`;
- `TiaPortal.GetProcess(...)`;
- `TiaPortalProcess.Attach()`.

`TiaPortalProcess` exposes process identity, path, mode, project path, installed software, attached sessions, and attach events. Process selection should use explicit user/session context and project identity rather than process ID alone.

`TiaPortalSession` exposes access level, trust authority, process identity, version, attach time, and utilization state. Trust and access level are part of the integration contract.

The current Add-In is loaded inside TIA Portal and does not start or attach to a separate TIA process as part of its selected-object workflow.

## Project lifecycle API

`TiaPortal.Projects` is a `ProjectComposition`. The V21 API includes project create, open, retrieve, upgrade, save, archive, and close operations.

`ProjectBase` exposes roots such as:

- devices and device groups;
- subnets;
- project library;
- language settings;
- history and product metadata.

These lifecycle operations are not current TIA Portal Code Agent features. Project upgrade, save, archive, and close must never be inferred from a model response or change proposal.

## Engineering Object Model

Most domain objects participate in the Engineering Object Model through `IEngineeringObject`.

The generic interface supports:

- composition discovery;
- attribute discovery and access;
- invocation metadata and invocation;
- creation metadata and creation.

Typed APIs should be preferred when available. Generic string-based invocation, creation, or attribute setting must not be exposed as an unrestricted agent capability.

## Compositions and parent relationships

A composition is the owned collection through which children are enumerated, found, created, or imported. It is not merely a list.

Parent traversal is useful for diagnostics and context, but normal navigation should begin at stable project roots. Reverse traversal can be ambiguous in associated or derived views.

## Engineering services

Objects implementing `IEngineeringServiceProvider` can expose optional services through `GetService<T>()` and service metadata.

Examples include:

- `SoftwareContainer` on a hardware `DeviceItem`;
- compile providers;
- download providers;
- cross-reference providers;
- protection and fingerprint services.

A missing service is a normal capability result. Availability can depend on product installation, license, device family, project state, access level, and trust.

## Hardware-to-software navigation

The standard path is:

```text
ProjectBase.Devices
  -> Device
    -> DeviceItems
      -> DeviceItem.GetService<SoftwareContainer>()
        -> SoftwareContainer.Software
```

The resulting software may be a PLC, WinCC classic, WinCC Unified, or another supported product model. Do not assume every `DeviceItem` contains software.

## Exclusive access and transactions

V21 exposes exclusive-access and transaction APIs for operations that modify project state.

General safety constraints for any future mutation include:

- keep exclusive access short;
- do not call a remote model while holding exclusive access;
- re-read and validate the target immediately before mutation;
- detect stale state;
- commit only after deterministic local validation;
- surface cancellation and partial failure.

These are future design requirements. The current product does not acquire exclusive access or execute project mutations.

## Resource and error handling

- Dispose session-scoped Siemens resources deterministically.
- Keep live Siemens objects inside the TIA integration operation.
- Convert returned data to bounded serializable contracts.
- Treat `EngineeringNotSupportedException` as a capability or version mismatch.
- Avoid long-running synchronous work on the TIA Portal UI thread.
- Preserve technical exception details in protected diagnostics while keeping user errors concise.

## Current project boundary

The current Add-In:

- receives its TIA context from `ProjectTreeAddInProvider` callbacks;
- captures a serializable selection snapshot;
- releases live Siemens objects before Bridge and runtime work;
- does not implement a shared project gateway, transaction runner, or in-repository MCP handler;
- does not create, open, save, archive, close, compile, compare, download, or mutate projects.

Before documenting a new object-model capability as supported, verify it against the installed V21 assemblies, implement the complete Add-In and Bridge workflow, and validate it in a controlled V21 environment.