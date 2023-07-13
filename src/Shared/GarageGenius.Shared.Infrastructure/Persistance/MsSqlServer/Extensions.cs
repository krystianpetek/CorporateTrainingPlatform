using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
public static class Extensions
{
	public static IServiceCollection AddMsSqlServerDbContext<T>(this IServiceCollection services, IWebHostEnvironment webHostEnvironment) where T : DbContext
	{
		services.AddOptions<SqlServerSettings>()
		.BindConfiguration(SqlServerSettings.SectionName)
		.Validate(SqlServerSettings.ValidationRules)
		.ValidateOnStart();

		using ServiceProvider? serviceProvider = services.BuildServiceProvider();

		SqlServerSettings connection = serviceProvider.GetRequiredService<IOptions<SqlServerSettings>>()!.Value;
		IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

		if (connection.InMemoryDatabase)
		{
			services.AddDbContext<T>(x => x.UseInMemoryDatabase(connection.SqlServerConnection));
		}
		else
		{
			services.AddDbContext<T>(dbContextOptionsBuilder =>
			{
				dbContextOptionsBuilder.UseSqlServer(connection.SqlServerConnection);
				dbContextOptionsBuilder.EnableDetailedErrors();
				if (webHostEnvironment.IsDevelopment())
				{
					dbContextOptionsBuilder.EnableSensitiveDataLogging();
				}
			});
		}
		services.AddScoped<AuditableEntitySaveChangesInterceptor>();
		return services;
	}
}
