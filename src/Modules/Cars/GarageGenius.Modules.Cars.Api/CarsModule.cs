using GarageGenius.Modules.Cars.Application;
using GarageGenius.Modules.Cars.Core;
using GarageGenius.Modules.Cars.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Cars.Api;
internal class CarsModule : IModule
{
    public const string BasePath = "cars-module";
    public string Name => "Cars";
    public IEnumerable<string>? Policies { get; } = new string[] { "cars" };

    public void Register(IServiceCollection services)
    {
        services.AddCarsCore();
        services.AddCarsApplication();
        services.AddCarsInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.MapHealthCheck(Name);
    }
}
