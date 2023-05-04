using GarageGenius.Shared.Abstractions.Persistance;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Api")]
namespace GarageGenius.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCustomersModuleInfrastructure(this IServiceCollection services)
    {

        return services;
    }
}