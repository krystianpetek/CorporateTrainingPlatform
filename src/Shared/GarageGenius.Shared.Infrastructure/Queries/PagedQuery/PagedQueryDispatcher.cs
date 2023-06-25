using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Queries.PagedQuery;
internal class PagedQueryDispatcher : IPagedQueryDispatcher
{
	private readonly IServiceProvider _serviceProvider;

	public PagedQueryDispatcher(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task<TPagedQueryResult> DispatchPagedQueryAsync<TPagedQueryResult>(IPagedQuery<TPagedQueryResult> query, CancellationToken cancellationToken = default)
	{
		using IServiceScope? scope = _serviceProvider.CreateScope();
		Type? handlerType = typeof(IPagedQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TPagedQueryResult));
		object handler = scope.ServiceProvider.GetRequiredService(handlerType);
		MethodInfo? method = handlerType.GetMethod(nameof(IPagedQueryHandler<IPagedQuery<TPagedQueryResult>, TPagedQueryResult>.HandlePagedQueryAsync));

		if (method is null)
			throw new InvalidOperationException($"Paged query handler for '{typeof(TPagedQueryResult).Name}' is invalid.");

		return await (Task<TPagedQueryResult>)method.Invoke(handler, new object[] { query, cancellationToken });
	}
}
