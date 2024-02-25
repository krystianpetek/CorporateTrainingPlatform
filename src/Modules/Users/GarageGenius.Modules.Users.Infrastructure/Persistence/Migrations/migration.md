# How to create new migration in Modular Monolith

## Create

```powershell
cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context UsersDbContext --project ..\..\Modules\Users\GarageGenius.Modules.Users.Core\ --output-dir Persistence\Migrations
dotnet ef database update
```

## Remove

```powershell
dotnet ef migrations remove --context UsersDbContext --project ..\..\Modules\Users\GarageGenius.Modules.Users.Core\
```
