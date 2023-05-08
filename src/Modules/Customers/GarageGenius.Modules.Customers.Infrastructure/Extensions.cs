using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Api")]
namespace GarageGenius.Modules.Customers.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCustomersModuleInfrastructure(this IServiceCollection services)
    {
        services.AddSqlServerDbContext<CustomersDbContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}