# Release Guide

This document defines the required procedure for creating a TIA Portal Code Agent release.

Release infrastructure, credentials, Trusted Publishing, and runner configuration are documented separately in [`RELEASE_INFRASTRUCTURE.md`](RELEASE_INFRASTRUCTURE.md).

## Authority

Do not create or publish a release unless a maintainer explicitly requests it.

Before starting, confirm the exact release version.

Supported formats:

```text
X.Y.Z
X.Y.Z-alpha.N
X.Y.Z-beta.N
X.Y.Z-rc.N
```

The Git tag is the public source of truth for the product version. Do not edit project files, package files, manifests, or version properties to prepare a release.

See [`VERSIONING.md`](VERSIONING.md) for the complete versioning rules.

## 1. Check the release preconditions

Before creating a release:

1. Confirm that the target changes are merged into `main`.
2. Confirm that the latest `CI` check for the target commit passed.
3. Confirm that the requested version has never been published.
4. Confirm that the release tag does not already exist.
5. Confirm that the target commit is contained in `main`.
6. Review the changes since the previous release for unexpected or incomplete work.

Do not release directly from a feature branch or pull request branch.

## 2. Validate the release locally

On a Windows machine with TIA Portal V21 and the required signing credentials:

```powershell
git switch main
git pull --ff-only

.\build.ps1 release -Version X.Y.Z-beta.N
```

The command must complete successfully. It cleans previous outputs, builds, tests, signs and verifies the Add-In, assembles the installation payload, creates the NuGet package, validates its contents, and tests local tool installation.

If the required TIA Portal V21 or signing environment is unavailable, do not report local release validation as successful. State that final validation must be completed by the configured release runner.

## 3. Create and push the release tag

Create an annotated tag from the validated `main` commit:

```powershell
git tag -a vX.Y.Z-beta.N -m "Release vX.Y.Z-beta.N"
git push origin vX.Y.Z-beta.N
```

Never:

- move an existing release tag;
- delete and recreate a published release tag;
- reuse a version;
- tag a commit that is not contained in `main`.

Pushing the tag starts the publication job in `.github/workflows/pipeline.yml`.

## 4. Monitor publication

Verify that the `Publish NuGet` job:

1. checked out the expected tag;
2. validated that the tag belongs to `main`;
3. completed `build.ps1 release`;
4. published `TiaAgent.Cli.<version>.nupkg`;
5. created the GitHub Release;
6. attached the NuGet package and signed Add-In.

Expected release assets:

```text
TiaAgent.Cli.<version>.nupkg
TiaAgent-<version>.addin
```

Versions containing `-alpha.N`, `-beta.N`, or `-rc.N` must be marked as prereleases on GitHub.

## 5. Validate the published release

Confirm that the GitHub Release is published and contains the expected assets and generated release notes.

After NuGet.org finishes processing the package, install the exact published version in a clean location:

```powershell
# Stable
dotnet tool install --global TiaAgent.Cli --version X.Y.Z

# Prerelease
dotnet tool install --global TiaAgent.Cli --version X.Y.Z-beta.N
```

For an existing installation:

```powershell
dotnet tool update --global TiaAgent.Cli --version X.Y.Z-beta.N
```

Then run:

```powershell
tia-agent version
tia-agent doctor
```

Confirm that the reported product version matches the release version and that diagnostics complete successfully.

## Failure handling

If the workflow fails before publishing an immutable artifact, retry the failed job or use the workflow's manual dispatch with the existing release tag.

If the NuGet version was already published, it cannot be replaced. Correct the issue and create a new version.

If the tag points to incorrect code after publication, do not move the tag. Create a corrected release with a new version.

Never bypass failed validation, signing, tests, or package verification.

## Prohibited actions

Release agents and maintainers must not:

- manually edit product versions;
- publish from an unmerged branch;
- use `--skipEngMemberCheck`;
- bypass signing or package validation;
- add permanent NuGet API keys to the normal release workflow;
- publish Siemens assemblies;
- report success before checking both GitHub Releases and NuGet.org.

## Completion report

A release task is complete only after both the GitHub Release and NuGet publication have been verified.

Report:

```text
Version:
Tag:
Commit:
CI status:
Publish workflow:
GitHub Release:
NuGet publication:
Attached artifacts:
Smoke-test result:
Known issues:
```
