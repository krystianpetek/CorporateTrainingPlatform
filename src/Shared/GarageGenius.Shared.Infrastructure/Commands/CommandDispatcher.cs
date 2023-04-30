using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Shared.Infrastructure.Commands;
internal class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        using IServiceScope? scope = _serviceProvider.CreateScope();
        ICommandHandler<TCommand> handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command, cancellationToken);
    }
}
