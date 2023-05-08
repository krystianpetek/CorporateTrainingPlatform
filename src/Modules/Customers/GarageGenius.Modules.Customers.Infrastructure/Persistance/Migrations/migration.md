cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context UsersDbContext --project ..\..\Modules\Users\GarageGenius.Modules.Users.Core\ --output-dir Persistance\Migrations
dotnet ef database update
