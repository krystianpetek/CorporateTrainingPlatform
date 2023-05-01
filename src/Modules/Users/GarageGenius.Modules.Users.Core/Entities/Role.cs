namespace GarageGenius.Modules.Users.Core.Entities;
internal record Role
{
    public static string DefaultRole => "user";

    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}
