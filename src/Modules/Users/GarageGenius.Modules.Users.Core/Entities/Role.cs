namespace GarageGenius.Modules.Users.Core.Entities;
internal record Role
{
    public static string DefaultRole => "user";

    public Guid RoleId { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<string> Permissions { get; private set; }
}
