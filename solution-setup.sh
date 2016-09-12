#!/bin/bash
if [ -z "$1" ]
  then
    echo "Please choose a solution name (Camel casing)!"
    exit
fi

mkdir $1-solution
cd $1-solution
mkdir -p src/API src/Models/ViewModels src/Models/DTOModels src/Services/Itf src/Services/Imp src/Services/Exceptions src/Entities/Models src/Entities/Data test
touch global.json
cat << EOT >> global.json
{
    "projects": [
        "src",
        "test"
    ]
}
EOT
cd test
dotnet new -t xunittest
dotnet restore
dotnet build

cd ../../src/API
dotnet new -t Web
astyle Program.cs
astyle Startup.cs
dotnet restore
dotnet build

cd ../Models
dotnet new -t lib
rm Library.cs
cp ../../../Templates/projectModels.json project.json
dotnet restore
dotnet build

cd ../Services
dotnet new -t lib
rm Library.cs
cp ../../../Templates/projectServices.json project.json
dotnet restore
dotnet build

cd ../Entities
dotnet new -t lib
rm Library.cs
cp ../../../Scripts/setup-database.sh .
cp ../../../Templates/projectEntities.json project.json
dotnet restore
dotnet build
