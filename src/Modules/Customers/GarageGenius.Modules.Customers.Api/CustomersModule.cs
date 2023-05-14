using GarageGenius.Modules.Customers.Application;
using GarageGenius.Modules.Customers.Core;
using GarageGenius.Modules.Customers.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Api;
internal class CustomersModule : IModule
{
    public const string BasePath = "customers-module";
    public string Name => "Customers";
    public IEnumerable<string>? Policies { get; } = new string[] { "customers" };

    public void Register(IServiceCollection services)
    {
        services.AddCustomersCore();
        services.AddCustomersApplication();
        services.AddCustomersInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseSharedHealthCheck(Name);
    }
}
