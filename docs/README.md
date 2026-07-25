# Documentation

This directory contains maintained user guides, contributor references, maintainer procedures, architectural decisions, and technical specifications for TIA Portal Code Agent.

## Start here

- [Installation](INSTALLATION.md) — install the CLI, payload, and TIA Portal Add-In.
- [CLI reference](CLI.md) — verified commands, subcommands, and options.
- [Configuration](CONFIGURATION.md) — files, fields, environment variables, ports, and generated credentials.
- [Running end-to-end](RUN.md) — build, configure, start, and validate the complete system.
- [Runtime configuration](RUNTIME.md) — select and configure Mimo, OpenCode, or Claude Code.
- [Troubleshooting](TROUBLESHOOTING.md) — diagnose installation, runtime, and Add-In problems.

## Product lifecycle

- [Updating](UPDATING.md) — update the CLI package and activate its bundled payload.
- [Rollback](ROLLBACK.md) — restore a previously installed payload version.
- [Compatibility](COMPATIBILITY.md) — supported Windows, .NET, and TIA Portal combinations.
- [Installed layout](LAYOUT.md) — filesystem layout and manifest locations.

## Contributors and maintainers

- [Repository guidance](../AGENTS.md) — engineering constraints and working conventions.
- [Contributing](../CONTRIBUTING.md) — branch, pull request, validation, and security workflow.
- [Dependencies](DEPENDENCIES.md) — dependency policy and version management.
- [Versioning](VERSIONING.md) — product version and release-channel policy.
- [Releasing](RELEASING.md) — tag-driven build, NuGet publication, and GitHub Release procedure.
- [Repository ruleset](maintainers/repository-ruleset.md) — protected-branch configuration for maintainers.

## Architecture and specifications

The files under [`spec/`](spec/) describe product behavior, architecture, security boundaries, and Siemens integration constraints. Implemented behavior must still be verified against the current codebase; future requirements are explicitly identified as such.

- [Product specification](spec/PRODUCT_SPEC.md)
- [Architecture](spec/ARCHITECTURE.md)
- [Add-In technical specification](spec/ADDIN_TECHNICAL_SPEC.md)
- [Security model](spec/SECURITY_MODEL.md)
- [Known unknowns and validation queue](spec/KNOWN_UNKNOWNS.md)
- [TIA Portal Openness V21 reference](spec/tia-openness-v21/README.md)

Architectural decisions are recorded under [`adr/`](adr/).

## Directory conventions

- `docs/*.md` contains maintained guides useful to users or contributors.
- `docs/adr/` contains durable architectural decisions.
- `docs/maintainers/` contains repository administration procedures.
- `docs/spec/` contains technical specifications and reference material.

Temporary plans, completed checklists, investigation notes, and implementation scratch files do not belong in the maintained documentation tree.