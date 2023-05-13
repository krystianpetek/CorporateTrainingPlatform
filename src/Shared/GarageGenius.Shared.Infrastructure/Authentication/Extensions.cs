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
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            JsonWebTokenOptions jsonWebTokenOptions = serviceProvider.GetRequiredService<IOptions<JsonWebTokenOptions>>().Value;

            jwtBearerOptions.RequireHttpsMetadata = true;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.Audience = jsonWebTokenOptions.Audience;
            jwtBearerOptions.ClaimsIssuer = jsonWebTokenOptions.Issuer;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ClockSkew = TimeSpan.Zero,

                ValidIssuer = jsonWebTokenOptions.Issuer,
                ValidAudience = jsonWebTokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebTokenOptions.IssuerSigningKey)),
                ValidateAudience = jsonWebTokenOptions.ValidateAudience,
                ValidateIssuer = jsonWebTokenOptions.ValidateIssuer,
                ValidateIssuerSigningKey = jsonWebTokenOptions.ValidateIssuerSigningKey,
                ValidateLifetime = jsonWebTokenOptions.ValidateLifetime,
            };
        });
        services.AddPasswordManager();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>(); // token should be generated once per request
        services.AddSingleton<IJsonWebTokenStorage, JsonWebTokenStorage>();
        services.AddHttpContextAccessor();
        return services;
    }
}
