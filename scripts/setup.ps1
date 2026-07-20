#Requires -Version 5.1
<#
.SYNOPSIS
    Full setup orchestrator for TIA Portal Code Agent.

.DESCRIPTION
    Automates the end-to-end setup: environment check, build, test, package,
    install Add-In, configure OpenCode MCP, copy agent profiles, and verify.

.PARAMETER SkipBuild
    Skip the build step (useful if already built).

.PARAMETER SkipTests
    Skip running tests.

.PARAMETER SkipInstall
    Skip copying the .addin to TIA Portal.

.PARAMETER McpPort
    MCP server port. Default: 43121.

.PARAMETER OpenCodePort
    OpenCode server port. Default: 43120.

.PARAMETER OpenCodeUrl
    OpenCode server URL. Default: http://127.0.0.1:43120.

.PARAMETER OpenCodeHome
    Path to OpenCode home directory. Auto-detected if not set.

.PARAMETER ModelProvider
    Model provider for OpenCode. Default: openai.

.PARAMETER ModelName
    Model name for OpenCode. Default: gpt-4o.

.EXAMPLE
    .\scripts\setup.ps1

.EXAMPLE
    .\scripts\setup.ps1 -SkipTests -ModelProvider anthropic -ModelName claude-sonnet-4-20250514

.EXAMPLE
    .\scripts\setup.ps1 -OpenCodeHome "C:\Users\me\.opencode" -McpPort 50000
#>
param(
    [switch]$SkipBuild,
    [switch]$SkipTests,
    [switch]$SkipInstall,
    [int]$McpPort = 43121,
    [int]$OpenCodePort = 43120,
    [string]$OpenCodeUrl = "http://127.0.0.1:43120",
    [string]$OpenCodeHome,
    [string]$ModelProvider = "openai",
    [string]$ModelName = "gpt-4o"
)

$ErrorActionPreference = "Stop"
$Root = Split-Path $PSScriptRoot -Parent
$Version = "0.1.0"

# ============================================================
# Helpers
# ============================================================

function Write-Header($text) {
    Write-Host ""
    Write-Host "======================================" -ForegroundColor Cyan
    Write-Host "  $text" -ForegroundColor Cyan
    Write-Host "======================================" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Step($step, $total, $text) {
    Write-Host "[$step/$total] $text" -ForegroundColor Yellow
}

function Write-Ok($text) {
    Write-Host "  OK: $text" -ForegroundColor Green
}

function Write-Fail($text) {
    Write-Host "  FAIL: $text" -ForegroundColor Red
}

function Write-Warn($text) {
    Write-Host "  WARN: $text" -ForegroundColor Yellow
}

function Write-Info($text) {
    Write-Host "  $text" -ForegroundColor Gray
}

function Write-SubHeader($text) {
    Write-Host ""
    Write-Host "--- $text ---" -ForegroundColor Magenta
    Write-Host ""
}

# ============================================================
# Auto-detect OpenCode home
# ============================================================

function Get-OpenCodeHome {
    if ($OpenCodeHome -and (Test-Path $OpenCodeHome)) {
        return $OpenCodeHome
    }

    # Standard locations
    $candidates = @(
        "$env:USERPROFILE\.opencode",
        "$env:USERPROFILE\.config\opencode",
        "$env:APPDATA\opencode"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path $candidate) {
            Write-Info "OpenCode home found: $candidate"
            return $candidate
        }
    }

    return $null
}

# ============================================================
# PHASE 1: Environment Verification
# ============================================================

function Test-Environment {
    Write-Header "PHASE 1: Environment Verification"
    $failed = $false

    # .NET SDK
    Write-SubHeader ".NET SDK"
    try {
        $sdkVersion = dotnet --version 2>&1
        if ($LASTEXITCODE -eq 0) {
            Write-Ok ".NET SDK: $sdkVersion"
        } else {
            Write-Fail ".NET SDK not found"
            $failed = $true
        }
    } catch {
        Write-Fail ".NET SDK not found"
        $failed = $true
    }

    # .NET Framework 4.8
    Write-SubHeader ".NET Framework 4.8"
    $fwKey = "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"
    if (Test-Path $fwKey) {
        $release = (Get-ItemProperty $fwKey -Name Release -ErrorAction SilentlyContinue).Release
        if ($release -ge 528040) {
            Write-Ok ".NET Framework 4.8+ installed (Release: $release)"
        } else {
            Write-Warn ".NET Framework found but release $release < 528040"
        }
    } else {
        Write-Fail ".NET Framework 4.8 not found"
        $failed = $true
    }

    # TIA Portal V21
    Write-SubHeader "TIA Portal V21"
    $tiaPath = "C:\Program Files\Siemens\Automation\Portal V21"
    if (Test-Path $tiaPath) {
        Write-Ok "TIA Portal V21 installed"
    } else {
        Write-Warn "TIA Portal V21 not found at $tiaPath"
        Write-Info "Add-In packaging requires TIA Portal V21 Publisher"
    }

    # Openness Assemblies
    Write-SubHeader "Openness Assemblies"
    $publicApiDir = "C:\Program Files\Siemens\Automation\Portal V21\PublicAPI\V21\net48"
    if (Test-Path $publicApiDir) {
        $dlls = Get-ChildItem "$publicApiDir\Siemens.Engineering*.dll" -ErrorAction SilentlyContinue
        if ($dlls.Count -gt 0) {
            Write-Ok "$($dlls.Count) Siemens assemblies found"
        } else {
            Write-Warn "PublicAPI directory exists but no Siemens DLLs found"
        }
    } else {
        Write-Warn "PublicAPI directory not found"
    }

    # Openness Group
    Write-SubHeader "Openness Group Membership"
    try {
        $groups = ([System.Security.Principal.WindowsIdentity]::GetCurrent()).Groups |
            ForEach-Object { $_.Translate([System.Security.Principal.NTAccount]).Value }
        $inGroup = $groups | Where-Object { $_ -match "Siemens TIA Openness" }
        if ($inGroup) {
            Write-Ok "User is member of 'Siemens TIA Openness' group"
        } else {
            Write-Warn "User may not be in 'Siemens TIA Openness' group"
            Write-Info "If Openness calls fail with EngineeringSecurityException, add user to the group"
        }
    } catch {
        Write-Warn "Could not verify group membership"
    }

    # Port availability
    Write-SubHeader "Port Availability"
    foreach ($port in @($McpPort, $OpenCodePort)) {
        $connection = Get-NetTCPConnection -LocalPort $port -ErrorAction SilentlyContinue
        if ($connection) {
            Write-Warn "Port $port is in use"
        } else {
            Write-Ok "Port $port is available"
        }
    }

    # OpenCode
    Write-SubHeader "OpenCode"
    $ocHome = Get-OpenCodeHome
    if ($ocHome) {
        Write-Ok "OpenCode home: $ocHome"
    } else {
        Write-Warn "OpenCode home not found"
        Write-Info "Install OpenCode and run once, or pass -OpenCodeHome"
        Write-Info "See: https://github.com/anthropics/opencode"
    }

    if ($failed) {
        Write-Host ""
        Write-Fail "Critical prerequisites missing. Fix the FAIL items above and re-run."
        exit 1
    }

    Write-Host ""
    Write-Ok "Environment verification complete"
}

# ============================================================
# PHASE 2: Restore & Build
# ============================================================

function Invoke-BuildPhase {
    Write-Header "PHASE 2: Restore & Build"

    Write-Step 1 3 "Restoring NuGet packages..."
    dotnet restore "$Root\TiaAgent.sln" --verbosity quiet
    if ($LASTEXITCODE -ne 0) { Write-Fail "Restore failed"; exit 1 }
    Write-Ok "Packages restored"

    if (-not $SkipBuild) {
        Write-Step 2 3 "Building solution (Release)..."
        dotnet build "$Root\TiaAgent.sln" --configuration Release --verbosity quiet
        if ($LASTEXITCODE -ne 0) { Write-Fail "Build failed"; exit 1 }
        Write-Ok "Solution compiled"

        # Verify output
        $dlls = Get-ChildItem "$Root\src\*\bin\Release\*\*.dll" -Recurse -ErrorAction SilentlyContinue
        Write-Ok "$($dlls.Count) DLLs generated"
    } else {
        Write-Info "Build skipped (-SkipBuild)"
    }

    if (-not $SkipTests) {
        Write-Step 3 3 "Running tests..."
        dotnet test "$Root\TiaAgent.sln" --configuration Release --verbosity normal
        if ($LASTEXITCODE -ne 0) { Write-Fail "Tests failed"; exit 1 }
        Write-Ok "All tests passed"
    } else {
        Write-Step 3 3 "Tests skipped (-SkipTests)"
    }
}

# ============================================================
# PHASE 3: Package Add-In
# ============================================================

function New-AddInPackage {
    Write-Header "PHASE 3: Package Add-In"

    $packDir = "$Root\artifacts"
    $addinDir = "$packDir\TiaAgent.AddIn"
    $versionTag = $Version -replace '\.', '-'
    $addinFile = "$packDir\TiaAgent-$versionTag.addin"

    # Clean previous
    Write-Step 1 5 "Cleaning previous packages..."
    if (Test-Path $addinDir) { Remove-Item $addinDir -Recurse -Force }
    if (Test-Path $addinFile) { Remove-Item $addinFile -Force }
    New-Item -ItemType Directory -Path $addinDir -Force | Out-Null
    Write-Ok "Clean"

    # Copy DLLs
    Write-Step 2 5 "Copying Add-In assemblies..."
    $addinBin = "$Root\src\TiaAgent.AddIn\bin\Release\net48"
    if (Test-Path $addinBin) {
        Copy-Item "$addinBin\*.dll" $addinDir -ErrorAction SilentlyContinue
    } else {
        Write-Fail "Add-In build output not found: $addinBin"
        exit 1
    }

    $deps = @("TiaAgent.Contracts", "TiaAgent.Application", "TiaAgent.Simulator", "TiaAgent.OpenCode")
    foreach ($dep in $deps) {
        $depBin = "$Root\src\$dep\bin\Release\netstandard2.0"
        if (Test-Path $depBin) {
            Copy-Item "$depBin\*.dll" $addinDir -ErrorAction SilentlyContinue
        }
    }
    Write-Ok "Assemblies copied"

    # Generate Config.xml
    Write-Step 3 5 "Generating Config.xml..."
    $configXml = @"
<?xml version="1.0" encoding="utf-8"?>
<PackageConfiguration xmlns="http://www.siemens.com/automation/Openness/AddIn/Publisher/V21">
  <Author>TIA Agent Project</Author>
  <Description>AI-powered engineering assistant for TIA Portal V21</Description>
  <AddInVersion>$Version</AddInVersion>
  <Product>
    <Name>TIA Portal Code Agent</Name>
    <Id>tia-portal-code-agent</Id>
    <Version>$Version.0</Version>
  </Product>
  <FeatureAssembly>
    <AssemblyInfo>
      <Assembly>TiaAgent.AddIn.dll</Assembly>
    </AssemblyInfo>
  </FeatureAssembly>
  <AdditionalAssemblies>
    <AssemblyInfo>
      <Assembly>TiaAgent.Contracts.dll</Assembly>
    </AssemblyInfo>
    <AssemblyInfo>
      <Assembly>TiaAgent.Application.dll</Assembly>
    </AssemblyInfo>
    <AssemblyInfo>
      <Assembly>TiaAgent.Simulator.dll</Assembly>
    </AssemblyInfo>
    <AssemblyInfo>
      <Assembly>TiaAgent.OpenCode.dll</Assembly>
    </AssemblyInfo>
  </AdditionalAssemblies>
  <RequiredPermissions>
    <TIAPermissions>
      <TIA.ReadOnly />
    </TIAPermissions>
  </RequiredPermissions>
</PackageConfiguration>
"@
    $configXml | Out-File -FilePath "$addinDir\Config.xml" -Encoding UTF8
    Write-Ok "Config.xml generated"

    # Run Publisher
    Write-Step 4 5 "Running Siemens Publisher..."
    $publisher = "C:\Program Files\Siemens\Automation\Portal V21\PublicAPI\V21\Siemens.Engineering.AddIn.Publisher.exe"
    if (Test-Path $publisher) {
        & $publisher --configuration "$addinDir\Config.xml" --outfile $addinFile --console 2>&1 | ForEach-Object { Write-Info $_ }
        if ($LASTEXITCODE -ne 0) { Write-Fail "Publisher failed"; exit 1 }
        Write-Ok ".addin created: $addinFile"
    } else {
        Write-Warn "Publisher not found, creating ZIP fallback"
        $tempZip = "$packDir\temp.zip"
        Compress-Archive -Path "$addinDir\*" -DestinationPath $tempZip -Force
        Move-Item $tempZip $addinFile -Force
        Write-Ok ".addin (ZIP fallback): $addinFile"
    }

    # Summary
    Write-Step 5 5 "Package summary"
    $size = (Get-Item $addinFile).Length / 1KB
    Write-Info "File: $addinFile"
    Write-Info "Size: $([math]::Round($size, 1)) KB"
    Write-Info "Version: $Version"
}

# ============================================================
# PHASE 4: Install Add-In to TIA Portal
# ============================================================

function Install-AddIn {
    Write-Header "PHASE 4: Install Add-In"

    if ($SkipInstall) {
        Write-Info "Install skipped (-SkipInstall)"
        return
    }

    $userAddIns = "$env:APPDATA\Siemens\Automation\Portal V21\UserAddIns"
    $versionTag = $Version -replace '\.', '-'
    $addinFile = "$Root\artifacts\TiaAgent-$versionTag.addin"

    if (!(Test-Path $userAddIns)) {
        Write-Warn "Add-Ins folder not found: $userAddIns"
        Write-Info "Run TIA Portal V21 at least once to create it"
        Write-Info "Or manually create: $userAddIns"
        return
    }

    if (!(Test-Path $addinFile)) {
        Write-Fail ".addin not found: $addinFile"
        Write-Info "Run packaging first"
        return
    }

    Write-Step 1 2 "Copying to Add-Ins folder..."
    Copy-Item $addinFile $userAddIns -Force
    Write-Ok "Copied to: $userAddIns"

    Write-Step 2 2 "Installation"
    Write-Host ""
    Write-Host "  To activate the Add-In:" -ForegroundColor Cyan
    Write-Host "    1. Open TIA Portal V21" -ForegroundColor White
    Write-Host "    2. Go to Options > Settings > Add-Ins" -ForegroundColor White
    Write-Host "    3. Activate 'TIA Portal Code Agent'" -ForegroundColor White
    Write-Host ""
}

# ============================================================
# PHASE 5: Configure OpenCode
# ============================================================

function Set-OpenCodeConfig {
    Write-Header "PHASE 5: Configure OpenCode"

    $ocHome = Get-OpenCodeHome

    if (-not $ocHome) {
        Write-Warn "OpenCode home not found. Skipping MCP configuration."
        Write-Info "After installing OpenCode, manually configure:"
        Write-Info "  Add to your OpenCode config:"
        Write-Info '  "mcp": {'
        Write-Info '    "servers": {'
        Write-Info '      "tia-agent": {'
        Write-Info "        `"url`": `"http://127.0.0.1:${McpPort}/mcp`""
        Write-Info '        "transport": "streamable-http"'
        Write-Info "      }"
        Write-Info "    }"
        Write-Info "  }"
        return
    }

    # Copy agent profiles to OpenCode agents directory
    Write-Step 1 4 "Copying agent profiles..."
    $agentsDir = "$ocHome\agents"
    if (!(Test-Path $agentsDir)) {
        New-Item -ItemType Directory -Path $agentsDir -Force | Out-Null
    }

    $agentFiles = @("tia-explain.md", "tia-review.md", "tia-change.md")
    foreach ($agent in $agentFiles) {
        $src = "$Root\agents\$agent"
        $dst = "$agentsDir\$agent"
        if (Test-Path $src) {
            Copy-Item $src $dst -Force
            Write-Ok "Copied $agent"
        }
    }

    # Generate/update OpenCode MCP config
    Write-Step 2 4 "Configuring MCP connection..."

    # Find the global OpenCode config (opencode.jsonc or opencode.json)
    $ocConfigCandidates = @(
        "$ocHome\opencode.jsonc",
        "$ocHome\opencode.json",
        "$ocHome\config.jsonc",
        "$ocHome\config.json"
    )
    $ocConfigPath = $ocConfigCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1

    $mcpUrl = "http://127.0.0.1:${McpPort}/mcp"
    $mcpInsert = '  "mcp": { "servers": { "tia-agent": { "url": "' + $mcpUrl + '", "transport": "streamable-http" } } }'

    if ($ocConfigPath) {
        Write-Info "Found OpenCode config: $ocConfigPath"
        $content = Get-Content $ocConfigPath -Raw

        if ($content -match '"mcp"') {
            Write-Info "MCP configuration already present, updating..."
            # Simple text replacement for the tia-agent url
            $content = $content -replace '("url"\s*:\s*")([^"]*tia-agent[^"]*)(")', "`$1$mcpUrl`$3"
            $content | Out-File $ocConfigPath -Encoding UTF8 -NoNewline
            Write-Ok "Updated MCP config in $ocConfigPath"
        } else {
            # Insert MCP block before the last closing brace
            $insertPoint = $content.LastIndexOf("}")
            if ($insertPoint -gt 0) {
                $before = $content.Substring(0, $insertPoint).TrimEnd()
                $after = $content.Substring($insertPoint)
                if (-not $before.EndsWith(",") -and -not $before.EndsWith("{")) {
                    $before = $before + ","
                }
                $newContent = $before + "`n" + $mcpInsert + "`n" + $after
                $newContent | Out-File $ocConfigPath -Encoding UTF8 -NoNewline
                Write-Ok "Injected MCP config into $ocConfigPath"
            } else {
                Write-Warn "Could not parse $ocConfigPath to inject MCP config"
            }
        }
    } else {
        Write-Warn "No OpenCode config found in $ocHome"
        Write-Info "Expected: opencode.jsonc or opencode.json"
        Write-Info "After creating it, add this MCP block:"
        Write-Host ""
        Write-Host "  $mcpInsert" -ForegroundColor Gray
        Write-Host ""
    }

    # Generate local project config files from examples
    Write-Step 3 4 "Creating local configuration files..."
    $configDir = "$Root\config"
    $localConfigs = @(
        @{ Source = "appsettings.example.json"; Target = "appsettings.json" },
        @{ Source = "opencode.example.json"; Target = "opencode.json" }
    )
    foreach ($cfg in $localConfigs) {
        $sourcePath = Join-Path $configDir $cfg.Source
        $targetPath = Join-Path $configDir $cfg.Target
        if (!(Test-Path $targetPath) -and (Test-Path $sourcePath)) {
            Copy-Item $sourcePath $targetPath
            Write-Ok "Created $($cfg.Target)"
        } else {
            Write-Info "$($cfg.Target) already exists, skipping"
        }
    }

    # Update opencode.json with actual ports
    $projectOcConfig = "$configDir\opencode.json"
    if (Test-Path $projectOcConfig) {
        $oc = Get-Content $projectOcConfig -Raw | ConvertFrom-Json
        $oc.server.port = $OpenCodePort
        $oc.server.url = $OpenCodeUrl
        $oc.mcp.servers."tia-agent".url = "http://127.0.0.1:${McpPort}/mcp"
        $oc.model.provider = $ModelProvider
        $oc.model.model = $ModelName
        $oc | ConvertTo-Json -Depth 10 | Out-File $projectOcConfig -Encoding UTF8
        Write-Ok "Updated opencode.json with custom ports and model"
    }

    Write-Step 4 4 "Configuration summary"
    Write-Info "MCP endpoint: http://127.0.0.1:${McpPort}/mcp"
    Write-Info "OpenCode server: $OpenCodeUrl"
    Write-Info "Model: $ModelProvider/$ModelName"
}

# ============================================================
# PHASE 6: Final Verification
# ============================================================

function Test-Final {
    Write-Header "PHASE 6: Final Verification"
    $allOk = $true

    # Check solution builds
    Write-Step 1 3 "Build verification..."
    $buildOut = dotnet build "$Root\TiaAgent.sln" --configuration Release --verbosity quiet 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Ok "Solution builds"
    } else {
        Write-Fail "Solution build failed"
        $allOk = $false
    }

    # Check package exists
    Write-Step 2 3 "Package verification..."
    $versionTag = $Version -replace '\.', '-'
    $addinFile = "$Root\artifacts\TiaAgent-$versionTag.addin"
    if (Test-Path $addinFile) {
        $size = (Get-Item $addinFile).Length / 1KB
        Write-Ok "Package exists ($([math]::Round($size, 1)) KB)"
    } else {
        Write-Warn "Package not found (packaging may have been skipped)"
    }

    # Check config files
    Write-Step 3 3 "Config file verification..."
    $requiredFiles = @(
        "config\appsettings.json",
        "config\opencode.json"
    )
    foreach ($f in $requiredFiles) {
        if (Test-Path "$Root\$f") {
            Write-Ok "$f"
        } else {
            Write-Warn "$f missing"
        }
    }

    if ($allOk) {
        Write-Host ""
        Write-Host "======================================" -ForegroundColor Green
        Write-Host "  SETUP COMPLETE" -ForegroundColor Green
        Write-Host "====================================%" -ForegroundColor Green
    }
}

# ============================================================
# MAIN
# ============================================================

Write-Host ""
Write-Host "  TIA Portal Code Agent - Full Setup" -ForegroundColor Cyan
Write-Host "  Version: $Version" -ForegroundColor Gray
Write-Host ""

# Run phases
Test-Environment
Invoke-BuildPhase
New-AddInPackage
Install-AddIn
Set-OpenCodeConfig
Test-Final

# ============================================================
# Next Steps
# ============================================================

Write-Host ""
Write-Host "======================================" -ForegroundColor Cyan
Write-Host "  NEXT STEPS" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "  1. Activate the Add-In in TIA Portal V21:" -ForegroundColor White
Write-Host "     Options > Settings > Add-Ins > TIA Portal Code Agent" -ForegroundColor Gray
Write-Host ""
Write-Host "  2. Start the MCP server:" -ForegroundColor White
Write-Host "     .\scripts\run-mcp.ps1" -ForegroundColor Gray
Write-Host "     or: .\build.ps1 run" -ForegroundColor Gray
Write-Host ""
Write-Host "  3. OpenCode will connect to the MCP server automatically" -ForegroundColor White
Write-Host "     via the configured Streamable HTTP transport." -ForegroundColor Gray
Write-Host ""
Write-Host "  4. In TIA Portal, right-click an object and use:" -ForegroundColor White
Write-Host "     'Explain selected object' context menu action" -ForegroundColor Gray
Write-Host ""
Write-Host "  Quick test (simulator only, no TIA needed):" -ForegroundColor Yellow
Write-Host "     .\scripts\run-simulator.ps1" -ForegroundColor Gray
Write-Host "     Then connect from OpenCode or any MCP client." -ForegroundColor Gray
Write-Host ""
