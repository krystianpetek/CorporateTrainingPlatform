using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Shared.Infrastructure.Persistance;
using GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Users.Api")]
namespace GarageGenius.Modules.Users.Core;

internal static class Extensions
{
    public static async Task<IServiceCollection> AddUsersModuleCore(this IServiceCollection services)
    {
        services.AddSqlServerDbContext<UsersDbContext>();
        services.AddTransient<IDbContextSeeder, UsersDbContextSeeder>();

        using IServiceScope? serviceScope = services.BuildServiceProvider().CreateScope();
        var dbSeeder = serviceScope.ServiceProvider.GetService<IDbContextSeeder>();
        await dbSeeder.SeedDatabaseAsync();
        return services;
    }
}