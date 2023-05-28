using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Shared.Infrastructure.Dispatchers;

internal sealed class InMemoryDispatcher : IDispatcher
{
	private readonly ICommandDispatcher _commandDispatcher;
	private readonly IEventDispatcher _eventDispatcher;
	private readonly IQueryDispatcher _queryDispatcher;
	private readonly IPagedQueryDispatcher _pagedQueryDispatcher;

	public InMemoryDispatcher(ICommandDispatcher commandDispatcher,
		IEventDispatcher eventDispatcher,
		IQueryDispatcher queryDispatcher,
		IPagedQueryDispatcher pagedQueryDispatcher)
	{
		_commandDispatcher = commandDispatcher;
		_eventDispatcher = eventDispatcher;
		_queryDispatcher = queryDispatcher;
		_pagedQueryDispatcher = pagedQueryDispatcher;
	}

	public Task DispatchCommandAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
	{
		return _commandDispatcher.DispatchCommandAsync(command, cancellationToken);
	}

	public Task DispatchEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
	{
		return _eventDispatcher.DispatchEventAsync(@event, cancellationToken);
	}

	public Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
	{
		return _queryDispatcher.DispatchQueryAsync(query, cancellationToken);
	}

	public Task<TPagedQueryResult> DispatchPagedQueryAsync<TPagedQueryResult>(IPagedQuery<TPagedQueryResult> query, CancellationToken cancellationToken = default)
	{
		return _pagedQueryDispatcher.DispatchPagedQueryAsync(query, cancellationToken);
	}
}
