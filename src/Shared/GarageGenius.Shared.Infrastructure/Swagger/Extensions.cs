using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
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
            // swaggerGenOptions.IncludeXmlComments(); // TODO - add XML comments to the project controllers
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
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Authorization using the Bearer scheme. To access to this API, please enter bearer token",
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
                        Scheme = "bearer",
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
        app.UseSwagger((SwaggerOptions swaggerOptions) =>
        {
            swaggerOptions.RouteTemplate = "garageGenius/{documentName}/swagger.json";

        });
        app.UseSwaggerUI((SwaggerUIOptions swaggerUIOptions) =>
        {
            swaggerUIOptions.RoutePrefix = "garageGenius";
            swaggerUIOptions.SwaggerEndpoint("/garageGenius/v1/swagger.json", "GeniusGarage API");

            swaggerUIOptions.EnableFilter();
            swaggerUIOptions.EnableDeepLinking();
            swaggerUIOptions.EnablePersistAuthorization();
            swaggerUIOptions.EnableValidator();

            swaggerUIOptions.DisplayRequestDuration();
            swaggerUIOptions.DisplayOperationId();

            swaggerUIOptions.ShowCommonExtensions();
            swaggerUIOptions.ShowExtensions();
        });
        return app;
    }
}
