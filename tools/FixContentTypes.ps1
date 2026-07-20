param([string]$AddinPath)

Add-Type -AssemblyName WindowsBase

$package = [System.IO.Packaging.Package]::Open($AddinPath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite)

$ctUri = [System.IO.Packaging.PackUriHelper]::CreatePartUri(
    (New-Object System.Uri("/[Content_Types].xml", [System.UriKind]::Relative))
)
$ctPart = $package.GetPart($ctUri)

$reader = New-Object System.IO.StreamReader($ctPart.GetStream())
$xml = $reader.ReadToEnd()
$reader.Close()

if ($xml -notmatch "UnrestrictedAccess") {
    $xml = $xml.Replace(
        '<Default Extension="ReadOnly" ContentType="text/plain" />',
        '<Default Extension="UnrestrictedAccess" ContentType="text/plain" /><Default Extension="ReadOnly" ContentType="text/plain" />'
    )
    $stream = $ctPart.GetStream([System.IO.FileMode]::Create)
    $writer = New-Object System.IO.StreamWriter($stream)
    $writer.Write($xml)
    $writer.Close()
    Write-Host "Content_Types: added UnrestrictedAccess"
} else {
    Write-Host "Content_Types: already has UnrestrictedAccess"
}

$package.Close()
