using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
public record UpdateCustomerCommand : ICommand
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? PhoneNumber { get; init; }
}
