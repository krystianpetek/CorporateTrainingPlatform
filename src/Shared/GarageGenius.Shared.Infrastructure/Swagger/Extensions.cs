using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GarageGenius.Shared.Infrastructure.Swagger;
public static class Extensions
{
    public static IServiceCollection AddSharedSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen((SwaggerGenOptions swaggerGenOptions) =>
        {
            swaggerGenOptions.EnableAnnotations();
            swaggerGenOptions.CustomSchemaIds((Type x) => x.FullName);
            swaggerGenOptions.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Title = "GeniusGarage WebAPI",
                Version = "v1",
                Description = "The GarageGenius REST API.",
                Contact = new OpenApiContact
                {
                    Name = "GeniusGarage Support",
                    Email = "krystianpetek2@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/krystian-petek-3731b9215/")
                }
            });
            // TODO OAuth2.0 with OpenIdConnect and OpenIddict
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Authorization using the Bearer scheme. To access to this API, pass 'Bearer {token}'",
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }

    public static IApplicationBuilder UseSharedSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI((SwaggerUIOptions swaggerUIOptions) =>
        {
            swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json","GeniusGarage");

            swaggerUIOptions.RoutePrefix = "swagger";
            swaggerUIOptions.DisplayRequestDuration();
            swaggerUIOptions.EnableDeepLinking();
            swaggerUIOptions.ShowCommonExtensions();
            swaggerUIOptions.ShowExtensions();

            swaggerUIOptions.EnableFilter();
            swaggerUIOptions.EnableValidator();
            swaggerUIOptions.EnablePersistAuthorization();
        });
        return app;
    }
}
