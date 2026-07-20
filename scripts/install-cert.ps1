# Install TIA Portal Code Agent certificate to Company Trusted store
$cert = Get-ChildItem Cert:\CurrentUser\My | Where-Object { $_.Subject -match "TIA Portal Code Agent" } | Select-Object -First 1
if (-not $cert) {
    Write-Host "No certificate found" -ForegroundColor Red
    exit 1
}
Write-Host "Installing certificate: $($cert.Subject) [$($cert.Thumbprint)]"
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store("Company Trusted TIA Portal Add-Ins", "LocalMachine")
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()
Write-Host "Certificate installed successfully!" -ForegroundColor Green
