cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context CarsDbContext --project ..\..\Modules\Cars\GarageGenius.Modules.Cars.Infrastructure\ --output-dir Persistance\Migrations
dotnet ef database update --context CarsDbContext