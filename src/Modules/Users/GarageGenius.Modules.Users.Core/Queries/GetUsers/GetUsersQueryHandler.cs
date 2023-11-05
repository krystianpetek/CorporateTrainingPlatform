using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Exceptions;
using GarageGenius.Shared.Abstractions.Queries.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace GarageGenius.Modules.Users.Core.Queries.GetUsers;

internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryDto>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUsersQueryHandler(
        ILogger logger,
        IUserRepository userRepository,
        IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userRepository = userRepository;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetUsersQueryDto> HandleQueryAsync(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, "GetUsersPolicy");
        if (!authorizationResult.Succeeded)
            throw new AuthorizationRequirementException("GetUsersPolicy");

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