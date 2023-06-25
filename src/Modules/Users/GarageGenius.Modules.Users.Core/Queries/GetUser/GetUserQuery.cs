using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Users.Core.Queries.GetUser;

public record GetUserQuery(Guid Id) : IQuery<GetUserQueryDto>;
