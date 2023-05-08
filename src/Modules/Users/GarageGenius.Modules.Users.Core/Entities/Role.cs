using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class Role : AuditableEntity
{
    public static string DefaultRole => "user";

    public string Name { get; private set; }
    public IEnumerable<string> Permissions { get; private set; }

    private Role() { }

    public Role(string name, IEnumerable<string> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}
