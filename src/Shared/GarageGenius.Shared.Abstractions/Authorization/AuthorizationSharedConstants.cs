using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Shared.Abstractions.Authorization;
public static class AuthorizationSharedConstants
{
	public static readonly string AdministratorRequirement = "administrator";
	public static readonly string ManagerRequirement = "manager";
	public static readonly string EmployeeRequirement = "employee";
	public static readonly string CustomerRequirement = "customer";

	public static readonly OperationAuthorizationRequirement CreateRequirement = new OperationAuthorizationRequirement() { Name = nameof(CreateRequirement) };
	public static readonly OperationAuthorizationRequirement ReadRequirement = new OperationAuthorizationRequirement() { Name = nameof(ReadRequirement) };
	public static readonly OperationAuthorizationRequirement UpdateRequirement = new OperationAuthorizationRequirement() { Name = nameof(UpdateRequirement) };
	public static readonly OperationAuthorizationRequirement DeleteRequirement = new OperationAuthorizationRequirement() { Name = nameof(DeleteRequirement) };
}
