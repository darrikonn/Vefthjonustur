#!/bin/bash
mkdir $1-solution
cd $1-solution
mkdir -p src/API src/Models src/Services src/Entities test/test-library
touch global.json
cat << EOT >> global.json
{
    "projects": [
        "src",
        "test"
    ]
}
EOT
cd test/test-library
dotnet new -t xunittest
dotnet restore
dotnet build
cd ../../src/API
dotnet new -t Web
dotnet restore
dotnet build
cd ../Models
dotnet new -t lib
dotnet restore
dotnet build
cd ../Services
dotnet new -t lib
dotnet restore
dotnet build
cd ../Entities
dotnet new -t lib
dotnet restore
dotnet build
