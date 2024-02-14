rm -r Argentini.Sfumato/nupkg
source clean.sh
cd Argentini.Sfumato
dotnet pack --configuration Release
cd ..
