using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;

public record GetUserQuery(Guid id) : IQuery<GetUserQueryDto>;
