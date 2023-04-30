using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;

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