using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Cars.Api")]
namespace GarageGenius.Modules.Cars.Core;

internal static class Extensions
{
    public static IServiceCollection AddCarsCore(this IServiceCollection services)
    {
        return services;
    }
}