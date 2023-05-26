using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Core.Commands.SignIn;
internal class SignInCommandHandler : ICommandHandler<SignInCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IJsonWebTokenService _jwtTokenService;
    private readonly IJsonWebTokenStorage _jwtTokenStorage;

    public SignInCommandHandler(
        Serilog.ILogger logger,
        IUserRepository userRepository,
        IJsonWebTokenService jwtTokenService,
        IJsonWebTokenStorage jwtTokenStorage,
        IPasswordManager passwordManager)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _jwtTokenService = jwtTokenService;
        _jwtTokenStorage = jwtTokenStorage;
    }

    public async Task HandleCommandAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            throw new InvalidEmailException(command.Email);

        if (string.IsNullOrWhiteSpace(command.Password))
            throw new MissingPasswordException();

        User user = await _userRepository.GetByEmailAsync(command.Email.ToLower(), cancellationToken) ?? throw new InvalidCredentialsException();

        if (!_passwordManager.IsValid(command.Password, user.Password))
            throw new InvalidCredentialsException();

        user.VerifyUserState();

        Dictionary<string, object> claims = new Dictionary<string, object> { ["permissions"] = user.Role.Permissions };
        JsonWebTokenResponse token = _jwtTokenService.GenerateToken(user.UserId, user.Email, user.RoleName, claims);

        _jwtTokenStorage.SetToken(token);
        _logger.Information(
    "Handled {CommandName} in {ModuleName} module, signed in user with ID: {UserId}",
    nameof(SignInCommand), nameof(Users), user.UserId);
        // TODO refresh token ?
    }
}
