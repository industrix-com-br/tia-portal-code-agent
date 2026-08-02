# Code scanning stale-alert dismissal report

- Main commit verified: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Alerts dismissed in this pass: **76**
- Generated build-output alerts dismissed: **16**
- Removed-source alerts dismissed: **60**
- Failures: **0**
- Open alerts remaining: **635**

## Dismissed alerts

| Alert | Severity | Rule | Classification | Location |
|---:|---|---|---|---|
| #738 | note | `cs/nested-if-statements` | generated build artifact | `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:100` |
| #676 | warning | `cs/useless-assignment-to-local` | generated build artifact | `src/TiaAgent.ResponseCenter/obj/Release/net8.0-windows/Views/AgentResponseWindow.g.cs:81` |
| #675 | warning | `cs/useless-assignment-to-local` | generated build artifact | `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:239` |
| #491 | note | `cs/linq/missed-where` | removed source | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:174` |
| #490 | note | `cs/linq/missed-select` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:196` |
| #489 | note | `cs/catch-of-all-exceptions` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25` |
| #488 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:142` |
| #487 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:104` |
| #486 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:41` |
| #485 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:72` |
| #484 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:148` |
| #483 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:147` |
| #482 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:128` |
| #481 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:127` |
| #480 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:117` |
| #479 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:113` |
| #478 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:97` |
| #477 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:58` |
| #476 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:57` |
| #475 | note | `cs/path-combine` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:17` |
| #474 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/SbomGenerator.cs:142` |
| #473 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:163` |
| #472 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:128` |
| #471 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:111` |
| #470 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:87` |
| #469 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:80` |
| #468 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:29` |
| #464 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:172` |
| #463 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:171` |
| #462 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:168` |
| #461 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:162` |
| #460 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Commands/VerifyReleaseCommand.cs:43` |
| #459 | note | `cs/path-combine` | removed source | `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:33` |
| #458 | note | `cs/empty-catch-block` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25` |
| #457 | warning | `cs/local-not-disposed` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:151` |
| #456 | warning | `cs/local-not-disposed` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:150` |
| #455 | warning | `cs/local-not-disposed` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:132` |
| #454 | warning | `cs/local-not-disposed` | removed source | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:131` |
| #273 | note | `cs/useless-tostring-call` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` |
| #272 | note | `cs/useless-tostring-call` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` |
| #271 | note | `cs/useless-tostring-call` | removed source | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:77` |
| #270 | note | `cs/useless-tostring-call` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` |
| #269 | note | `cs/useless-tostring-call` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` |
| #268 | note | `cs/linq/missed-where` | removed source | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:378` |
| #267 | note | `cs/linq/missed-where` | removed source | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:119` |
| #255 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88` |
| #254 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81` |
| #253 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86` |
| #252 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37` |
| #251 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86` |
| #250 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88` |
| #249 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81` |
| #248 | note | `cs/missed-ternary-operator` | generated build artifact | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37` |
| #245 | note | `cs/missed-readonly-modifier` | removed source | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:357` |
| #244 | note | `cs/missed-readonly-modifier` | removed source | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:356` |
| #234 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230` |
| #233 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:212` |
| #232 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:190` |
| #231 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:161` |
| #230 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:150` |
| #229 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:118` |
| #228 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:69` |
| #227 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:104` |
| #181 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:176` |
| #180 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:35` |
| #178 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:162` |
| #177 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:87` |
| #176 | note | `cs/catch-of-all-exceptions` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:46` |
| #171 | warning | `cs/useless-assignment-to-local` | generated build artifact | `src/TiaAgent.AddIn/obj/Release/net48/Ui/AssistantPanel.g.cs:55` |
| #49 | note | `cs/path-combine` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:155` |
| #48 | note | `cs/path-combine` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:64` |
| #47 | note | `cs/path-combine` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:16` |
| #46 | note | `cs/path-combine` | removed source | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:12` |
| #38 | note | `cs/empty-catch-block` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230` |
| #21 | warning | `cs/constant-condition` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:155` |
| #20 | warning | `cs/constant-condition` | removed source | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:131` |
