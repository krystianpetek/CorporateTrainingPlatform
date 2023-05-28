using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class Role : AuditableEntity
{
	internal static string DefaultRole => "Customer";

	public string Name { get; private set; } // TODO ValueObject
	public IEnumerable<string> Permissions { get; private set; } // TODO ValueObject

	private Role() { }

	public Role(string name, IEnumerable<string> permissions)
	{
		Name = name;
		Permissions = permissions;
	}
}
