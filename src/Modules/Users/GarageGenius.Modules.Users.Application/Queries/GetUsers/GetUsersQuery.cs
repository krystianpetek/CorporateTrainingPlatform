using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Users.Application.Queries.GetUsers;

public record GetUsersQuery() : IQuery<GetUsersQueryDto>;