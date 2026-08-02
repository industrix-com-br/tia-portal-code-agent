# Code scanning classification

- Main commit: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Open alerts: **711**
- Alerts whose referenced path is absent from current main: **76**
- Alerts whose most recent instance is not on main: **0**

## Counts by severity

| Severity | Count |
|---|---:|
| error | 1 |
| high | 1 |
| note | 660 |
| warning | 49 |

## Counts by rule

| Rule | Count |
|---|---:|
| `cs/path-combine` | 352 |
| `cs/catch-of-all-exceptions` | 180 |
| `cs/empty-catch-block` | 72 |
| `cs/local-not-disposed` | 24 |
| `cs/missed-ternary-operator` | 13 |
| `cs/dispose-not-called-on-throw` | 12 |
| `cs/linq/missed-where` | 11 |
| `cs/linq/missed-select` | 10 |
| `cs/useless-assignment-to-local` | 9 |
| `cs/missed-using-statement` | 8 |
| `cs/useless-tostring-call` | 5 |
| `cs/missed-readonly-modifier` | 3 |
| `cs/unmanaged-code` | 2 |
| `cs/nested-if-statements` | 2 |
| `cs/equality-on-floats` | 2 |
| `cs/constant-condition` | 2 |
| `cs/call-to-unmanaged-code` | 2 |
| `cs/user-controlled-bypass` | 1 |
| `cs/loss-of-precision` | 1 |

## Alerts referencing paths absent from main

| Alert | State | Severity | Rule | Most recent ref | Location | Message |
|---:|---|---|---|---|---|---|
| #738 | open | note | `cs/nested-if-statements` | `refs/heads/main` | `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:100` | These 'if' statements can be combined.  |
| #676 | open | warning | `cs/useless-assignment-to-local` | `refs/heads/main` | `src/TiaAgent.ResponseCenter/obj/Release/net8.0-windows/Views/AgentResponseWindow.g.cs:81` | This assignment to resourceLocater is useless, since its value is never read.  |
| #675 | open | warning | `cs/useless-assignment-to-local` | `refs/heads/main` | `src/TiaAgent.Cli/obj/Release/net8.0/generated/System.Text.RegularExpressions.Generator/System.Text.RegularExpressions.Generator.RegexGenerator/RegexGenerator.g.cs:239` | This assignment to timeout is useless, since its value is never read.  |
| #491 | open | note | `cs/linq/missed-where` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:174` | This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.  |
| #490 | open | note | `cs/linq/missed-select` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:196` | This foreach loop immediately maps its iteration variable to another variable - consider mapping the sequence explicitly using '.Select(...)'.  |
| #489 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25` | Generic catch clause.  |
| #488 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:142` | Generic catch clause.  |
| #487 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:104` | Generic catch clause.  |
| #486 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:41` | Generic catch clause.  |
| #485 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:72` | Generic catch clause.  |
| #484 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:148` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #483 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:147` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #482 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:128` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #481 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:127` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #480 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:117` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #479 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:113` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #478 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:97` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #477 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:58` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #476 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:57` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #475 | open | note | `cs/path-combine` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:17` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #474 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/SbomGenerator.cs:142` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #473 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:163` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #472 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:128` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #471 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:111` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #470 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:87` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #469 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:80` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #468 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseValidator.cs:29` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #464 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:172` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #463 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:171` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #462 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:168` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #461 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Release/ReleaseGenerator.cs:162` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #460 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Commands/VerifyReleaseCommand.cs:43` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #459 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.Cli/Commands/GenerateReleaseMetadataCommand.cs:33` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #458 | open | note | `cs/empty-catch-block` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:25` | Poor error handling: empty catch block.  |
| #457 | open | warning | `cs/local-not-disposed` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:151` | Disposable 'StringWriter' is created but not disposed.  |
| #456 | open | warning | `cs/local-not-disposed` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:150` | Disposable 'StringWriter' is created but not disposed.  |
| #455 | open | warning | `cs/local-not-disposed` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:132` | Disposable 'StringWriter' is created but not disposed.  |
| #454 | open | warning | `cs/local-not-disposed` | `refs/heads/main` | `tests/TiaAgent.Cli.Tests/Release/ReleaseMetadataTests.cs:131` | Disposable 'StringWriter' is created but not disposed.  |
| #273 | open | note | `cs/useless-tostring-call` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` | Redundant call to 'ToString'.  |
| #272 | open | note | `cs/useless-tostring-call` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` | Redundant call to 'ToString'.  |
| #271 | open | note | `cs/useless-tostring-call` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:77` | Redundant call to 'ToString'.  |
| #270 | open | note | `cs/useless-tostring-call` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` | Redundant call to 'ToString'.  |
| #269 | open | note | `cs/useless-tostring-call` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:57` | Redundant call to 'ToString'.  |
| #268 | open | note | `cs/linq/missed-where` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:378` | This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.  |
| #267 | open | note | `cs/linq/missed-where` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:119` | This foreach loop implicitly filters its target sequence - consider filtering the sequence explicitly using '.Where(...)'.  |
| #255 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #254 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #253 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86` | Both branches of this 'if' statement return - consider using '?' to express intent better.  |
| #252 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.OpenCode/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #251 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:86` | Both branches of this 'if' statement return - consider using '?' to express intent better.  |
| #250 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:88` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #249 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Range.g.cs:81` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #248 | open | note | `cs/missed-ternary-operator` | `refs/heads/main` | `src/TiaAgent.Contracts/obj/Release/netstandard2.0/generated/PolySharp.SourceGenerators/PolySharp.SourceGenerators.PolyfillsGenerator/System.Index.g.cs:37` | Both branches of this 'if' statement write to the same variable - consider using '?' to express intent better.  |
| #245 | open | note | `cs/missed-readonly-modifier` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:357` | Field 'Value' can be 'readonly'.  |
| #244 | open | note | `cs/missed-readonly-modifier` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/SimpleJson.cs:356` | Field 'Type' can be 'readonly'.  |
| #234 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230` | Generic catch clause.  |
| #233 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:212` | Generic catch clause.  |
| #232 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:190` | Generic catch clause.  |
| #231 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:161` | Generic catch clause.  |
| #230 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:150` | Generic catch clause.  |
| #229 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:118` | Generic catch clause.  |
| #228 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:69` | Generic catch clause.  |
| #227 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs:104` | Generic catch clause.  |
| #181 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:176` | Generic catch clause.  |
| #180 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.Application/OpenCode/OpenCodeOrchestrator.cs:35` | Generic catch clause.  |
| #178 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:162` | Generic catch clause.  |
| #177 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:87` | Generic catch clause.  |
| #176 | open | note | `cs/catch-of-all-exceptions` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:46` | Generic catch clause.  |
| #171 | open | warning | `cs/useless-assignment-to-local` | `refs/heads/main` | `src/TiaAgent.AddIn/obj/Release/net48/Ui/AssistantPanel.g.cs:55` | This assignment to resourceLocater is useless, since its value is never read.  |
| #49 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:155` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #48 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:64` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #47 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:16` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #46 | open | note | `cs/path-combine` | `refs/heads/main` | `src/TiaAgent.AddIn/Bridge/BridgeClientConfig.cs:12` | Call to 'System.IO.Path.Combine' may silently drop its earlier arguments.  |
| #38 | open | note | `cs/empty-catch-block` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:230` | Poor error handling: empty catch block.  |
| #21 | open | warning | `cs/constant-condition` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:155` | Condition is always true because of ... == .... Condition is always true because of ... == ....  |
| #20 | open | warning | `cs/constant-condition` | `refs/heads/main` | `src/TiaAgent.OpenCode/Client/OpenCodeProcessManager.cs:131` | Condition is always false because of ... == ....  |

## Alerts whose most recent instance is not on main

None.
