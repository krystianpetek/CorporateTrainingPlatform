## Create new project
dotnet new classlib -n GarageGenius.Modules.Users.Shared

## Add project to solution
dotnet sln ../../../  add  .\GarageGenius.Modules.Users.Shared.csproj

## Create project reference
dotnet add  .\GarageGenius.Modules.Users.Shared.csproj reference ..\GarageGenius.Modules.Users.Core\GarageGenius.Modules.Users.Core.csproj
