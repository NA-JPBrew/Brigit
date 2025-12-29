$ErrorActionPreference = "Stop"

$Repo = "TeamBrigit/Brigit" 
$InstallDir = "$HOME\Brigit"
$BinDir = "$InstallDir\bin"

Write-Host "Installing Brigit CLI..." -ForegroundColor Cyan

try {
    $LatestRelease = Invoke-RestMethod "https://api.github.com/repos/$Repo/releases/latest"
    $Version = $LatestRelease.tag_name
    $DownloadUrl = $LatestRelease.assets | Where-Object { $_.name -like "Brigit.CLI*.zip" } | Select-Object -ExpandProperty browser_download_url
} catch {
    Write-Error "Failed to fetch release information. Please check the repository settings."
}

if (-not $DownloadUrl) {
    Write-Error "Release asset 'Brigit.CLI.zip' not found."
}

Write-Host "Downloading version $Version..."
$ZipPath = "$env:TEMP\Brigit.CLI.zip"
Invoke-WebRequest -Uri $DownloadUrl -OutFile $ZipPath

if (Test-Path $InstallDir) {
    Remove-Item $InstallDir -Recurse -Force
}
New-Item -ItemType Directory -Path $BinDir -Force | Out-Null

Expand-Archive -Path $ZipPath -DestinationPath $BinDir -Force
Remove-Item $ZipPath

$UserPath = [Environment]::GetEnvironmentVariable("Path", "User")
if ($UserPath -notlike "*$BinDir*") {
    Write-Host "Adding $BinDir to PATH..."
    [Environment]::SetEnvironmentVariable("Path", "$UserPath;$BinDir", "User")
    $env:Path += ";$BinDir"
    Write-Host "Path updated. Please restart your terminal." -ForegroundColor Yellow
}

Write-Host "Brigit CLI installed successfully! Run 'brigit create' to start." -ForegroundColor Green
Write-Host "Happy Hacking!" -ForegroundColor Magenta
