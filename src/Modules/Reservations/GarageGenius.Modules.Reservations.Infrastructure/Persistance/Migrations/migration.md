cd .\WebApi\GarageGenius.WebApi\
dotnet ef migrations add Initial --verbose --context ReservationsDbContext --project ..\..\Modules\Reservations\GarageGenius.Modules.Reservations.Infrastructure\ --output-dir Persistance\Migrations
dotnet ef database update --context ReservationsDbContext