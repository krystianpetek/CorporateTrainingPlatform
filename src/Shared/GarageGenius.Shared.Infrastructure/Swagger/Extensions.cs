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
            swaggerGenOptions.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
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
                        Scheme = "Bearer",
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
            swaggerUIOptions.SwaggerEndpoint(
                url: "/swagger/v1/swagger.json",
               name: "GeniusGarage");
        });
        return app;
    }
}
