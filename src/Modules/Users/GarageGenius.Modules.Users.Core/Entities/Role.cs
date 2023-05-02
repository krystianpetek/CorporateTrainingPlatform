namespace GarageGenius.Modules.Users.Core.Entities;
internal record Role
{
    public static string DefaultRole => "user";

    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}
