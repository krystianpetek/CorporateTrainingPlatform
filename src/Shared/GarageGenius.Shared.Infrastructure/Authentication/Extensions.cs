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

        services.AddOptions<JsonWebTokenOptions>()
            .BindConfiguration(JsonWebTokenOptions.SectionName)
            .Validate(JsonWebTokenOptions.ValidationRules)
            .ValidateOnStart();

        services.AddPasswordManager();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>(); // token should be generated once per request
        
        return services;
    }
}
