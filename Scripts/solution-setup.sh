#!/bin/bash
mkdir $1-solution
cd $1-solution
mkdir -p src/API src/Models src/Services/Data src/Services/Services/Interface src/Services/Services/Implementation src/Entities/Models/DTOModels src/Entities/Models/ViewModels test/Test-Library
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
touch setup-database.sh
cat << EOT >> setup-database.sh
#!/bin/bash
if [ -z "$1" ]
  then
    name="Initial"
  else
    name=$1
fi
dotnet ef --startup-project ../API/ database drop
dotnet ef --startup-project ../API/ migrations remove
dotnet ef --startup-project ../API/ migrations add $name -o Data/Migrations
dotnet ef --startup-project ../API/ database update
EOT
chmod +x setup-database.sh
dotnet new -t lib
dotnet restore
dotnet build
cd ../Entities
dotnet new -t lib
dotnet restore
dotnet build
