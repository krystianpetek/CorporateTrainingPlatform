using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Application;

internal static class Extensions
{
    public static IServiceCollection AddReservationsApplication(this IServiceCollection services)
    {
        return services;
    }
}