using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Abstractions.Modules;
public interface IModule
{
    string Name { get; }
    abstract IEnumerable<string>? Policies { get; }
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}
