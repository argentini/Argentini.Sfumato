rm -r Sfumato/nupkg
source clean.sh
cd Sfumato
dotnet pack --configuration Release
cd ..

rm -r Sfumato.Cli/nupkg
source clean.sh
cd Sfumato.Cli
dotnet pack --configuration Release
cd ..
