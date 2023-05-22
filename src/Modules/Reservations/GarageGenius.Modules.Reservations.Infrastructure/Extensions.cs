using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddReservationsInfrastructure(this IServiceCollection services)
    {
        services.AddMsSqlServerDbContext<ReservationsDbContext>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();
        return services;
    }
}