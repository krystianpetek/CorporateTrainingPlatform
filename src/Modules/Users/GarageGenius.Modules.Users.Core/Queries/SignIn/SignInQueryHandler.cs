using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Users.Core.Queries.SignIn;
internal class SignInQueryHandler : IQueryHandler<SignInQuery, JsonWebTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly Serilog.ILogger _logger;
    private readonly IPasswordManager _passwordManager;
    private readonly IJsonWebTokenService _jwtTokenService;

    public SignInQueryHandler(IJsonWebTokenService jwtTokenService, IPasswordManager passwordManager, Serilog.ILogger logger, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService;
        _passwordManager = passwordManager;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<JsonWebTokenDto> HandleAsync(SignInQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Email))
            throw new InvalidEmailException(query.Email);

        if (string.IsNullOrWhiteSpace(query.Password))
            throw new MissingPasswordException();

        User user = await _userRepository.GetByEmailAsync(query.Email.ToLower()) ?? throw new InvalidCredentialsException();

        if (!_passwordManager.IsValid(query.Password, user.Password))
            throw new InvalidCredentialsException();

        Dictionary<string, object> claims = new Dictionary<string, object> { ["permissions"] = user.Role.Permissions };
        JsonWebTokenDto token = _jwtTokenService.GenerateToken(user.Id, user.Email,user.RoleId, claims);

        _logger.Information($"User with ID: '{user.Id}' has signed in.");
        
        return token;
        // TODO refresh token
    }
}
