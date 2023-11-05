using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries.Query;
using Serilog;

namespace GarageGenius.Modules.Users.Core.Queries.GetUsers;

internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryDto>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        ILogger logger,
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<GetUsersQueryDto> HandleQueryAsync(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetUsersAsync(cancellationToken);
        
        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, fetch all users",
            nameof(GetUsersQueryHandler),
            nameof(Users),
            query);
        
        // todo - audit who is fetching all users
        // TODO - cache?
        return users;
    }
}