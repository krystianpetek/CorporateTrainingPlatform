using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using GarageGenius.Shared.Abstractions.Queries.Query;
using GarageGenius.Shared.Infrastructure.Queries.PagedQuery;
using GarageGenius.Shared.Infrastructure.Queries.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Queries;
public static class Extensions
{
	public static IServiceCollection AddSharedQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
	{
		services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
		IEnumerable<Type> queryTypes = assemblies.SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))));
		foreach (var type in queryTypes)
		{
			services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)), type);
		}

		services.AddSingleton<IPagedQueryDispatcher, PagedQueryDispatcher>();
		IEnumerable<Type> pagedQueryTypes = assemblies.SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(IPagedQueryHandler<,>))));
		foreach (var type in pagedQueryTypes)
		{
			services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPagedQueryHandler<,>)), type);
		}
		return services;
	}
}
