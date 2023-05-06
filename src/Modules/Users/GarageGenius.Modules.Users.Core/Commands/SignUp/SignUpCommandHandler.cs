using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Events;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Authorization;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Date;
using GarageGenius.Shared.Infrastructure.MessageBroker;
using Microsoft.Extensions.Logging;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    private readonly ILogger<SignUpCommandHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ISystemDate _systemDate;
    private readonly IPasswordManager _passwordManager;
    private readonly IMessageBroker _messageBroker;

    public SignUpCommandHandler(
        ILogger<SignUpCommandHandler> logger,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ISystemDate systemDate,
        IPasswordManager passwordManager,
        IMessageBroker messageBroker)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _systemDate = systemDate;
        _passwordManager = passwordManager;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            throw new InvalidEmailException(command.Email);
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            throw new MissingPasswordException();
        }

        string email = command.Email.ToLowerInvariant();
        User user = await _userRepository.GetByEmailAsync(email);

        if (user is not null)
        {
            throw new EmailAlreadyRegisteredException();
        }

        string roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.DefaultRole : command.Role.ToLowerInvariant();
        Role role = await _roleRepository.GetAsync(roleName);
        if (role is null)
        {
            throw new RoleNotFoundException(roleName);
        }

        DateTime now = _systemDate.GetCurrentDate();
        string password = _passwordManager.Generate(command.Password);

        user = new User
        {
            Id = command.UserId,
            Email = email,
            Password = password,
            Role = role,
            CreatedDate = now,
            State = UserState.Active,
        };
        await _userRepository.AddAsync(user);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
        await _messageBroker.PublishAsync(new UserCreated(user.Id, user.Email));
    }
}
