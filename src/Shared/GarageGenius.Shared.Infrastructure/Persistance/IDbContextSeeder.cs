namespace GarageGenius.Shared.Infrastructure.Persistance;
public interface IDbContextSeeder
{
    Task SeedDatabaseAsync();
}
