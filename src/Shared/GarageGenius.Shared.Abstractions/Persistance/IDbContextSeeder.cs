namespace GarageGenius.Shared.Abstractions.Persistance;
public interface IDbContextSeeder
{
	Task SeedDatabaseAsync();
}
