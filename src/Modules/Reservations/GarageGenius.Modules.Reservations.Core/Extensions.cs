using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Core;

internal static class Extensions
{
    public static IServiceCollection AddReservationsCore(this IServiceCollection services)
    {
        return services;
    }
}