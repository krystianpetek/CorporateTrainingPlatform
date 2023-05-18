using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Booking.Application;

internal static class Extensions
{
    public static IServiceCollection AddBookingApplication(this IServiceCollection services)
    {
        return services;
    }
}