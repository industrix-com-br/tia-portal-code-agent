# Add-In signing

Every public alpha, beta, RC, and stable `.addin` artifact must be signed and verified before the NuGet package is published.

## Tooling

`tools/OpcSigner` signs the OPC package produced by the Siemens Add-In Publisher. Certificate sources are evaluated in this order:

1. `TIA_SIGNING_CERT_THUMBPRINT` in the Windows certificate store;
2. `TIA_SIGNING_CERT_PFX` plus `TIA_SIGNING_CERT_PASSWORD`;
3. `TIA_SIGNING_CERT_PFX_BASE64` plus `TIA_SIGNING_CERT_PASSWORD`.

Certificate files and passwords must never be committed.

## Development packaging

```powershell
.\build.ps1 pack
```

Development packaging may use the local self-signed fallback. It is not a public release.

## Release packaging

```powershell
.\build.ps1 release -Version 0.3.0-beta.1
```

`release` requires signing, verifies the signature before finalizing the artifact, and fails when valid certificate material is unavailable.

The GitHub publication job receives these repository secrets:

- `TIA_SIGNING_CERT_PFX_BASE64`;
- `TIA_SIGNING_CERT_PASSWORD`;
- optionally `TIA_SIGNING_CERT_THUMBPRINT`.

## Manual verification

```powershell
.\tools\OpcSigner\bin\Release\net48\OpcSigner.exe verify artifacts\TiaAgent-0.3.0-beta.1.addin
```

## Rotation

Before a certificate expires:

1. obtain a replacement code-signing certificate;
2. update the GitHub secrets or runner certificate store;
3. run a prerelease with the new certificate;
4. verify the generated `.addin`;
5. revoke or archive the retired private key according to the organization policy.
