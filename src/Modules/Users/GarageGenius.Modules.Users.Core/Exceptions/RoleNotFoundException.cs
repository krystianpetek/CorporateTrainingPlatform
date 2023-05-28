using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Users.Core.Exceptions;
internal class RoleNotFoundException : GarageGeniusException
{
	public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.") { }
}
