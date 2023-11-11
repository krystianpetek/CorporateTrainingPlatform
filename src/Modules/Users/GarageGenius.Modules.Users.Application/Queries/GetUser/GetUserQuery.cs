using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Users.Application.Queries.GetUser;

public record GetUserQuery(Guid Id) : IQuery<GetUserQueryDto>;
