namespace GarageGenius.Modules.Users.Core.Queries.GetUsers;
public record GetUsersQueryDto(IReadOnlyList<GetUsersDto> Users);

public sealed record GetUsersDto(Guid Id, Guid CustomerId, string Role, string Email, string State, DateTime Created);