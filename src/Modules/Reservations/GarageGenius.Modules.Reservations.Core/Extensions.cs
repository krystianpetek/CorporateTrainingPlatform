using GarageGenius.Modules.Reservations.Core.Reservations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Core;

internal static class Extensions
{
	public static IServiceCollection AddReservationsCore(this IServiceCollection services)
	{
		services.AddScoped<IReservationDomainService, ReservationDomainService>();
		return services;
	}
}