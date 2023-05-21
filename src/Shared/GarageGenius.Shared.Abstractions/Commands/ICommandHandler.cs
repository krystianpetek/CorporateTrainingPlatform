namespace GarageGenius.Shared.Abstractions.Commands;
public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleCommandAsync(TCommand command, CancellationToken cancellationToken = default);
}
