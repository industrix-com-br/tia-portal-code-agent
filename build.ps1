#Requires -Version 5.1
<#
.SYNOPSIS
    Build and release entrypoint for TIA Portal Code Agent.

.EXAMPLE
    .\build.ps1 build
    .\build.ps1 test
    .\build.ps1 pack -Version 0.3.0-beta.1
    .\build.ps1 release -Version 0.3.0-beta.1
#>
param(
    [Parameter(Position = 0)]
    [ValidateSet("build", "test", "pack", "release", "install-dev", "clean", "help")]
    [string]$Command = "help",

    [ValidatePattern('^\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+|-dev)?$')]
    [string]$Version
)

$ErrorActionPreference = "Stop"
$Root = $PSScriptRoot
$Configuration = "Release"
$ArtifactsDir = Join-Path $Root "artifacts"
$Solution = Join-Path $Root "TiaAgent.sln"
$ReleaseVersionPattern = '\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+)?'

function Resolve-ProductVersion {
    param([string]$ExplicitVersion)

    if ($ExplicitVersion) { return $ExplicitVersion }
    if ($env:TIA_AGENT_VERSION) { return $env:TIA_AGENT_VERSION }

    if ($env:GITHUB_REF_TYPE -eq "tag" -and $env:GITHUB_REF_NAME -match "^v(?<version>$ReleaseVersionPattern)$") {
        return $Matches.version
    }

    try {
        $tag = (& git -C $Root describe --tags --exact-match HEAD 2>$null).Trim()
        if ($LASTEXITCODE -eq 0 -and $tag -match "^v(?<version>$ReleaseVersionPattern)$") {
            return $Matches.version
        }
    } catch { }

    return "0.0.0-dev"
}

function Resolve-CommitSha {
    if ($env:GITHUB_SHA) { return $env:GITHUB_SHA }

    try {
        $sha = (& git -C $Root rev-parse HEAD 2>$null).Trim()
        if ($LASTEXITCODE -eq 0 -and $sha) { return $sha }
    } catch { }

    return "unknown"
}

$ProductVersion = Resolve-ProductVersion -ExplicitVersion $Version
$CommitSha = Resolve-CommitSha
$MsBuildVersionArguments = @("-p:Version=$ProductVersion", "-p:SourceRevisionId=$CommitSha")

$tiaBasePath = "C:\Program Files\Siemens\Automation\Portal V21"
$tiaNet48Path = "$tiaBasePath\PublicAPI\V21\net48"
$tiaAddInPath = "$tiaBasePath\PublicAPI\V21"
if (Test-Path "$tiaNet48Path\Siemens.Engineering.Base.dll") {
    $env:TiaPublicApiDir = $tiaNet48Path
    $env:SiemensAssembliesExist = "true"
    Write-Host "TIA Openness V21 detected: $tiaNet48Path" -ForegroundColor Gray
}
if (Test-Path "$tiaAddInPath\Siemens.Engineering.AddIn.Base.dll") {
    $env:TiaAddInApiDir = $tiaAddInPath
}

function Write-Header([string]$Text) {
    Write-Host ""
    Write-Host "======================================" -ForegroundColor Cyan
    Write-Host "  $Text" -ForegroundColor Cyan
    Write-Host "======================================" -ForegroundColor Cyan
}

function Write-Ok([string]$Text) { Write-Host "  OK: $Text" -ForegroundColor Green }
function Write-Info([string]$Text) { Write-Host "  $Text" -ForegroundColor Gray }

function Invoke-Dotnet {
    param([Parameter(Mandatory = $true)][string[]]$Arguments)

    $separatorIndex = [Array]::IndexOf($Arguments, '--')
    if ($separatorIndex -ge 0) {
        $before = if ($separatorIndex -gt 0) { $Arguments[0..($separatorIndex - 1)] } else { @() }
        $after = $Arguments[$separatorIndex..($Arguments.Length - 1)]
        & dotnet @before @MsBuildVersionArguments @after
    } else {
        & dotnet @Arguments @MsBuildVersionArguments
    }

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet command failed with exit code $LASTEXITCODE"
    }
}

function Invoke-MsBuildTarget {
    param(
        [Parameter(Mandatory = $true)][string]$Target,
        [string[]]$ExtraArguments = @()
    )

    & dotnet msbuild "$Root\src\TiaAgent.AddIn\TiaAgent.AddIn.csproj" -t:$Target -p:Configuration=$Configuration @MsBuildVersionArguments @ExtraArguments
    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild target '$Target' failed with exit code $LASTEXITCODE"
    }
}

function Ensure-OpcSigner {
    $opcSignerExe = "$Root\tools\OpcSigner\bin\$Configuration\net48\OpcSigner.exe"
    if (-not (Test-Path $opcSignerExe)) {
        Write-Info "Building OpcSigner..."
        Invoke-Dotnet @("build", "$Root\tools\OpcSigner\OpcSigner.csproj", "--configuration", $Configuration, "--verbosity", "minimal")
    }
}

function Invoke-Clean {
    Write-Header "CLEAN"
    Get-ChildItem "$Root\src", "$Root\tests", "$Root\tools" -Directory -Recurse -Include bin,obj -ErrorAction SilentlyContinue |
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    if (Test-Path $ArtifactsDir) {
        Remove-Item $ArtifactsDir -Recurse -Force
    }
    Write-Ok "Build outputs removed"
}

function Invoke-Build {
    Write-Header "BUILD $ProductVersion"
    Invoke-Dotnet @("restore", $Solution, "--force-evaluate", "--verbosity", "minimal")
    Invoke-Dotnet @("build", $Solution, "--configuration", $Configuration, "--no-restore", "--verbosity", "minimal")

    foreach ($artifact in @(
        "$Root\src\TiaAgent.AddIn\bin\$Configuration\net48\TiaAgent.AddIn.dll",
        "$Root\src\TiaAgent.Bridge\bin\$Configuration\net8.0\TiaAgent.Bridge.dll",
        "$Root\src\TiaAgent.Cli\bin\$Configuration\net8.0\TiaAgent.Cli.dll",
        "$Root\src\TiaAgent.ResponseCenter\bin\$Configuration\net8.0-windows\TiaAgent.ResponseCenter.exe"
    )) {
        if (-not (Test-Path $artifact)) {
            throw "Expected build artifact not found: $artifact"
        }
    }

    Write-Ok "Solution compiled"
}

function Invoke-Test {
    param([switch]$NoRestore)

    Write-Header "TEST $ProductVersion"
    if (-not $NoRestore) {
        Invoke-Dotnet @("restore", $Solution, "--force-evaluate", "--verbosity", "minimal")
    }

    Invoke-Dotnet @("test", $Solution, "--configuration", $Configuration, "--no-restore", "--verbosity", "normal")
    Write-Ok "Tests passed"
}

function Invoke-PackAddIn {
    param([switch]$RequireSigning)

    Ensure-OpcSigner
    if (-not (Test-Path $ArtifactsDir)) {
        New-Item -ItemType Directory -Path $ArtifactsDir -Force | Out-Null
    }

    Get-ChildItem $ArtifactsDir -Filter "TiaAgent-*.addin*" -File -ErrorAction SilentlyContinue | Remove-Item -Force

    $extraArguments = @("-p:RequireSigning=$($RequireSigning.IsPresent.ToString().ToLowerInvariant())")
    Invoke-MsBuildTarget -Target "PackAddIn" -ExtraArguments $extraArguments

    $expected = Join-Path $ArtifactsDir "TiaAgent-$ProductVersion.addin"
    if (-not (Test-Path $expected)) {
        throw "Expected Add-In artifact not found: $expected"
    }

    Write-Ok "Add-In packaged as $(Split-Path $expected -Leaf)"
}

function Invoke-VerifyAddIn {
    param([switch]$RequireSigning)

    $extraArguments = @("-p:RequireSigning=$($RequireSigning.IsPresent.ToString().ToLowerInvariant())")
    Invoke-MsBuildTarget -Target "VerifyAddIn" -ExtraArguments $extraArguments
    Write-Ok "Add-In verified"
}

function New-PayloadManifest {
    param([string]$PayloadDir)

    $files = @()
    Get-ChildItem $PayloadDir -Recurse -File | ForEach-Object {
        $relativePath = $_.FullName.Substring($PayloadDir.Length + 1).Replace("\", "/")
        $files += @{
            relativePath = $relativePath
            sha256Hash = (Get-FileHash $_.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
            sizeBytes = $_.Length
        }
    }

    $bridgePath = Join-Path $PayloadDir "Bridge\TiaAgent.Bridge.dll"
    $responseCenterPath = Join-Path $PayloadDir "ResponseCenter\TiaAgent.ResponseCenter.exe"
    $addInName = "TiaAgent-$ProductVersion.addin"
    $addInPath = Join-Path $PayloadDir "AddIn\$addInName"

    $manifest = [ordered]@{
        schemaVersion = 1
        productVersion = $ProductVersion
        commitSha = $CommitSha
        builtAt = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")
        compatibility = [ordered]@{
            tiaPortalVersion = "V21"
            opennessVersion = "V21"
            targetFramework = "net8.0"
        }
        components = [ordered]@{
            bridge = [ordered]@{
                relativePath = "Bridge/TiaAgent.Bridge.dll"
                version = $ProductVersion
                sha256Hash = (Get-FileHash $bridgePath -Algorithm SHA256).Hash.ToLowerInvariant()
                sizeBytes = (Get-Item $bridgePath).Length
            }
            responseCenter = [ordered]@{
                relativePath = "ResponseCenter/TiaAgent.ResponseCenter.exe"
                version = $ProductVersion
                sha256Hash = (Get-FileHash $responseCenterPath -Algorithm SHA256).Hash.ToLowerInvariant()
                sizeBytes = (Get-Item $responseCenterPath).Length
            }
            addin = [ordered]@{
                relativePath = "AddIn/$addInName"
                version = $ProductVersion
                sha256Hash = (Get-FileHash $addInPath -Algorithm SHA256).Hash.ToLowerInvariant()
                sizeBytes = (Get-Item $addInPath).Length
            }
        }
        files = $files
    }

    $manifest | ConvertTo-Json -Depth 6 | Set-Content (Join-Path $PayloadDir "payload-manifest.json") -Encoding UTF8
}

function Test-NuGetPayload {
    param([Parameter(Mandatory = $true)][string]$PackagePath)

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($PackagePath)
    try {
        $entries = @($archive.Entries | ForEach-Object { $_.FullName })
        $expectedAddIn = "tools/net8.0/any/payload/AddIn/TiaAgent-$ProductVersion.addin"
        $required = @(
            "tools/net8.0/any/payload/payload-manifest.json",
            "tools/net8.0/any/payload/Bridge/TiaAgent.Bridge.dll",
            "tools/net8.0/any/payload/ResponseCenter/TiaAgent.ResponseCenter.exe",
            "tools/net8.0/any/payload/ResponseCenter/TiaAgent.ResponseCenter.runtimeconfig.json",
            $expectedAddIn,
            "tools/net8.0/any/payload/notices/THIRD_PARTY_NOTICES.md",
            "tools/net8.0/any/payload/notices/LICENSE"
        )

        foreach ($entry in $required) {
            if ($entries -notcontains $entry) {
                throw "NuGet package is missing required payload entry: $entry"
            }
        }

        if (-not ($entries | Where-Object { $_ -like "tools/net8.0/any/payload/config/*" })) {
            throw "NuGet package does not contain configuration templates"
        }

        if ($entries | Where-Object { $_ -like "tools/net8.0/any/payload/Bridge/Siemens.*.dll" }) {
            throw "NuGet payload must not contain Siemens runtime assemblies"
        }
    } finally {
        $archive.Dispose()
    }
}

function Test-NuGetInstall {
    param([Parameter(Mandatory = $true)][string]$PackagePath)

    $installDir = Join-Path $ArtifactsDir "package-install-test"
    if (Test-Path $installDir) {
        Remove-Item $installDir -Recurse -Force
    }

    & dotnet tool install TiaAgent.Cli --tool-path $installDir --version $ProductVersion --add-source $ArtifactsDir --ignore-failed-sources
    if ($LASTEXITCODE -ne 0) {
        throw "Produced NuGet package could not be installed"
    }

    Remove-Item $installDir -Recurse -Force
}

function Invoke-PackNuGet {
    Write-Header "PACK NUGET $ProductVersion"

    if (-not (Test-Path $ArtifactsDir)) {
        New-Item -ItemType Directory -Path $ArtifactsDir -Force | Out-Null
    }

    $payloadDir = Join-Path $ArtifactsDir "cli-payload"
    if (Test-Path $payloadDir) {
        Remove-Item $payloadDir -Recurse -Force
    }

    New-Item -ItemType Directory -Path "$payloadDir\Bridge", "$payloadDir\ResponseCenter", "$payloadDir\AddIn", "$payloadDir\config", "$payloadDir\notices" -Force | Out-Null

    Invoke-Dotnet @("publish", "$Root\src\TiaAgent.Bridge\TiaAgent.Bridge.csproj", "--configuration", $Configuration, "--output", "$payloadDir\Bridge", "--no-restore")
    Get-ChildItem "$payloadDir\Bridge" -Filter "Siemens.*.dll" -File -ErrorAction SilentlyContinue | Remove-Item -Force

    Invoke-Dotnet @("publish", "$Root\src\TiaAgent.ResponseCenter\TiaAgent.ResponseCenter.csproj", "--configuration", $Configuration, "--output", "$payloadDir\ResponseCenter", "--no-restore")

    $addInPath = Join-Path $ArtifactsDir "TiaAgent-$ProductVersion.addin"
    if (-not (Test-Path $addInPath)) {
        throw "Add-In artifact not found: $addInPath"
    }
    Copy-Item $addInPath "$payloadDir\AddIn\" -Force

    Copy-Item "$Root\config\*" "$payloadDir\config\" -Recurse -Force
    Copy-Item "$Root\THIRD_PARTY_NOTICES.md" "$payloadDir\notices\" -Force
    Copy-Item "$Root\LICENSE" "$payloadDir\notices\" -Force

    New-PayloadManifest -PayloadDir $payloadDir

    Get-ChildItem $ArtifactsDir -Filter "TiaAgent.Cli.*.nupkg" -File -ErrorAction SilentlyContinue | Remove-Item -Force
    Invoke-Dotnet @("pack", "$Root\src\TiaAgent.Cli\TiaAgent.Cli.csproj", "--configuration", $Configuration, "--output", $ArtifactsDir, "--no-restore")

    $packagePath = Join-Path $ArtifactsDir "TiaAgent.Cli.$ProductVersion.nupkg"
    if (-not (Test-Path $packagePath)) {
        throw "Expected NuGet package not found: $packagePath"
    }

    Test-NuGetPayload -PackagePath $packagePath
    Test-NuGetInstall -PackagePath $packagePath
    Write-Ok "NuGet package created and installation-tested: $(Split-Path $packagePath -Leaf)"
}

function Invoke-PackArtifacts {
    param([switch]$RequireSigning)

    Invoke-PackAddIn -RequireSigning:$RequireSigning
    Invoke-VerifyAddIn -RequireSigning:$RequireSigning
    Invoke-PackNuGet
}

function Invoke-Pack {
    Invoke-Build
    Invoke-PackArtifacts
}

function Invoke-Release {
    if ($ProductVersion -notmatch "^$ReleaseVersionPattern$") {
        throw "Release requires a valid version such as 0.3.0-beta.1, 0.3.0-rc.1, or 0.3.0. Resolved: $ProductVersion"
    }

    Invoke-Clean
    Invoke-Build
    Invoke-Test -NoRestore
    Invoke-PackArtifacts -RequireSigning
    Write-Ok "Release $ProductVersion is ready in $ArtifactsDir"
}

function Invoke-InstallDev {
    Invoke-Build
    Invoke-PackAddIn
    Invoke-VerifyAddIn
    Invoke-PackNuGet

    $cliDll = "$Root\src\TiaAgent.Cli\bin\$Configuration\net8.0\TiaAgent.Cli.dll"
    $payloadDir = Join-Path $ArtifactsDir "cli-payload"
    & dotnet $cliDll install --version $ProductVersion --payload-dir $payloadDir --force
    if ($LASTEXITCODE -ne 0) {
        throw "Development product installation failed with exit code $LASTEXITCODE"
    }

    Write-Ok "Development product installed with Add-In and Response Center"
}

function Show-Help {
    Write-Header "TIA PORTAL CODE AGENT"
    Write-Host "Usage: .\build.ps1 <command> [-Version X.Y.Z[-channel.N]]"
    Write-Host ""
    Write-Host "Commands:"
    Write-Host "  build        Restore and compile the solution"
    Write-Host "  test         Restore and run the test suite"
    Write-Host "  pack         Build the product, package the Add-In and create the NuGet package"
    Write-Host "  release      Clean, build, test, sign, verify and package a release"
    Write-Host "  install-dev  Build, package and install the complete local product"
    Write-Host "  clean        Remove bin, obj and artifacts"
    Write-Host ""
    Write-Host "Resolved version: $ProductVersion"
}

switch ($Command) {
    "build" { Invoke-Build }
    "test" { Invoke-Test }
    "pack" { Invoke-Pack }
    "release" { Invoke-Release }
    "install-dev" { Invoke-InstallDev }
    "clean" { Invoke-Clean }
    default { Show-Help }
}
