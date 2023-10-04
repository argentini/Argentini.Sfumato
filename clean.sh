cd Argentini.Sfumato
rm -r bin
rm -r obj
dotnet restore
cd ../

cd Argentini.Sfumato.Tests
rm -r bin
rm -r obj
dotnet restore
cd ../
