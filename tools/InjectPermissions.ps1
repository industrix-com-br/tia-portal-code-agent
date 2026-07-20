# Inject Security permission into .addin OPC package
param([string]$AddinPath)

Add-Type -AssemblyName WindowsBase
Add-Type -AssemblyName System.IO

$package = [System.IO.Packaging.Package]::Open($AddinPath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite)

# 1. Create the Security permission file
$securityUri = [System.Uri]::new("/Permissions/Required/Security/System.UnrestrictedAccess", [System.UriKind]::Relative)
$securityPart = $package.CreatePart($securityUri, "text/plain")
$securityContent = "System.UnrestrictedAccess`nTerminal window requires full UI, file I/O, environment, and process permissions"
$writer = New-Object System.IO.StreamWriter($securityPart.GetStream())
$writer.Write($securityContent)
$writer.Close()
Write-Host "Created: /Permissions/Required/Security/System.UnrestrictedAccess"

# 2. Find and update Content_Types.xml
$ctPart = $null
foreach ($part in $package.GetParts()) {
    if ($part.Uri.ToString() -match "Content_Types") {
        $ctPart = $part
        break
    }
}
if ($ctPart) {
    $ctReader = New-Object System.IO.StreamReader($ctPart.GetStream())
    $ctXml = $ctReader.ReadToEnd()
    $ctReader.Close()
    if ($ctXml -notmatch "UnrestrictedAccess") {
        $ctXml = $ctXml.Replace(
            '<Default Extension="ReadOnly" ContentType="text/plain" />',
            '<Default Extension="UnrestrictedAccess" ContentType="text/plain" /><Default Extension="ReadOnly" ContentType="text/plain" />'
        )
        $ctStream2 = $ctPart.GetStream([System.IO.FileMode]::Create)
        $ctWriter = New-Object System.IO.StreamWriter($ctStream2)
        $ctWriter.Write($ctXml)
        $ctWriter.Close()
        Write-Host "Updated Content_Types.xml"
    } else {
        Write-Host "Content_Types.xml already has UnrestrictedAccess"
    }
}

# 3. Find and update _rels/.rels
$relsPart = $null
foreach ($part in $package.GetParts()) {
    if ($part.Uri.ToString() -eq "/_rels/.rels") {
        $relsPart = $part
        break
    }
}
if ($relsPart) {
    $relsReader = New-Object System.IO.StreamReader($relsPart.GetStream())
    $relsXml = $relsReader.ReadToEnd()
    $relsReader.Close()
    if ($relsXml -notmatch "System.UnrestrictedAccess") {
        $relsXml = $relsXml.Replace(
            '<Relationship Type="Tia" Target="/Permissions/Required/Tia/TIA.ReadOnly"',
            '<Relationship Type="Security" Target="/Permissions/Required/Security/System.UnrestrictedAccess" Id="System.UnrestrictedAccess" /><Relationship Type="Tia" Target="/Permissions/Required/Tia/TIA.ReadOnly"'
        )
        $relsStream2 = $relsPart.GetStream([System.IO.FileMode]::Create)
        $relsWriter = New-Object System.IO.StreamWriter($relsStream2)
        $relsWriter.Write($relsXml)
        $relsWriter.Close()
        Write-Host "Updated .rels with Security relationship"
    } else {
        Write-Host ".rels already has Security relationship"
    }
}

$package.Close()
Write-Host "Done! File: $AddinPath"
