using GarageGenius.Modules.Users.Core.ValueObjects;

namespace GarageGenius.Modules.Users.Core.Entities;
internal class User
{
    public Guid Id { get; private set; }
    public Guid RoleId { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public UserState State { get; private set; }
    public DateTime CreatedDate { get; private set; }
}


