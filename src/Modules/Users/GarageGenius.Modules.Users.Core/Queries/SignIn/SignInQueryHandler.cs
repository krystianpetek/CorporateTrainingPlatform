using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Authorization;
using GarageGenius.Shared.Abstractions.Queries;
using Microsoft.Extensions.Logging;

namespace GarageGenius.Modules.Users.Core.Queries.SignIn;
internal class SignInQueryHandler : IQueryHandler<SignInQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SignInQueryHandler> _logger;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtTokenService _jwtTokenService;

    public SignInQueryHandler(IJwtTokenService jwtTokenService, IPasswordManager passwordManager, ILogger<SignInQueryHandler> logger, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService;
        _passwordManager = passwordManager;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<string> HandleAsync(SignInQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Email))
        {
            throw new InvalidEmailException(query.Email);
        }

        if (string.IsNullOrWhiteSpace(query.Password))
        {
            throw new MissingPasswordException();
        }

        var user = await _userRepository.GetByEmailAsync(query.Email.ToLowerInvariant());
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.IsValid(query.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };

        string token = _jwtTokenService.GenerateToken(user.Email.Value);

        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        return token;
    }
}
