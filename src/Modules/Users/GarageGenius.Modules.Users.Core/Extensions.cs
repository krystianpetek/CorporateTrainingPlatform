using FluentValidation;
using GarageGenius.Modules.Users.Core.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Core.Commands.SignIn;
using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Persistance.Repositories;
using GarageGenius.Modules.Users.Core.Queries.GetUsers.Policy;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Core.ServiceMapper;
using GarageGenius.Shared.Abstractions.Persistance;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Users.Core;

internal static class Extensions
{
	public static async Task<IServiceCollection> AddUsersCore(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddScoped<IRoleRepository, RoleRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddMsSqlServerDbContext<UsersDbContext>(webHostEnvironment);
		services.AddTransient<IDbContextSeeder, UsersDbContextSeeder>();
		services.AddScoped<IUserServiceMapper, UserServiceMapper>();

		services.AddScoped<IAuthorizationHandler, GetUsersPolicyHandler>();
		
		services.AddAuthorization(authorizationOptions =>
		{
			authorizationOptions.GetUsersPolicy();
		});

		services.AddScoped<IValidator<DeactivateUserCommand>, DeactivateUserCommandValidator>();
		services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();
		services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();

		using IServiceScope? serviceScope = services.BuildServiceProvider().CreateScope();
		var dbSeeder = serviceScope.ServiceProvider.GetService<IDbContextSeeder>();
		await dbSeeder.SeedDatabaseAsync(); // TODO redesign seeder to be more generic and to better approach, idk yet
		return services;
	}
}