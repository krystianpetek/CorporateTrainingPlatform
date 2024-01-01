using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
public static class Extensions
{
	public static IServiceCollection AddPostgreSqlServerDbContext<T>(this IServiceCollection services, IWebHostEnvironment webHostEnvironment) where T : DbContext
	{
		services.AddOptions<PostgreSqlSettings>()
		.BindConfiguration(PostgreSqlSettings.SectionName)
		.Validate(PostgreSqlSettings.ValidationRules)
		.ValidateOnStart();

		using ServiceProvider? serviceProvider = services.BuildServiceProvider();

		PostgreSqlSettings connection = serviceProvider.GetRequiredService<IOptions<PostgreSqlSettings>>()!.Value;
		IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

		if (connection.InMemoryDatabase)
		{
			services.AddDbContext<T>(x => x.UseInMemoryDatabase(connection.PostgreSqlConnection));
		}
		else
		{
			services.AddDbContext<T>(dbContextOptionsBuilder =>
			{
				dbContextOptionsBuilder.UseNpgsql(connection.PostgreSqlConnection);
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
