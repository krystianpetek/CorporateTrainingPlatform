using GarageGenius.Modules.Users.Core.Dto;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class User : AuditableEntity
{
    public Guid Id { get; private set; }
    public Role Role { get; private set; }
    public string RoleId { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Password { get; private set; }
    public UserState? State { get; private set; }

    public User(EmailAddress email, string password, Role role)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        Role = role;
        RoleId = role.Name;
        this.Activate();
    }

    private User() { }

    internal void Deactivate()
    {
        State = UserState.Unactive;
    }

    internal void Activate()
    {
        State = UserState.Active;
    }

    internal void VerifyUserState()
    {
        if (State! != UserState.Active.Value)
            throw new UserInactiveStateException(this.Id); // domain entity should throw exception ?
    }
}

public static class UserExtensions
{
    internal static GetUserDto AsGetUserDto(this User user)
    {
        return new GetUserDto(user.Id, user.RoleId, user.Email.Value, user.State, user.Created);
    }
}
