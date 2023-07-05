using FluentValidation;
using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
using GarageGenius.Modules.Customers.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Application;

internal static class Extensions
{
	public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
	{
		services.AddCustomersCore();

		services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
		services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();

		return services;
	}
}