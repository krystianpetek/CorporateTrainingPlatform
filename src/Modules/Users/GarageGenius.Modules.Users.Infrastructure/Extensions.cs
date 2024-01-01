using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Users.Infrastructure.Persistance.Repositories;
using GarageGenius.Shared.Abstractions.Persistance;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Users.Infrastructure;

internal static class Extensions
{
	public static async Task<IServiceCollection> AddUsersInfrastructureAsync(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddScoped<IRoleRepository, RoleRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		//services.AddMsSqlServerDbContext<UsersDbContext>(webHostEnvironment);
		services.AddPostgreSqlServerDbContext<UsersDbContext>(webHostEnvironment);
		services.AddTransient<IDbContextSeeder, UsersDbContextSeeder>();

		using IServiceScope? serviceScope = services.BuildServiceProvider().CreateScope();
		var dbSeeder = serviceScope.ServiceProvider.GetService<IDbContextSeeder>();
		await dbSeeder.SeedDatabaseAsync(); // TODO redesign seeder to be more generic and to better approach, idk yet
		return services;
	}
}