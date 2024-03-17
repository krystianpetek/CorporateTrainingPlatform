cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context NotificationsDbContext --project ..\..\Modules\Notifications\GarageGenius.Modules.Notifications.Infrastructure\ --output-dir Persistence\Migrations
dotnet ef database update --context NotificationsDbContext