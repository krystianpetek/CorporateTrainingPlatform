using FluentValidation;
using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Shared.Abstractions.Commands.Decorators;
[CommandHandlerDecorator]
public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
	private readonly ICommandHandler<TCommand> _decorated;
	private readonly IValidator<TCommand> _validator;

	public ValidationCommandHandlerDecorator(
		ICommandHandler<TCommand> decorated,
		IValidator<TCommand> validator)
	{
		_decorated = decorated;
		_validator = validator;
	}

	public async Task HandleCommandAsync(TCommand command, CancellationToken cancellationToken = default)
	{
		var validationResult = await _validator.ValidateAsync(command, cancellationToken);
		if (validationResult.Errors.Any())
			throw new GarageGeniusValidationException(validationResult.Errors);

		await _decorated.HandleCommandAsync(command, cancellationToken);
	}
}