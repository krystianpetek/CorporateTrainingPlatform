using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Infrastructure.Authentication.PasswordManager;
using System.Reflection;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;

namespace GarageGenius.Shared.Infrastructure.Authentication;
public static class Extensions
{
    public static IServiceCollection AddSharedAuthentication(this IServiceCollection services, IList<Assembly> assemblies, IConfiguration configuration)
    {
        services.AddOptions<JsonWebTokenOptions>()
            .BindConfiguration(JsonWebTokenOptions.SectionName)
            .Validate(JsonWebTokenOptions.ValidationRules)
            .ValidateOnStart();

        services.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtBearerOptions =>
        {
            
        });
        services.AddPasswordManager();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>(); // token should be generated once per request
        services.AddSingleton<IJsonWebTokenStorage, JsonWebTokenStorage>();
        services.AddHttpContextAccessor();
        return services;
    }
}
