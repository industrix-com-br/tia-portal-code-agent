# Security Policy

## Safety and security overview

TIA Portal Code Agent operates in industrial engineering environments. Project integrity, credential protection, and safe failure behavior are mandatory.

The maintained technical security model is [`docs/spec/SECURITY_MODEL.md`](docs/spec/SECURITY_MODEL.md).

Current baseline controls:

- **Read-only product workflow:** the Add-In exposes explanations, reviews, and change proposals. It does not implement a project-change approval or apply workflow.
- **Current manifest permission:** `src/TiaAgent.AddIn/Config.xml` requests `TIA.ReadWrite` even though the product workflow is read-only. This mismatch is documented and requires implementation and V21 host validation before the permission can be reduced safely.
- **Loopback services:** Bridge and runtime-server endpoints bind to `127.0.0.1`.
- **Authenticated Bridge API:** protected endpoints use the local bearer token stored in `%LOCALAPPDATA%\TiaAgent\bridge.token`.
- **Untrusted engineering data:** project content cannot grant permission, approve a change, or alter tool policy.
- **Supply-chain boundary:** Siemens runtime assemblies are not committed or bundled in the NuGet payload.

Unsupported product behavior includes direct project writes, PLC download, online control, safety-program modification, hardware or network changes, and unattended project-wide refactoring.

## Supported baseline

Security fixes target the current maintained product line and repository baseline:

| Component | Baseline |
|---|---|
| TIA Portal | V21 |
| Add-In | .NET Framework 4.8 |
| Bridge and CLI | .NET 8 |
| Operating system | Windows x64 supported by the validated V21 environment |

Exact supported product versions and tested TIA Portal update levels must be stated in each release. Do not infer support from old prerelease numbers in documentation.

## Reporting a vulnerability

Use GitHub Private Vulnerability Reporting through **Security > Advisories > New draft security advisory**.

When private vulnerability reporting is unavailable, email `security@industrix.com.br` or contact the maintainer identified by [`.github/CODEOWNERS`](.github/CODEOWNERS).

Include:

- a description of the issue and potential impact;
- reproducible steps or a proof of concept;
- affected components and versions;
- relevant logs with credentials and project source removed;
- suggested mitigation when available.

Do not disclose the vulnerability publicly before maintainers have investigated and coordinated a fix or advisory.