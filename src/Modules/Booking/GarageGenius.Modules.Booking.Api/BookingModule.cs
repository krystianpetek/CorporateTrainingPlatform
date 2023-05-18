using GarageGenius.Modules.Booking.Application;
using GarageGenius.Modules.Booking.Core;
using GarageGenius.Modules.Booking.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Booking.Api;
internal class BookingModule : IModule
{
    public const string BasePath = "booking-module";
    public string Name => "Booking";
    public IEnumerable<string>? Policies { get; } = new string[] { "booking" };

    public void Register(IServiceCollection services)
    {
        services.AddBookingCore();
        services.AddBookingApplication();
        services.AddBookingInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.MapHealthCheck(Name);
    }
}
