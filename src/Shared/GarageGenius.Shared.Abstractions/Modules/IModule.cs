using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Shared.Abstractions.Modules;
public interface IModule
{
	string Name { get; }
	abstract IEnumerable<string> Policies { get; }
	void Register(WebApplicationBuilder webApplicationBuilder);
	void Use(WebApplication app);
}
