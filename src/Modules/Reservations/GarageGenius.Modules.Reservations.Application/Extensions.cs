using FluentValidation;
using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Modules.Reservations.Application.Commands.CompleteReservation;
using GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Reservations.Application;

internal static class Extensions
{
	public static IServiceCollection AddReservationsApplication(this IServiceCollection services)
	{
		services.AddScoped<IValidator<AddReservationCommand>, AddReservationCommandValidator>();
		services.AddScoped<IValidator<CompleteReservationCommand>, CompleteReservationCommandValidator>();
		services.AddScoped<IValidator<UpdateReservationCommand>, UpdateReservationCommandValidator>();
		return services;
	}
}