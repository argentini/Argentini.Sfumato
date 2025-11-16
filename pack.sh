source clean.sh

rm -r Sfumato/nupkg
cd Sfumato
dotnet pack --configuration Release
cd ..

rm -r Sfumato.Cli/nupkg
cd Sfumato.Cli
dotnet pack --configuration Release
cd ..

source clean.sh
