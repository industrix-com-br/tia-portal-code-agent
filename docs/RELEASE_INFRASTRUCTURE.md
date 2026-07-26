# Release Infrastructure

This document describes the administrative configuration required by the release workflow. It is not part of the normal release procedure.

For the operational release steps, follow [`RELEASING.md`](RELEASING.md).

## GitHub secrets

Configure the following repository secrets:

- `NUGET_USER`: NuGet.org account username used by `NuGet/login@v1`;
- `TIA_SIGNING_CERT_PFX_BASE64`;
- `TIA_SIGNING_CERT_PASSWORD`;
- optionally `TIA_SIGNING_CERT_THUMBPRINT`.

Do not configure a permanent NuGet API key in the normal release workflow.

## GitHub token permissions

The publication job requires:

```yaml
permissions:
  contents: write
  id-token: write
  issues: write
```

- `contents: write` allows the workflow to create the GitHub Release, upload assets, and create tags;
- `id-token: write` enables NuGet Trusted Publishing through OpenID Connect;
- `issues: write` allows commenting on issues for agent-friendly release triggers.

## NuGet Trusted Publishing

On NuGet.org, configure a Trusted Publishing policy with:

- repository owner: `industrix-com-br`;
- repository: `tia-portal-code-agent`;
- workflow file: `pipeline.yml`;
- environment: only when the workflow is configured to use one.

The workflow requests `id-token: write`, calls `NuGet/login@v1`, and uses the temporary API key returned by that action.

## First package bootstrap

NuGet.org may require the package ID to exist before a Trusted Publishing policy can be finalized.

When required:

1. Create a short-lived, package-scoped NuGet API key.
2. Publish the first package version manually.
3. Configure Trusted Publishing immediately afterward.
4. Revoke the temporary API key.

Do not add API-key fallback logic to the normal workflow.

## Release runner

Publication uses a self-hosted Windows runner with these labels:

```text
self-hosted, Windows, x64, tia-v21, release-runner
```

The runner must provide:

- Windows x64;
- TIA Portal V21;
- Siemens Add-In Publisher;
- .NET SDK 8 matching `global.json`;
- Git;
- access to the configured signing secrets;
- outbound access to GitHub and NuGet.org.

Pull-request CI must not run on this machine.

## Workflow contract

The release workflow is `.github/workflows/pipeline.yml`.

### Triggers

The workflow supports three triggers:

1. **Push to main / tags** - runs CI only
2. **Pull requests** - runs CI only
3. **workflow_dispatch** - runs release publication with version input
4. **Issues labeled** - runs release when issue labeled `release:run`

### Release jobs

When triggered for release:

1. **resolve-release** - validates version, checks preconditions
2. **publish** - creates tag, builds, publishes to NuGet and GitHub
3. **verify-release** - verifies publication, runs smoke tests

### Idempotency

The release workflow is idempotent:

- Tag already exists at expected commit: continue
- Tag exists at different commit: fail
- NuGet version exists: fail (cannot reuse version)
- GitHub Release incomplete: rebuild from immutable tag
- NuGet published but no GitHub Release: retry GitHub Release creation

### Concurrency

Release jobs use a concurrency group to prevent overlapping releases:

```yaml
concurrency:
  group: pipeline-release-{version}
  cancel-in-progress: false
```

## Agent-friendly release triggers

Agents can trigger releases via:

### Direct dispatch

```bash
gh workflow run pipeline.yml \
  --ref main \
  -f version=0.3.2
```

### Issue-based trigger

```bash
gh issue create \
  --title "Release v0.3.2" \
  --label "release:run"
```

The workflow:

1. Validates the issue title format: `Release vX.Y.Z` or `Release vX.Y.Z-prerelease.N`
2. Validates the actor has write/maintain/admin permission
3. Comments the workflow run URL on the issue
4. Publishes the release
5. Comments the final release report
6. Closes the issue on success

## Permissions model

### Workflow permissions

- CI jobs: read-only
- Release jobs: contents:write, id-token:write, issues:write

### Actor validation

For issue-based triggers, the workflow checks that the actor has write, maintain, or admin permission on the repository.

### Tag creation

Tags are created by the workflow, not by external actors. This ensures:

- Tags always point to HEAD of main at dispatch time
- Tags are only created after CI verification
- No orphaned tags from failed release attempts

## Documentation

When changing release infrastructure, update:

- [`RELEASING.md`](RELEASING.md) - operational release steps
- [`RELEASE_INFRASTRUCTURE.md`](RELEASE_INFRASTRUCTURE.md) - this file
- [`VERSIONING.md`](VERSIONING.md) - versioning rules
- `AGENTS.md` - agent instructions
