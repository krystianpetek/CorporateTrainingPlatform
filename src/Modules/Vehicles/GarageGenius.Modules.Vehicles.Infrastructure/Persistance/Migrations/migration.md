cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context VehiclesDbContext --project ..\..\Modules\Vehicles\GarageGenius.Modules.Vehicles.Infrastructure\ --output-dir Persistance\Migrations
dotnet ef database update --context VehiclesDbContext