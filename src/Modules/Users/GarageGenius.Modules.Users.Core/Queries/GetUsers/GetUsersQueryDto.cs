using GarageGenius.Modules.Users.Core.Queries.GetUser;

namespace GarageGenius.Modules.Users.Core.Queries.GetUsers;
public record GetUsersQueryDto(Guid Id, Guid CustomerId, string Role, string Email, string State, DateTime Created);