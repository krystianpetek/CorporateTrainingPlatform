using GarageGenius.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Modules;
public static class Extensions
{
	public static IReadOnlyCollection<Assembly> LoadSharedAssemblies(this WebApplicationBuilder webApplicationBuilder, string modulePart)
	{
		// load all assemblies
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
		var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();

		// filter only GarageGenius monolith modules
		var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
			.Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
			.Where(x => x.Contains(modulePart))
			.ToList();

		foreach (var settings in Directory.EnumerateFiles(webApplicationBuilder.Environment.ContentRootPath, "*.settings.json", SearchOption.AllDirectories))
		{
			webApplicationBuilder.Configuration.AddJsonFile(settings);

		}

		//var abc = webApplicationBuilder.Configuration
		//    .AsEnumerable()
		//    .Where(x => x.Key.StartsWith("Module:"))
		//    .Where(x => x.Key.EndsWith(":Enabled")).Select(x => new
		//    {
		//        name = x.Key.Split(":")[1],
		//        enabled = x.Value
		//    });
		var modules = LoadModulesSettings(webApplicationBuilder.Configuration);
		foreach (var module in modules)
		{
			if (!module.Enabled)
			{
				files.RemoveAll(x => x.Contains(module.Name));
			}
		}

		files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

		return assemblies;
	}

	public static IReadOnlyCollection<IModule> LoadModules(this IReadOnlyCollection<Assembly> assemblies, IConfiguration configuration)
	{
		return assemblies
			.SelectMany(x => x.GetTypes())
			.Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
			.OrderBy(x => x.Name)
			.Select(Activator.CreateInstance)
			.Cast<IModule>()
			.ToList();

		//var remains = LoadModulesSettings(configuration);
		//foreach (var module in remains)
		//{
		//    if (!module.Enabled)
		//    {
		//        modules.RemoveAll(x => x.Name.Contains(module.Name));
		//    }
		//}
		//return modules;
	}

	public static IReadOnlyCollection<ModuleOptions> LoadModulesSettings(this IConfiguration configuration)
	{
		return configuration
			.AsEnumerable()
			.Where(x => x.Key.StartsWith("Module:"))
			.Where(x => x.Key.EndsWith(":Enabled"))
			.Select(x => new ModuleOptions(x.Key.Split(":")[1], bool.Parse(x.Value)))
			.ToImmutableList();

		//foreach(var module in modules)
		//{
		//    var moduleSettings = configuration
		//        .AsEnumerable()
		//        .Where(x => x.Key.StartsWith($"Module:{module.name}:"))
		//        .ToDictionary(x => x.Key.Replace($"Module:{module.name}:", ""), x => x.Value);
		//    if (moduleSettings.Any())
		//    {
		//        configuration.GetSection($"Modules:{module.name}").Bind(moduleSettings);
		//    }
		//}   
	}

}
