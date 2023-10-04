if (Test-Path ".\Argentini.Sfumato\nupkg") { Remove-Item ".\Argentini.Sfumato\nupkg" -Recurse -Force }
. ./clean.ps1
Set-Location Argentini.Sfumato
dotnet pack
Set-Location ..
