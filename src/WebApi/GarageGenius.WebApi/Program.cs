using GarageGenius.Shared.Abstractions.Exceptions;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure;
using GarageGenius.Shared.Infrastructure.Modules;
using GarageGenius.WebApi.Middlewares.ErrorHandling;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;
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

			builder.Services.AddGlobalErrorHandling(); // TODO - rewrite GlobalErrorHandling to ExceptionHandler with ProblemDetails
			builder.Services.AddProblemDetails();
			builder.Services.AddEndpointsApiExplorer();

			IReadOnlyCollection<Assembly> assemblies = builder.LoadSharedAssemblies("GarageGenius.Modules.");
			IReadOnlyCollection<IModule> modules = assemblies.LoadModules(builder.Configuration);

			builder.Services.AddSharedInfrastructure(builder.Configuration, assemblies.ToList(), modules.ToList());
			foreach (IModule module in modules)
			{
				module.Register(builder);
				Log.Information($"Loaded module: {module.Name}");
			}
			builder.Services.AddControllers();
			WebApplication? app = builder.Build();
			app.UseSerilogRequestLogging(requestLoggingOptions =>
			{
				requestLoggingOptions.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
				{
					diagnosticContext.Set("UserId", httpContext?.User?.Identity?.Name ?? "Anonymous");
				};
				requestLoggingOptions.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000} ms";
			});

			app.UseSharedInfrastructure();
			foreach (IModule module in modules)
			{
				module.Use(app);
				Log.Information($"Mapped registered services for module: {module.Name}");
			}

			app.UseGlobalErrorHandling();  // TODO - rewrite GlobalErrorHandling to ExceptionHandler with ProblemDetails
			app.UseExceptionHandler(exceptionHandlerApp =>
			{
				exceptionHandlerApp.Run(async context =>
				{
					context.Response.ContentType = "application/problem+json";
					if (context.RequestServices.GetService<IProblemDetailsService>() is { } problemDetailsService)
					{
						var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
						var exceptionType = exceptionHandlerFeature?.Error;
						if (exceptionType is not null)
						{
							(string Title, string Detail, int StatusCode) = exceptionType switch
							{
								GarageGeniusException garageGeniusException =>
								(
									exceptionType.GetType().Name,
									exceptionType.Message,
									context.Response.StatusCode = (int)garageGeniusException.StatusCode
								),
								GarageGeniusValidationException garageGeniusValidationException =>
								(
									exceptionType.GetType().Name,
									garageGeniusValidationException.Errors.FirstOrDefault().ErrorMessage,
									context.Response.StatusCode = StatusCodes.Status400BadRequest
								),
								_ =>
								(
									exceptionType.GetType().Name,
									exceptionType.Message,
									context.Response.StatusCode = StatusCodes.Status500InternalServerError
								)
							};
							var problem = new ProblemDetailsContext
							{
								HttpContext = context,
								ProblemDetails =
								{
									Title = Title,
									Detail = Detail,
									Status = StatusCode,
									Instance = context.Request.Path,
								},
							};
							if (builder.Environment.IsDevelopment())
							{
								problem.ProblemDetails.Extensions.Add("exception", exceptionHandlerFeature?.Error.ToString());
							}

							await problemDetailsService.WriteAsync(problem);
						}
					}
				});
			});
			app.UseStatusCodePages();

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
			await Log.CloseAndFlushAsync();
		}
		return 0;
	}
}