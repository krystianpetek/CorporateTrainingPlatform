using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
public record CreateCustomerCommand : ICommand
{
    [Required]
    public string EmailAddress { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? PhoneNumber { get; init; }
}
