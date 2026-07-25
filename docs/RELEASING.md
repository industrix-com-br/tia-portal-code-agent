# Releasing

A release is produced from one immutable Git tag, published as one NuGet package containing the complete installation payload, and mirrored as a GitHub Release with downloadable assets.

## 1. Choose the version

Follow [`VERSIONING.md`](VERSIONING.md). Examples:

```text
0.3.0-beta.1
0.3.0-rc.1
0.3.0
```

Confirm the target commit is merged into `main` and the `CI` check passes.

## 2. Validate locally

On a Windows machine with TIA Portal V21 and signing credentials available:

```powershell
.\build.ps1 release -Version 0.3.0-beta.1
```

The command cleans previous outputs, builds, tests, packages and signs the Add-In, verifies it, assembles the payload, creates the NuGet package, validates its contents, and tests local tool installation.

## 3. Create the tag

```powershell
git switch main
git pull --ff-only
git tag -a v0.3.0-beta.1 -m "Release v0.3.0-beta.1"
git push origin v0.3.0-beta.1
```

The tag triggers `.github/workflows/pipeline.yml`. The publication job extracts the version, runs:

```powershell
.\build.ps1 release -Version 0.3.0-beta.1
```

It then authenticates to NuGet through Trusted Publishing, pushes the generated `.nupkg` with `--skip-duplicate`, and creates a GitHub Release containing:

- generated release notes;
- `TiaAgent.Cli.<version>.nupkg`;
- `TiaAgent-<version>.addin`.

Versions containing a prerelease suffix such as `-alpha.N`, `-beta.N`, or `-rc.N` are marked as prereleases on GitHub.

## 4. Validate the GitHub Release

Open the release associated with the tag and confirm:

- the release is published rather than only showing as a tag;
- prerelease versions are marked correctly;
- the NuGet package and signed Add-In are attached;
- the generated release notes describe the expected changes.

## 5. Validate NuGet publication

After NuGet finishes processing the package, verify the exact version is visible and install it in a clean location:

```powershell
# Stable
dotnet tool install --global TiaAgent.Cli --version 0.3.0

# Prerelease
dotnet tool install --global TiaAgent.Cli --version 0.3.0-beta.1
```

Then run:

```powershell
tia-agent version
tia-agent doctor
```

## GitHub and NuGet.org configuration

### GitHub secrets

Configure:

- `NUGET_USER`: NuGet.org account username used by `NuGet/login@v1`;
- `TIA_SIGNING_CERT_PFX_BASE64`;
- `TIA_SIGNING_CERT_PASSWORD`;
- optionally `TIA_SIGNING_CERT_THUMBPRINT`.

### GitHub token permissions

The release job requires:

```yaml
permissions:
  contents: write
  id-token: write
```

`contents: write` creates the GitHub Release and uploads its assets. `id-token: write` enables NuGet Trusted Publishing through OIDC.

### NuGet Trusted Publishing

On NuGet.org, create a Trusted Publishing policy for:

- repository owner: `industrix-com-br`;
- repository: `tia-portal-code-agent`;
- workflow file: `pipeline.yml`;
- environment: only when the workflow is configured to use one.

The workflow requests `id-token: write`, calls `NuGet/login@v1`, and uses the temporary API key returned by that action. Do not configure a permanent NuGet API key in the normal release workflow.

### First package bootstrap

NuGet.org may require the package ID to exist before a Trusted Publishing policy can be finalized. When necessary, publish the first version manually with a short-lived scoped API key, configure Trusted Publishing immediately afterward, revoke the key, and do not add hybrid API-key fallback logic to the workflow.

## Runner requirement

Publication uses the self-hosted Windows release runner labels:

```text
self-hosted, Windows, x64, tia-v21, release-runner
```

That runner must have TIA Portal V21, the Siemens Add-In Publisher, .NET SDK 8, and access to the signing secrets. Pull-request CI never runs on this machine.
