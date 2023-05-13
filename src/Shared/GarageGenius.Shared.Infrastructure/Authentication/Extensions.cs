using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Infrastructure.Authentication.PasswordManager;
using System.Reflection;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
using Microsoft.Extensions.Configuration;

namespace GarageGenius.Shared.Infrastructure.Authentication;
public static class Extensions
{
    public static IServiceCollection AddSharedAuthentication(this IServiceCollection services, IList<Assembly> assemblies, IConfiguration configuration)
    {
        services.AddAuthentication(authenticationOptions =>
        {
            //authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        services.AddPasswordManager();
        services.Configure<JsonWebTokenOptions>(configuration.GetRequiredSection(JsonWebTokenOptions.SectionName));
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>(); // token should be generated once per request
        return services;
    }
}
