using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Shared.Abstractions.Authorization;
public static class AuthorizationSharedConstants
{
	public static readonly string AdministratorRequirement = nameof(AdministratorRequirement);
	public static readonly string ManagerRequirement = nameof(ManagerRequirement);
	public static readonly string EmployeeRequirement = nameof(EmployeeRequirement);
	public static readonly string CustomerRequirement = nameof(CustomerRequirement);

	public static readonly OperationAuthorizationRequirement CreateRequirement = new OperationAuthorizationRequirement() { Name = nameof(CreateRequirement) };
	public static readonly OperationAuthorizationRequirement ReadRequirement = new OperationAuthorizationRequirement() { Name = nameof(ReadRequirement) };
	public static readonly OperationAuthorizationRequirement UpdateRequirement = new OperationAuthorizationRequirement() { Name = nameof(UpdateRequirement) };
	public static readonly OperationAuthorizationRequirement DeleteRequirement = new OperationAuthorizationRequirement() { Name = nameof(DeleteRequirement) };
}
