using GarageGenius.Modules.Users.Core.ValueObjects;

namespace GarageGenius.Modules.Users.Core.Entities;
internal record User
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Email Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public UserState State { get; set; }
    //public DateTime CreatedDate { get; private set; }
}


