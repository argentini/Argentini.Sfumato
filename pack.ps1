if (Test-Path ".\Sfumato.Cli\nupkg") { Remove-Item ".\Sfumato.Cli\nupkg" -Recurse -Force }
. ./clean.ps1
Set-Location Sfumato.Cli
dotnet pack --configuration Release
Set-Location ..

if (Test-Path ".\Sfumato\nupkg") { Remove-Item ".\Sfumato\nupkg" -Recurse -Force }
. ./clean.ps1
Set-Location Sfumato
dotnet pack --configuration Release
Set-Location ..
