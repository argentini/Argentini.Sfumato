. ./clean.ps1

if (Test-Path ".\Sfumato.Cli\nupkg") { Remove-Item ".\Sfumato.Cli\nupkg" -Recurse -Force }
Set-Location Sfumato.Cli
dotnet pack --configuration Release
Set-Location ..

if (Test-Path ".\Sfumato\nupkg") { Remove-Item ".\Sfumato\nupkg" -Recurse -Force }
Set-Location Sfumato
dotnet pack --configuration Release
Set-Location ..

. ./clean.ps1
