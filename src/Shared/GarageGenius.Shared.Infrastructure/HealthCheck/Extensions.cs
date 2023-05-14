using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GarageGenius.Shared.Infrastructure.HealthCheck;
public static class Extensions
{
    public static IApplicationBuilder UseSharedHealthCheck(this IApplicationBuilder app, string moduleName)
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
