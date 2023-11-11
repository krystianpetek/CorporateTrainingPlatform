using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Shared;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Exceptions;
using GarageGenius.Shared.Abstractions.MessageBroker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly IUserRepository _userRepository;
	private readonly IRoleRepository _roleRepository;
	private readonly IPasswordManager _passwordManager;
	private readonly IMessageBroker _messageBroker;
	private readonly IAuthorizationService _authorizationService;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public SignUpCommandHandler(
		Serilog.ILogger logger,
		IUserRepository userRepository,
		IRoleRepository roleRepository,
		IPasswordManager passwordManager,
		IMessageBroker messageBroker,
		IAuthorizationService authorizationService, 
		IHttpContextAccessor httpContextAccessor)
	{
		_logger = logger;
		_userRepository = userRepository;
		_roleRepository = roleRepository;
		_passwordManager = passwordManager;
		_messageBroker = messageBroker;
		_authorizationService = authorizationService;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task HandleCommandAsync(SignUpCommand command, CancellationToken cancellationToken = default)
	{
		AuthorizationResult authorizationResult =
			await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, command.Role,
				UsersPolicyConstants.SignUpPolicy);
		if (!authorizationResult.Succeeded)
			throw new AuthorizationRequirementException(UsersPolicyConstants.SignUpPolicy);

		if (string.IsNullOrWhiteSpace(command.Email))
			throw new InvalidEmailException(command.Email);

		if (string.IsNullOrWhiteSpace(command.Password))
			throw new MissingPasswordException();

		string email = command.Email.ToLower();
		User? user = await _userRepository.GetByEmailAsync(email);
		if (user is not null)
			throw new EmailAlreadyRegisteredException();

		string roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.DefaultRole : command.Role.ToLower();
		Role? role = await _roleRepository.GetAsync(roleName, cancellationToken) ?? throw new RoleNotFoundException(roleName);

		string password = _passwordManager.Generate(command.Password);

		user = new User(email, password, roleName);
		await _userRepository.AddAsync(user, cancellationToken);

		_logger.Information(
	"Handled {CommandName} in {ModuleName} module, signed up user with ID: {UserId}",
	nameof(SignUpCommand), nameof(Users), user.UserId);

		await _messageBroker.PublishAsync(new UserCreatedEvent(user.UserId, user.CustomerId, user.Email), cancellationToken);
	}
}
