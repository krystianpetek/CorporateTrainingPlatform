using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
using GarageGenius.Shared.Infrastructure.Authentication.PasswordManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jsonWebTokenOptions.Issuer,
                ValidateIssuer = jsonWebTokenOptions.ValidateIssuer,

                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebTokenOptions.IssuerSigningKey)),
                ValidateIssuerSigningKey = jsonWebTokenOptions.ValidateIssuerSigningKey,

                RequireAudience = true,
                ValidAudience = jsonWebTokenOptions.Audience,
                ValidateAudience = jsonWebTokenOptions.ValidateAudience,

                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = jsonWebTokenOptions.ValidateLifetime,
            };
        });
        services.AddPasswordManager();
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>(); // token should be generated once per request
        services.AddSingleton<IJsonWebTokenStorage, JsonWebTokenStorage>();
        services.AddHttpContextAccessor();
        return services;
    }

    public static IApplicationBuilder UseSharedAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        return app;
    }
}
