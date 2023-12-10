using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Application.Commands.CreateUser;

public record CreateUserCommand(string Email, string Role) : ICommand
{
    public required string Role { get; init; } = Role;
    public required string Email { get; init; } = Email;
}