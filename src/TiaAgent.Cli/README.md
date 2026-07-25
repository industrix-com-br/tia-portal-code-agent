# TIA Portal Code Agent

[![License](https://img.shields.io/badge/license-Apache%202.0-blue)](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/LICENSE)

A local AI-assisted engineering interface for Siemens TIA Portal V21. The `TiaAgent.Cli` global tool installs the Bridge and Add-In payload and manages local runtime services.

> [!CAUTION]
> This project is experimental and is not ready for production use.

## Installation

> [!IMPORTANT]
> Only prerelease versions are currently published. Without `--prerelease` or an explicit `--version`, the .NET tool command searches only for a stable release and may report that the package was not found.

```powershell
# Recommended while the project is in prerelease
dotnet tool install --global TiaAgent.Cli --prerelease

# Install a specific prerelease version
dotnet tool install --global TiaAgent.Cli --version 0.3.0-beta.5

# Install the bundled payload
tia-agent install

# Start services
tia-agent start
```

After the first stable version is published, it can be installed with:

```powershell
dotnet tool install --global TiaAgent.Cli
```

Restart TIA Portal V21, enable **TIA Portal Code Agent** under **Options > Settings > Add-Ins**, then right-click a supported project object and choose an action under **AI Code Agent**.

## Current actions

- Explain selected object
- Review selected object
- Propose change

The current product workflow is read-only. The proposal action returns recommendations and does not directly modify the TIA project.

## Useful commands

```powershell
tia-agent doctor
tia-agent status
tia-agent runtime list
tia-agent version --verbose
tia-agent stop
```

There is no separate `tia-agent versions` command. Installed payload versions are displayed by `tia-agent version --verbose`.

## Requirements

- Windows 10 or 11 x64
- Siemens TIA Portal V21 with Openness
- membership in the `Siemens TIA Openness` Windows group
- .NET 8 SDK and .NET Framework 4.8 runtime
- `TiaMcpServer`
- Mimo CLI, OpenCode, or Claude Code CLI

## Documentation

- [GitHub repository](https://github.com/industrix-com-br/tia-portal-code-agent)
- [Installation guide](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/docs/INSTALLATION.md)
- [CLI reference](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/docs/CLI.md)
- [Troubleshooting](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/docs/TROUBLESHOOTING.md)
- [Security model](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/docs/spec/SECURITY_MODEL.md)

## License

Licensed under the [Apache License 2.0](https://github.com/industrix-com-br/tia-portal-code-agent/blob/main/LICENSE).
