using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Users.Core.Queries.GetUsers;

public record GetUsersQuery() : IQuery<GetUsersQueryDto>;