cd Sfumato
rm -r bin
rm -r obj
dotnet restore
cd ../

cd Sfumato.Cli
rm -r bin
rm -r obj
dotnet restore
cd ../

cd Sfumato.Tests
rm -r bin
rm -r obj
dotnet restore
cd ../

cd SampleWebsite
rm -r bin
rm -r obj
dotnet restore
cd ../
