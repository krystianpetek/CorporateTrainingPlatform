using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class User : AuditableEntity
{
    public Guid Id { get; private set; }
    public string RoleId { get; init; }
    public Role Role { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Password { get; private set; }
    public UserState State { get; private set; }

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
}


