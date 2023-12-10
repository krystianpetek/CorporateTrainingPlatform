using GarageGenius.Modules.Users.Application.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Application.Commands.CreateUser;

internal class CreateUserCommandHandler(
	Serilog.ILogger logger,
	IUserRepository userRepository) : ICommandHandler<CreateUserCommand>
{
	public async Task HandleCommandAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
	{
		var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
		if (user is not null)
			throw new EmailAlreadyRegisteredException();

		user = new User(command.Email, string.Empty, command.Role).Deactivate();

		await userRepository.AddAsync(user, cancellationToken);

		logger.Information(
			"Handled {CommandName} in {ModuleName} module, created user with ID: {UserId}",
			nameof(CreateUserCommand), nameof(Users), user.UserId);
	}
}
