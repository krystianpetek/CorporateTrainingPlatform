using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Api")]
namespace GarageGenius.Modules.Customers.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCustomersInfrastructure(this IServiceCollection services)
    {
        services.AddMsSqlServerDbContext<CustomersDbContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}