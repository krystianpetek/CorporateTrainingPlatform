﻿using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Authorization;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Infrastructure.MessageBroker;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IMessageBroker _messageBroker;

    public SignUpCommandHandler(
        Serilog.ILogger logger,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordManager passwordManager,
        IMessageBroker messageBroker)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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
        User user = await _userRepository.GetByEmailAsync(email) ?? throw new EmailAlreadyRegisteredException();

        string roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.DefaultRole : command.Role.ToLowerInvariant();
        
        Role role = await _roleRepository.GetAsync(roleName) ?? throw new RoleNotFoundException(roleName);

        string password = _passwordManager.Generate(command.Password);

        user = new User(email, password, role);
        await _userRepository.AddAsync(user);

        _logger.Information($"User with ID: '{user.Id}' has signed up.");
        await _messageBroker.PublishAsync(new UserCreated(user.Id, user.Email), cancellationToken);
    }
}
