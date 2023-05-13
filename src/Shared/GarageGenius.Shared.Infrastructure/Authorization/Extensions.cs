using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Infrastructure.Authentication.PasswordManager;
using System.Reflection;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Shared.Infrastructure.Authorization;
public static class Extensions
{
    public static IServiceCollection AddSharedAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }

    public static IApplicationBuilder UseSharedAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthorization();
        return app;
    }
}
