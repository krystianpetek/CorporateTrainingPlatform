using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
public static class Extensions
{
	public static IServiceCollection AddMsSqlServerDbContext<T>(this IServiceCollection services, IWebHostEnvironment webHostEnvironment) where T : DbContext
	{
		using ServiceProvider? serviceProvider = services.BuildServiceProvider();
		IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
		services.AddScoped<AuditableEntitySaveChangesInterceptor>();

		string connection = configuration.GetConnectionString("SqlServerConnection");
		bool inMemoryDatabase = configuration.GetValue<bool>("InMemoryDatabase");

		if (inMemoryDatabase)
		{
			services.AddDbContext<T>(x => x.UseInMemoryDatabase(connection));
		}
		else
		{
			services.AddDbContext<T>(dbContextOptionsBuilder =>
			{
				dbContextOptionsBuilder.UseSqlServer(connection);
				dbContextOptionsBuilder.EnableDetailedErrors();
				if (webHostEnvironment.IsDevelopment())
				{
					dbContextOptionsBuilder.EnableSensitiveDataLogging();
				}
			});
		}
		return services;
	}
}
