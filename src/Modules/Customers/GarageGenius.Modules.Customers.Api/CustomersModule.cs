using GarageGenius.Modules.Customers.Application;
using GarageGenius.Modules.Customers.Core;
using GarageGenius.Modules.Customers.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Modules.Customers.Api;
internal class CustomersModule : IModule
{
	public const string BasePath = "customers-module";
	public string Name => "Customers";
	public IEnumerable<string>? Policies { get; } = new string[] { "customers" };

	public void Register(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services
			.AddCustomersCore()
			.AddCustomersApplication()
			.AddCustomersInfrastructure(webApplicationBuilder.Environment);
	}

	public void Use(WebApplication app)
	{
		app.MapHealthCheck(Name);
	}
}
