using FluentValidation;
using GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
using GarageGenius.Modules.Vehicles.Application.Commands.UpdateVehicleOwner;
using GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Application;

internal static class Extensions
{
	public static IServiceCollection AddVehiclesApplication(this IServiceCollection services)
	{
		services.AddScoped<IAuthorizationHandler, AddVehiclePolicyHandler>();
		services.AddScoped<IAuthorizationHandler, GetCustomerVehiclesPolicyHandler>();

		services.AddScoped<IValidator<AddVehicleCommand>, AddVehicleCommandValidator>();
		services.AddScoped<IValidator<UpdateVehicleOwnerCommand>, UpdateVehicleOwnerCommandValidator>();
		
		services.AddAuthorization(authorizationOptions =>
		{
			authorizationOptions.AddVehiclePolicy();
			authorizationOptions.GetCustomerVehiclesPolicy();
		});
		return services;
	}
}
