using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Shared.Infrastructure.Dispatchers;

internal sealed class InMemoryDispatcher : IDispatcher
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public InMemoryDispatcher(ICommandDispatcher commandDispatcher, IEventDispatcher eventDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _eventDispatcher = eventDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    public Task DispatchCommandAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
        => _commandDispatcher.DispatchCommandAsync(command, cancellationToken);

    public Task DispatchEventAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
        => _eventDispatcher.DispatchEventAsync(@event, cancellationToken);

    public Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        => _queryDispatcher.DispatchQueryAsync(query, cancellationToken);
}
