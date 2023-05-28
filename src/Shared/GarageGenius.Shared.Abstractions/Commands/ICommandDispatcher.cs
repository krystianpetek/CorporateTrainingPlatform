namespace GarageGenius.Shared.Abstractions.Commands;
public interface ICommandDispatcher
{
	Task DispatchCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
}
