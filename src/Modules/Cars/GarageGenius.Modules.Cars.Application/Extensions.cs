using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Cars.Api")]
namespace GarageGenius.Modules.Cars.Application;

internal static class Extensions
{
    public static IServiceCollection AddCarsApplication(this IServiceCollection services)
    {
        return services;
    }
}