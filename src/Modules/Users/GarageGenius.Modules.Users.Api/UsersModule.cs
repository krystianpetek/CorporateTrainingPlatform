using GarageGenius.Modules.Users.Core;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Modules.Users.Api;
internal class UsersModule : IModule
{
	public const string BasePath = "users-module";
	public string Name { get; } = "Users";
	public IEnumerable<string> Policies { get; } = new string[] { "users" };

	public void Register(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services.AddUsersCoreAsync(webApplicationBuilder.Environment).GetAwaiter().GetResult();
		// TODO - this is a hack, need to redesign the seeder to be more generic and to better approach, idk yet
	}

	public void Use(WebApplication app)
	{
		app.MapHealthCheck(Name);
	}
}