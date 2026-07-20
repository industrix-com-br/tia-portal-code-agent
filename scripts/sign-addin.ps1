#Requires -Version 5.1
<#
.SYNOPSIS
    Signs a TIA Portal .addin package (OPC format) with a self-signed certificate.
.DESCRIPTION
    Creates or reuses a code-signing certificate and applies an OPC digital signature
    to the .addin file so TIA Portal V21 accepts it.
.PARAMETER AddinPath
    Path to the .addin file to sign.
.PARAMETER CertThumbprint
    Thumbprint of an existing code-signing certificate to use.
    If not provided, creates a new self-signed certificate.
.EXAMPLE
    .\sign-addin.ps1 -AddinPath "C:\artifacts\TiaAgent-0-1-0.addin"
#>
param(
    [Parameter(Mandatory)]
    [string]$AddinPath,
    [string]$CertThumbprint
)

$ErrorActionPreference = "Stop"

Write-Host "`n=== TIA Portal .addin Signing ===" -ForegroundColor Cyan

if (!(Test-Path $AddinPath)) {
    Write-Host "File not found: $AddinPath" -ForegroundColor Red
    exit 1
}

# Get or create certificate
if ($CertThumbprint) {
    $cert = Get-ChildItem Cert:\CurrentUser\My\$CertThumbprint -ErrorAction Stop
    Write-Host "Using existing certificate: $($cert.Subject)" -ForegroundColor Gray
} else {
    # Look for existing TIA code signing cert
    $existing = Get-ChildItem Cert:\CurrentUser\My -CodeSigningCert -ErrorAction SilentlyContinue |
        Where-Object { $_.Subject -match "TIA" } | Select-Object -First 1

    if ($existing) {
        $cert = $existing
        Write-Host "Reusing existing certificate: $($cert.Subject)" -ForegroundColor Gray
    } else {
        Write-Host "Creating self-signed code signing certificate..." -ForegroundColor Yellow
        $cert = New-SelfSignedCertificate `
            -Subject "CN=TIA Portal Code Agent" `
            -Type CodeSigningCert `
            -CertStoreLocation Cert:\CurrentUser\My `
            -NotAfter (Get-Date).AddYears(5) `
            -HashAlgorithm SHA256
        Write-Host "Certificate created: $($cert.Thumbprint)" -ForegroundColor Green
    }
}

Write-Host "Certificate: $($cert.Subject) [$($cert.Thumbprint)]" -ForegroundColor Gray

# Sign using System.IO.Packaging OPC signing
Write-Host "`nSigning .addin package..." -ForegroundColor Yellow

Add-Type -AssemblyName WindowsBase

$package = [System.IO.Packaging.Package]::Open($AddinPath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite)

try {
    # Remove existing signatures if any
    $package.SignatureManager.Signatures | ForEach-Object { $_.Delete() }

    # Create signature
    $sigManager = $package.SignatureManager
    $sig = $sigManager.Sign([System.Security.Cryptography.X509Certificates.X509Certificate2]$cert)

    # Set signature type to OPC (not thumbnail)
    $sig.SignedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"

    $package.Close()

    Write-Host "Package signed successfully!" -ForegroundColor Green
    Write-Host "File: $AddinPath" -ForegroundColor Gray
    $size = (Get-Item $AddinPath).Length / 1KB
    Write-Host "Size: $([math]::Round($size, 1)) KB" -ForegroundColor Gray
} catch {
    $package.Close()
    Write-Host "Signing failed: $_" -ForegroundColor Red
    exit 1
}
