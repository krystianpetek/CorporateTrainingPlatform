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

    public async Task HandleAsync(DeactivateUserCommand command, CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetAsync(command.UserId) ?? throw new UserNotFoundException(command.UserId);

        await _userRepository.DeactivateUserAsync(user.Id);
        _logger.Information("User with ID: {UserId} has been deactivated.", command.UserId);
    }
}
