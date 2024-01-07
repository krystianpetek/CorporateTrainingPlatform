using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.Repositories;
using GarageGenius.Modules.Reservations.Infrastructure.QueryStorage;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddReservationsInfrastructure(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddMsSqlServerDbContext<ReservationsDbContext>(webHostEnvironment);
		//services.AddPostgreSqlServerDbContext<ReservationsDbContext>(webHostEnvironment);
		services.AddScoped<IReservationRepository, ReservationRepository>();
		services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();
		services.AddScoped<IReservationQueryStorage, ReservationQueryStorage>();
		return services;
	}
}