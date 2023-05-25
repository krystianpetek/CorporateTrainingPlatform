cd .\WebApi\GarageGenius.WebApi\

## Add migrations
dotnet ef migrations add Initial --verbose --context ReservationsDbContext --project ..\..\Modules\Reservations\GarageGenius.Modules.Reservations.Infrastructure\ --output-dir Persistance\Migrations

## Update database
dotnet ef database update --context ReservationsDbContext

## Remove migrations
dotnet ef migrations remove --context ReservationsDbContext --project ..\..\Modules\Reservations\GarageGenius.Modules.Reservations.Infrastructure\