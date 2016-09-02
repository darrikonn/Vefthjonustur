#!/bin/bash
if [ -z "$1" ]
  then
    name="Initial"
  else
    name=$1
fi
dotnet ef --startup-project ../API/ database drop
dotnet ef --startup-project ../API/ migrations remove
dotnet ef --startup-project ../API/ migrations add $name
dotnet ef --startup-project ../API/ database update
