namespace GarageGenius.Modules.Users.Application.Queries.GetUser;
public record GetUserQueryDto(Guid Id, Guid CustomerId, string Role, string Email, string State, DateTime Created);