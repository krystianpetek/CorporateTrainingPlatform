namespace GarageGenius.Modules.Users.Application.Queries.GetUsers;
public record GetUsersQueryDto(IEnumerable<GetUsersDto> Users);

public sealed record GetUsersDto(Guid Id, Guid CustomerId, string Role, string Email, string State, DateTime Created);