using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Commands.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Commands;
public static class Extensions
{
	public static IServiceCollection AddSharedCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
	{
		services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

		IEnumerable<Type> commandHandlersWithoutDecorators = assemblies.SelectMany(
			x => x.GetTypes()
			.Where(t => t.GetInterfaces()
			.Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(ICommandHandler<>) && t.GetCustomAttribute<CommandHandlerDecoratorAttribute>() == null)));

		foreach (var type in commandHandlersWithoutDecorators)
		{
			services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)), type);
		}

		services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
		return services;
	}
}
