#!/bin/bash
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add $1
dotnet ef database update
