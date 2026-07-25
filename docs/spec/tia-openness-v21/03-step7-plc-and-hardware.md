# TIA Portal V21 Step7, PLC, and hardware API

## Scope

This file summarizes the V21 Step7 and hardware API surface relevant to code navigation and review. It is not a list of tools or write operations supported by TIA Portal Code Agent.

The current product may capture selected PLC object metadata and source content and may use the external `TiaMcpServer` for read-oriented context. It does not implement block import, creation, deletion, compile, online compare, or download workflows.

## Assemblies and namespaces

The PLC domain is primarily exposed by:

- `Siemens.Engineering.Base` for projects, hardware, devices, services, compare, and download infrastructure;
- `Siemens.Engineering.Step7` for PLC software, blocks, tags, types, sources, OPC UA, alarms, technology objects, and PLC-specific download options.

Important namespace families include:

- `Siemens.Engineering.HW`;
- `Siemens.Engineering.HW.Features`;
- `Siemens.Engineering.SW`;
- `Siemens.Engineering.SW.Blocks`;
- `Siemens.Engineering.SW.Tags`;
- `Siemens.Engineering.SW.Types`;
- `Siemens.Engineering.SW.ExternalSources`;
- `Siemens.Engineering.SW.WatchAndForceTables`;
- `Siemens.Engineering.SW.TechnologicalObjects`;
- `Siemens.Engineering.SW.OpcUa`.

## Project-to-PLC navigation

```text
ProjectBase.Devices
  -> Device.DeviceItems
    -> DeviceItem.GetService<SoftwareContainer>()
      -> SoftwareContainer.Software as PlcSoftware
```

A hardware tree contains racks, interfaces, modules, and software-bearing items. Do not assume the first `DeviceItem` is the CPU; locate the `SoftwareContainer` capability and validate the returned software type.

## `PlcSoftware` root

`Siemens.Engineering.SW.PlcSoftware` exposes:

- `BlockGroup`;
- `ExternalSourceGroup`;
- `TagTableGroup`;
- `TypeGroup`;
- `WatchAndForceTableGroup`;
- `TechnologicalObjectGroup`;
- PLC alarm text lists;
- compare, update, and service-discovery APIs.

For read-oriented integration, treat `PlcSoftware` as the PLC aggregate root and return bounded, serializable summaries rather than live Siemens objects.

## Block hierarchy

```text
PlcSoftware.BlockGroup
  -> PlcBlockGroup
    ├── Blocks: PlcBlockComposition
    └── Groups: PlcBlockUserGroupComposition
```

`PlcBlockComposition` supports enumeration, lookup, imports, and typed creation APIs. `PlcBlock` exposes metadata such as name, number, programming language, consistency state, timestamps, memory information, protection state, multilingual text, and header data.

Block operations in the V21 API include export, document export, editor navigation, import through a composition, and deletion. Their presence in the API does not make them supported product actions.

Current safe read strategy:

1. identify the PLC and block from the selected context or external MCP response;
2. enumerate only the required group scope;
3. export or read source only through a verified supported path;
4. parse outside the live Siemens object graph;
5. bound returned content and identify truncation;
6. avoid logging complete source payloads.

## Tags and constants

The tag domain includes:

- `PlcTagTableGroup`;
- `PlcTagTable`;
- `PlcTag`;
- `PlcConstant`;
- `PlcSystemConstant`;
- `PlcUserConstant`.

Distinguish symbolic tags, user constants, system constants, addresses, data types, and owning tables. Do not infer program semantics from names alone.

## PLC types

`PlcSoftware.TypeGroup` exposes PLC types and nested groups. Type changes can alter memory layout and downstream data-block behavior; they are outside the current product boundary.

## External sources and documents

The Step7 API includes external-source groups, PLC documents, document import/export result types, and explicit import options. Availability varies by language and object type.

The current product must report unsupported extraction rather than claiming all PLC languages can be exported or parsed.

## Compile, compare, and cross-reference

V21 exposes compile services and result hierarchies, offline/online compare APIs, and cross-reference services.

These capabilities can inform future validation or impact-analysis workflows, but the current product does not expose compile or online compare actions. Read-oriented reference gathering must return explicit capability and truncation status.

## Other Step7 domains

The V21 surface also includes:

- watch and force tables;
- alarms and supervision;
- technology and motion objects;
- OPC UA configuration and access control;
- units and named-value documents;
- simulation and virtual PLC settings;
- PLC-specific download configurations.

These domains must not be exposed through generic reflection or mutation commands. Watch/force, protection, security, online, and download operations remain unsupported product behavior.

## Future mutation requirements

Any future block, tag, type, or source mutation would require a separate product design with preview, explicit approval, stale-state validation, exclusive access where supported, recovery, compile or consistency validation, and audit evidence.

This section records safety requirements only. It does not describe implemented commands.

## Validation sources

Before using a Step7 symbol:

- verify it in the supplied V21 XML catalog and installed assembly;
- confirm the object type and product license in a controlled V21 project;
- add an integration test for supported extraction behavior;
- update the product documentation only after the workflow is implemented end-to-end.