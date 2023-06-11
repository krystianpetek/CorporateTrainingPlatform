namespace GarageGenius.Shared.Abstractions.Exceptions;
public sealed class AuthorizationRequirementException : GarageGeniusException
{
	public AuthorizationRequirementException(string policyName) : base($"You do not have the required permissions to perform this action {policyName}.", System.Net.HttpStatusCode.Forbidden)
	{
	}
}
