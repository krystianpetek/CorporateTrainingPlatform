using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Core.Commands.DeactivateUser;
internal class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IUserRepository _userRepository;

    public DeactivateUserCommandHandler(
        Serilog.ILogger logger,
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task HandleCommandAsync(DeactivateUserCommand command, CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetAsync(command.UserId, cancellationToken) ?? throw new UserNotFoundException(command.UserId);
        await _userRepository.DeactivateUserAsync(user.UserId, cancellationToken);

        _logger.Information(
            "Handled {CommandName} in {ModuleName} module, deactivated user with ID: {UserId}", 
            nameof(DeactivateUserCommand), nameof(Users), user.UserId);
    }
}
