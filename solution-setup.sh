#!/bin/bash
mkdir $1-solution
cd $1-solution
mkdir -p src/app src/models src/services test/test-library
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
cd ../../src/app
dotnet new -t Web
dotnet restore
dotnet build
cd ../models
dotnet new -t lib
dotnet restore
dotnet build
cd ../services
dotnet new -t lib
dotnet restore
dotnet build
