using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Persistance.Repository;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Persistance;
using GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Users.Api")]
namespace GarageGenius.Modules.Users.Core;

internal static class Extensions
{
    public static async Task<IServiceCollection> AddUsersModuleCore(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSqlServerDbContext<UsersDbContext>();
        services.AddTransient<IDbContextSeeder, UsersDbContextSeeder>();

        using IServiceScope? serviceScope = services.BuildServiceProvider().CreateScope();
        var dbSeeder = serviceScope.ServiceProvider.GetService<IDbContextSeeder>();
        await dbSeeder.SeedDatabaseAsync();
        return services;
    }
}