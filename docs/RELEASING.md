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

## Release triggers

The permanent release workflow is `.github/workflows/pipeline.yml`.

### Trigger options

| Trigger | Command | Version Source | Actor Required |
|---------|---------|----------------|----------------|
| Manual dispatch | `gh workflow run pipeline.yml --ref main -f version=X.Y.Z` | `inputs.version` parameter | Maintainer |
| Issue label | Create issue + apply `release:run` label | Issue title: `Release vX.Y.Z` | write/maintain/admin |

### Option 1: Manual dispatch (recommended)

```bash
gh workflow run pipeline.yml \
  --ref main \
  -f version=0.3.2
```

### Option 2: Issue-based trigger (agent-friendly)

1. Create an issue with title: `Release v0.3.2` or `Release v0.3.2-beta.1`
2. Apply the label `release:run`
3. The workflow will execute automatically

**Issue title format (strict):**

```text
Release v0.3.2
Release v0.3.2-beta.1
Release v0.3.2-rc.1
```

**Actor validation:** Only users with write, maintain, or admin permission can trigger releases via issues.

The workflow:

- extracts the version from the issue title;
- validates the version format;
- validates actor permissions;
- comments the workflow run URL on the issue;
- publishes the release;
- comments the final release report;
- closes the issue on success;
- leaves issue open with error if publication fails.

### Monitoring a release

```bash
# List recent workflow runs
gh run list --workflow pipeline.yml --limit 10

# Watch a specific run
gh run watch <run-id> --exit-status

# View run details
gh run view <run-id> --log
```

## Preconditions

Before creating a release:

1. Confirm that the target changes are merged into `main`.
2. Confirm that the latest `CI` check for the target commit passed.
3. Confirm that the requested version has never been published.
4. Confirm that the release tag does not already exist.
5. Confirm that the target commit is contained in `main`.
6. Review the changes since the previous release for unexpected or incomplete work.

Do not release directly from a feature branch or pull request branch.

## What the workflow does

The release workflow performs these steps:

1. **Validates the version format** - rejects invalid formats early;
2. **Resolves HEAD commit** on `main` at dispatch time;
3. **Checks for existing tag** - fails if tag exists with different commit;
4. **Checks NuGet** - fails if version already published;
5. **Creates annotated tag** on HEAD commit;
6. **Verifies CI** - ensures the commit passed Pipeline CI;
7. **Builds the release** using `./build.ps1 release -Version <version>`;
8. **Publishes to NuGet** via Trusted Publishing;
9. **Creates GitHub Release** with proper prerelease flags;
10. **Attaches assets**: `TiaAgent.Cli.<version>.nupkg` and `TiaAgent-<version>.addin`;
11. **Verifies publication** - checks GitHub Release assets and NuGet availability;
12. **Runs smoke test** - installs and runs `tia-agent version` in clean environment.

## Idempotency

The release operation is safe to rerun:

- **Tag absent**: creates it;
- **Tag exists and points to expected commit**: continues;
- **Tag exists and points to another commit**: fails clearly;
- **NuGet package already exists and GitHub Release complete**: reports as already complete;
- **NuGet package exists but GitHub Release incomplete**: rebuilds from immutable tag;
- **Never moves an existing tag**;
- **Never deletes and recreates a published version**;
- **Never overwrites a NuGet package**;
- **Never silently ignores a partial release**.

## Validate the published release

After the workflow completes:

1. Check the GitHub Release for correct assets and prerelease flags;
2. Verify NuGet.org has the package:

```bash
curl -I https://api.nuget.org/v3-flatcontainer/tiaagent.cli/X.Y.Z/tiaagent.cli.X.Y.Z.nupkg
```

3. Install in a clean location:

```powershell
# Stable
dotnet tool install --global TiaAgent.Cli --version X.Y.Z

# Prerelease
dotnet tool install --global TiaAgent.Cli --version X.Y.Z-beta.N
```

4. Verify the installed version:

```powershell
tia-agent version
tia-agent doctor
```

## Failure handling

If the workflow fails:

1. Check the workflow run logs for the specific failure;
2. If the tag was not created, fix the issue and re-run the workflow;
3. If the tag was created but NuGet publication failed, re-run the workflow (idempotent);
4. If NuGet publication succeeded but GitHub Release is missing, re-run the workflow;
5. If NuGet version was already published, it cannot be replaced - create a new version;
6. If the tag points to incorrect code after publication, do not move the tag - create a corrected release with a new version.

Never bypass failed validation, signing, tests, or package verification.

## Prohibited actions

Release agents and maintainers must not:

- manually edit product versions;
- publish from an unmerged branch;
- use `--skipEngMemberCheck`;
- bypass signing or package validation;
- add permanent NuGet API keys to the normal release workflow;
- publish Siemens assemblies;
- report success before checking both GitHub Releases and NuGet.org;
- create temporary workflows, branches, or marker files for release operations;
- create PRs solely for release observability.

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

## Commands reference

### Create a release

```bash
# Via workflow dispatch
gh workflow run pipeline.yml --ref main -f version=0.3.2

# Via issue (agent-friendly)
gh issue create --title "Release v0.3.2" --label "release:run"
```

### Monitor a release

```bash
# List runs
gh run list --workflow pipeline.yml --limit 10

# Watch run
gh run watch <run-id> --exit-status

# View logs
gh run view <run-id> --log
```

### Check release status

```bash
# List releases
gh release list --limit 10

# View specific release
gh release view v0.3.2

# Check NuGet
curl -I https://api.nuget.org/v3-flatcontainer/tiaagent.cli/0.3.2/tiaagent.cli.0.3.2.nupkg
```
