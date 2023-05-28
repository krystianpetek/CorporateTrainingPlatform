using GarageGenius.Modules.Reservations.Application;
using GarageGenius.Modules.Reservations.Core;
using GarageGenius.Modules.Reservations.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Api;
internal class ReservationsModule : IModule
{
	public const string BasePath = "reservations-module";
	public string Name => "Reservations";
	public IEnumerable<string>? Policies { get; } = new string[] { "reservations" };

	public void Register(IServiceCollection services)
	{
		services.AddReservationsCore();
		services.AddReservationsApplication();
		services.AddReservationsInfrastructure();
	}

	public void Use(WebApplication app)
	{
		app.MapHealthCheck(Name);
	}
}
