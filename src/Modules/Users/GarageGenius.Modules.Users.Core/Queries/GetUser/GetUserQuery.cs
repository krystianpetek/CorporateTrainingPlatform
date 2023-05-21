using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;

public record GetUserQuery(Guid id) : IQuery<GetUserQueryDto>;
