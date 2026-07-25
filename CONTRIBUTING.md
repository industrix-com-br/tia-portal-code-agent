# Contributing

## Branch and pull request workflow

`main` is protected. Create a short-lived branch, open a pull request to `main`, keep the branch current, resolve review conversations, and merge only after the required CI check passes.

Use squash merge for normal contributions. Do not push release artifacts or version files to the repository.

## Local validation

Run the same product checks through the repository build entrypoint:

```powershell
.\build.ps1 build
.\build.ps1 test
```

For packaging changes on a machine with TIA Portal V21 installed:

```powershell
.\build.ps1 pack
```

`pack` creates the versioned Siemens Add-In, assembles the installation payload, creates the NuGet package, validates its contents, and verifies that the tool package can be installed.

## Versioning and releases

All first-party components share one product version. Public releases are created from tags on `main`; do not edit project files to prepare a release.

See:

- [`docs/VERSIONING.md`](docs/VERSIONING.md)
- [`docs/RELEASING.md`](docs/RELEASING.md)
- [`docs/SIGNING.md`](docs/SIGNING.md)

## Security

Do not open public issues for vulnerabilities. Follow [`SECURITY.md`](SECURITY.md) and the authoritative model in [`docs/spec/SECURITY_MODEL.md`](docs/spec/SECURITY_MODEL.md).
