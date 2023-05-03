using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Collections.Immutable;
using System.Reflection;

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
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Version = "v1",
                Title = "GeniusGarage"
            });
        });
        builder.Services.AddControllers();

        IList<Assembly> assemblies = LoadAssemblies(builder.Configuration, "GarageGenius.Modules.");
        IEnumerable<IModule> modules = assemblies.LoadModules();

        builder.Services.AddSharedInfrastructure(assemblies);
        foreach (IModule module in modules)
        {
            module.Register(builder.Services);
        }


        WebApplication? app = builder.Build();
        app.UseSharedInfrastructure();

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

    public static IList<IModule> LoadModules(this IEnumerable<Assembly> assemblies)
        => assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();

    public static IList<Assembly> LoadAssemblies(IConfiguration configuration, string modulePart)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }
}