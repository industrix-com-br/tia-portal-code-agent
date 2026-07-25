# Documentation

This directory contains the maintained user guides, contributor references, maintainer procedures, architectural decisions, and normative specifications for TIA Portal Code Agent.

## Start here

- [Installation](INSTALLATION.md) — install the CLI, payload, and TIA Portal Add-In.
- [Running end-to-end](RUN.md) — build, configure, start, and validate the complete development environment.
- [Runtime configuration](RUNTIME.md) — select and configure Mimo, OpenCode, or Claude Code.
- [Troubleshooting](TROUBLESHOOTING.md) — diagnose common installation, runtime, and Add-In problems.

## Product lifecycle

- [Updating](UPDATING.md) — update the installed payload and manage release channels.
- [Rollback](ROLLBACK.md) — restore a previously installed version.
- [Compatibility](COMPATIBILITY.md) — supported Windows, .NET, and TIA Portal combinations.
- [Installed layout](LAYOUT.md) — filesystem layout and manifest locations.

## Contributors and maintainers

- [Repository guidance](../AGENTS.md) — non-negotiable engineering constraints and working conventions.
- [Contributing](../CONTRIBUTING.md) — branch, pull request, validation, and security workflow.
- [Dependencies](DEPENDENCIES.md) — dependency policy and version management.
- [Versioning](VERSIONING.md) — product version and release-channel policy.
- [Releasing](RELEASING.md) — tag-driven build, NuGet publication, and GitHub Release procedure.
- [Repository ruleset](maintainers/repository-ruleset.md) — protected-branch configuration for maintainers.

## Architecture and specifications

The files under [`spec/`](spec/) are the normative source of truth for product behavior, architecture, security, and Siemens integration constraints.

- [Product specification](spec/PRODUCT_SPEC.md)
- [Architecture](spec/ARCHITECTURE.md)
- [Add-In technical specification](spec/ADDIN_TECHNICAL_SPEC.md)
- [Security model](spec/SECURITY_MODEL.md)
- [Known unknowns and validation queue](spec/KNOWN_UNKNOWNS.md)
- [TIA Portal Openness V21 reference](spec/tia-openness-v21/README.md)

Architectural decisions are recorded under [`adr/`](adr/).

## Directory conventions

- `docs/*.md` contains maintained guides that are useful to users or contributors.
- `docs/adr/` contains durable architectural decisions.
- `docs/maintainers/` contains repository administration procedures.
- `docs/spec/` contains normative contracts and technical reference material.

Temporary plans, completed release checklists, investigation notes, and implementation scratch files do not belong in the maintained documentation tree.