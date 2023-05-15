using GarageGenius.Modules.Users.Core.Dto;
using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;
internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserDto>
{
    private readonly Serilog.ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(Serilog.ILogger logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    public async Task<GetUserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        User user = await _userRepository.GetAsync(query.id) ?? throw new UserNotFoundException(query.id);

        _logger.Information("User with ID: {UserId}' has been retrieved.", user.Id);
        return user.AsGetUserDto();
    }
}
