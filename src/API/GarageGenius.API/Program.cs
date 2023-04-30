using Microsoft.OpenApi.Models;
using GarageGenius.Shared.Infrastructure;
using System.Reflection;
using System.Collections.Immutable;

namespace GarageGenius.API;

public static class Program
{
    public async static Task Main(string[] args)
    {
        WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen((swagger) =>
        {
            swagger.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Version = "v1",
                Title = "GeniusGarage"
            });
        });
        builder.Services.AddControllers();

        IReadOnlyCollection<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToImmutableList();
        builder.Services.AddSharedInfrastructure(assemblies);
        
        WebApplication? app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI((swagger) =>
        {
            swagger.SwaggerEndpoint(
                url: "/swagger/v1/swagger.json",
                name: "GeniusGarage");
        });

        app.MapControllers();

        await app.RunAsync();
    }
}
