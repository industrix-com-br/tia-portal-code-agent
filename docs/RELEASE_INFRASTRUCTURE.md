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
```

`contents: write` allows the workflow to create the GitHub Release and upload its assets.

`id-token: write` enables NuGet Trusted Publishing through OpenID Connect.

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

It must:

1. accept release tags matching `vX.Y.Z`, `vX.Y.Z-alpha.N`, `vX.Y.Z-beta.N`, or `vX.Y.Z-rc.N`;
2. verify that the tag resolves to a commit contained in `main`;
3. run `./build.ps1 release -Version <version>`;
4. authenticate to NuGet through Trusted Publishing;
5. publish `TiaAgent.Cli.<version>.nupkg`;
6. create a GitHub Release;
7. attach the NuGet package and signed Add-In;
8. mark prerelease versions correctly.

Any change to this contract must also update [`RELEASING.md`](RELEASING.md), [`VERSIONING.md`](VERSIONING.md), and the relevant contributor documentation.
