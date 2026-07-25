# TIA Portal V21 WinCC and WinCC Unified API

## Scope

This file summarizes the V21 HMI API families for future navigation and review work. It does not describe HMI tools currently exposed by TIA Portal Code Agent.

The current product can analyze a supported selected object when snapshot extraction or the external MCP integration provides the required context. It does not implement HMI import, mutation, validation, deletion, connection changes, or download workflows.

## Two HMI object models

TIA Portal V21 exposes distinct HMI API families:

| Product family | Assembly | Root software type |
|---|---|---|
| WinCC classic and panel-oriented engineering | `Siemens.Engineering.WinCC` | `Siemens.Engineering.Hmi.HmiTarget` |
| WinCC Unified | `Siemens.Engineering.WinCCUnified` | `Siemens.Engineering.HmiUnified.HmiSoftware` |

The models are not interchangeable. An integration must branch on the runtime type returned by `SoftwareContainer.Software`, not on the device display name.

## Discovery path

```text
ProjectBase.Devices
  -> DeviceItem
    -> GetService<SoftwareContainer>()
      -> Software
        -> HmiTarget or HmiSoftware
```

Any serialized result should include an explicit family discriminator such as `classic` or `unified`.

## WinCC classic

`Siemens.Engineering.Hmi.HmiTarget` exposes areas including:

- connections and cycles;
- graphic and text lists;
- screens, templates, popups, and slide-ins;
- tags;
- VBScript;
- alarms, logging, reports, recipes, scheduling, globalization, themes, and dynamics.

Typed compositions and folders should be used instead of manually constructed object paths. Connection, alarm, script, and runtime-related data require stricter review than descriptive metadata.

## WinCC Unified

`Siemens.Engineering.HmiUnified.HmiSoftware` exposes areas including:

- screens and screen groups;
- tags, system tags, tables, and table groups;
- connections;
- alarm classes and alarms;
- alarm logs, data logs, and audit trails;
- scripts;
- text, system-text, and graphic lists;
- OPC UA alarm types;
- plant-object tags;
- runtime settings.

The Unified UI model also contains shapes, controls, widgets, faceplates, dynamization, event handlers, trend parts, and plant views.

## Validation and import/export

Unified common objects expose validation patterns through `HmiBase`, `IValidator`, and `HmiValidationResult`. Classic and Unified import/export formats differ and must never be routed through one untyped operation.

These APIs are reference capabilities only. The current product does not expose Unified validation or HMI import/export commands.

## Efficient extraction

Screen object graphs can be large. Read-oriented integrations should return layered, bounded data:

1. target and screen metadata;
2. top-level hierarchy;
3. selected properties;
4. referenced tags, scripts, events, and dynamization;
5. detailed object properties only when explicitly requested.

Do not serialize every property of every screen object by default. Include truncation and capability status, and do not log full screen or script payloads unnecessarily.

## Risk boundary

Current product support is limited to analysis of available read context.

The following are not supported product workflows:

- changing screen text, objects, bindings, or dynamization;
- changing tags, alarms, connections, or runtime settings;
- deleting HMI objects;
- importing or exporting as a user-facing product action;
- validating or compiling an HMI target;
- downloading to an HMI device.

Any future HMI write workflow would need family-specific contracts, deterministic preview, explicit approval, stale-state validation, recovery, validation or compile evidence, and audit records.

## Validation sources

Before using an HMI symbol:

- verify it in the supplied V21 XML catalog and installed assembly;
- confirm whether the target is Classic or Unified;
- validate availability with a representative licensed V21 project;
- bound object-graph extraction in tests;
- update product documentation only after the workflow is implemented and validated.