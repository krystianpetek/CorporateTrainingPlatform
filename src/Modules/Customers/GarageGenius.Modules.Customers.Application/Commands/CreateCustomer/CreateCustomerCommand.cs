using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
public record CreateCustomerCommand : ICommand
{
	[DefaultValue("krystianpetek2@gmail.com")]
	public string EmailAddress { get; init; }
	[DefaultValue("Krystian")]
	public string? FirstName { get; init; }
	[DefaultValue("Petek")]
	public string? LastName { get; init; }
	[DefaultValue("123456789")]
	public string? PhoneNumber { get; init; }
}
