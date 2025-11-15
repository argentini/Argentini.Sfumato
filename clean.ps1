Set-Location Sfumato
if (Test-Path ".\bin") { Remove-Item bin -Recurse -Force }
if (Test-Path ".\obj") { Remove-Item obj -Recurse -Force }
dotnet restore
Set-Location ..

Set-Location Sfumato.Cli
if (Test-Path ".\bin") { Remove-Item bin -Recurse -Force }
if (Test-Path ".\obj") { Remove-Item obj -Recurse -Force }
dotnet restore
Set-Location ..

Set-Location Sfumato.Tests
if (Test-Path ".\bin") { Remove-Item bin -Recurse -Force }
if (Test-Path ".\obj") { Remove-Item obj -Recurse -Force }
dotnet restore
Set-Location ..
