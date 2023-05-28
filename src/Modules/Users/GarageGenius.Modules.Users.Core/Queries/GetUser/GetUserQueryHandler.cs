using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Modules.Users.Core.ServiceMapper;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;
internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IUserRepository _userRepository;
	private readonly IUserServiceMapper _userServiceMapper;

	public GetUserQueryHandler(
		Serilog.ILogger logger,
		IUserRepository userRepository,
		IUserServiceMapper userServiceMapper)
	{
		_logger = logger;
		_userRepository = userRepository;
		_userServiceMapper = userServiceMapper;
	}
	public async Task<GetUserQueryDto> HandleQueryAsync(GetUserQuery query, CancellationToken cancellationToken = default)
	{
		User user = await _userRepository.GetAsync(query.id, cancellationToken) ?? throw new UserNotFoundException(query.id);
		GetUserQueryDto mappedUser = _userServiceMapper.MapToGetUserQueryDto(user);

		_logger.Information(
			"Handled {QueryName} in {ModuleName} module, retrieved user with ID: {UserId}",
			nameof(GetUserQuery), nameof(Users), user.UserId);
		return mappedUser;
	}
}
