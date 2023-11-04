namespace GarageGenius.Modules.Users.Core.Queries.GetUser;
public record GetUserQueryDto(Guid Id, Guid CustomerId, string Role, string Email, string State, DateTime Created);