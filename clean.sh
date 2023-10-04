cd Sfumato
rm -r bin
rm -r obj
dotnet restore
cd ../

cd SfumatoTests
rm -r bin
rm -r obj
dotnet restore
cd ../
