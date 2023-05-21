using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddReservationsInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}