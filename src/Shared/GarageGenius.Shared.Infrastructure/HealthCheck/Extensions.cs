using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Infrastructure.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.HealthCheck;
public static class Extensions
{
    public static IServiceCollection AddSharedHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }

    public static IApplicationBuilder MapHealthCheck(this IApplicationBuilder app, string moduleName)
    {
        app.UseHealthChecks($"/health/{moduleName.ToLower()}-module", new HealthCheckOptions
        {
            ResponseWriter = async (HttpContext httpContext, HealthReport healthReport) =>
            {
                await httpContext.Response.WriteAsJsonAsync(new { message = $"{moduleName}, {healthReport.Status}" });
            }
        });
        return app;
    }
}
