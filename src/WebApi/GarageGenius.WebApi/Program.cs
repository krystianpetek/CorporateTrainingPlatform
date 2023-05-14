using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure;
using GarageGenius.Shared.Infrastructure.Modules;
using GarageGenius.WebApi.Middlewares.ErrorHandling;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Collections.Immutable;
using System.Reflection;

namespace GarageGenius.WebApi;

public static class Program
{
    public async static Task<int> Main(string[] args)
    {
        // ensure logging before configuration is loaded
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web host");
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext(),
                preserveStaticLogger: true);

            builder.Services.AddGlobalErrorHandling();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            IReadOnlyCollection<Assembly> assemblies = builder.LoadSharedAssemblies("GarageGenius.Modules.");
            IEnumerable<IModule> modules = assemblies.LoadModules(builder.Configuration);

            builder.Services.AddSharedInfrastructure( builder.Configuration, assemblies.ToList());
            foreach (IModule module in modules)
            {
                module.Register(builder.Services);
                Log.Information($"Loaded module: {module.Name}");
            }

            WebApplication? app = builder.Build();
            app.UseSerilogRequestLogging(requestLoggingOptions =>
            {
                requestLoggingOptions.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000} ms";
            });

            app.UseSharedInfrastructure();
            foreach (IModule module in modules)
            {
                module.Use(app);
                Log.Information($"Mapped registered services for module: {module.Name}");
            }

            app.UseGlobalErrorHandling();
            app.UseHttpsRedirection();
            app.MapControllers();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
        return 0;
    }
}