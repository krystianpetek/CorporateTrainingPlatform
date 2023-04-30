using GarageGenius.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Modules.Users.Core;

namespace GarageGenius.Modules.Users.Api;
internal class UsersModule : IModule
{
    public string Name { get; } = "Users";
    public IEnumerable<string> Policies { get; } = new string[] { "users" };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
