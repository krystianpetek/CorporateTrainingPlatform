using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GarageGenius.Modules.Customers.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddCustomersInfrastructure(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddMsSqlServerDbContext<CustomersDbContext>(webHostEnvironment);
		services.AddScoped<ICustomerRepository, CustomerRepository>();

		return services;
	}
}