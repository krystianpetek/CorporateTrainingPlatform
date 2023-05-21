using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Persistance.Repositories;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Core.ServiceMapper;
using GarageGenius.Shared.Abstractions.Persistance;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Users.Core;

internal static class Extensions
{
    public static async Task<IServiceCollection> AddUsersCore(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddMsSqlServerDbContext<UsersDbContext>();
        services.AddTransient<IDbContextSeeder, UsersDbContextSeeder>();
        services.AddScoped<IUserServiceMapper, UserServiceMapper>();

        using IServiceScope? serviceScope = services.BuildServiceProvider().CreateScope();
        var dbSeeder = serviceScope.ServiceProvider.GetService<IDbContextSeeder>();
        await dbSeeder.SeedDatabaseAsync(); // TODO redesign seeder to be more generic and to better approach, idk yet
        return services;
    }
}