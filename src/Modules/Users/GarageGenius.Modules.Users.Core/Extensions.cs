using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Users.Api")]
namespace GarageGenius.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddUsersModuleCore(this IServiceCollection services)
    {
        services.AddSqlServerDbContext<UsersDbContext>();
        return services;
    }
}