using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Booking.Core;

internal static class Extensions
{
    public static IServiceCollection AddBookingCore(this IServiceCollection services)
    {
        return services;
    }
}