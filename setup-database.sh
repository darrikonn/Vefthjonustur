#!/bin/bash
dotnet ef --startup-project ../API/ database drop
dotnet ef --startup-project ../API/ migrations remove
dotnet ef --startup-project ../API/ migrations add $1
dotnet ef --startup-project ../API/ database update
