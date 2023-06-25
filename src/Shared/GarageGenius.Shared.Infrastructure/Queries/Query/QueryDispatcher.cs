using GarageGenius.Shared.Abstractions.Queries.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Queries.Query;
internal class QueryDispatcher : IQueryDispatcher
{
	private readonly IServiceProvider _serviceProvider;

	public QueryDispatcher(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task<TQueryResult> DispatchQueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default)
	{
		using IServiceScope? scope = _serviceProvider.CreateScope();
		Type? handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TQueryResult));
		object handler = scope.ServiceProvider.GetRequiredService(handlerType);
		MethodInfo? method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TQueryResult>, TQueryResult>.HandleQueryAsync));

		if (method is null)
		{
			throw new InvalidOperationException($"Query handler for '{typeof(TQueryResult).Name}' is invalid.");
		}

		return await (Task<TQueryResult>)method.Invoke(handler, new object[] { query, cancellationToken });
	}
}
