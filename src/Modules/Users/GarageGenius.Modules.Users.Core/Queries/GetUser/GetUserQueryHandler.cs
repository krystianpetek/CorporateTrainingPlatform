using GarageGenius.Modules.Users.Core.Dto;
using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;
internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly Serilog.ILogger _logger;

    public GetUserQueryHandler(Serilog.ILogger logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    public async Task<GetUserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        User user = await _userRepository.GetAsync(query.id) ?? throw new UserNotFoundException(query.id);
        
        _logger.Information($"User with ID: '{user.Id}' has been retrieved.");
        return user.AsGetUserDto();
    }
}
