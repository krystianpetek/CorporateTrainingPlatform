# How to create new migration in Modular Monolith

## Create

```powershell
cd .\Api\GarageGenius.Api\
dotnet ef migrations add Initial --context UsersDbContext --project ..\..\Modules\Users\GarageGenius.Modules.Users.Core\
```

## Remove

```powershell
dotnet ef migrations remove --context UsersDbContext --project ..\..\Modules\Users\GarageGenius.Modules.Users.Core\
```
