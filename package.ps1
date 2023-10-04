if (Test-Path ".\Sfumato\nupkg") { Remove-Item ".\Sfumato\nupkg" -Recurse -Force }
. ./clean.ps1
Set-Location Sfumato
dotnet pack
Set-Location ..
