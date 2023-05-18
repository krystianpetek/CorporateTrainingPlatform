using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Booking.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddBookingInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}