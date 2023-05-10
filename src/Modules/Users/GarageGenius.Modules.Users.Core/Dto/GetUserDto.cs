namespace GarageGenius.Modules.Users.Core.Dto;
public record GetUserDto
{
    public Guid Id { get; init; }
    public string Role { get; init; }
    public string Email { get; init; }
    public string State { get; init; }
    public DateTime CreatedAt { get; init; }

    public GetUserDto(Guid id, string role, string email, string state, DateTime createdAt)
    {
        Id = id;
        Role = role;
        Email = email;
        State = state;
        CreatedAt = createdAt;
    }
}