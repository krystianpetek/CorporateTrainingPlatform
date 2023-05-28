using GarageGenius.Modules.Users.Core;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Users.Api;
internal class UsersModule : IModule
{
	public const string BasePath = "users-module";
	public string Name { get; } = "Users";
	public IEnumerable<string> Policies { get; } = new string[] { "users" };

	public void Register(IServiceCollection services)
	{
		services.AddUsersCore().GetAwaiter().GetResult();
	}

	public void Use(WebApplication app)
	{
		app.MapHealthCheck(Name);
	}
}