using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Customers.Core.Exceptions;
internal class CustomerNotFoundException : GarageGeniusException
{
    public CustomerNotFoundException(Guid id) : base($"User with ID: '{id}' was not found.") { }
}
