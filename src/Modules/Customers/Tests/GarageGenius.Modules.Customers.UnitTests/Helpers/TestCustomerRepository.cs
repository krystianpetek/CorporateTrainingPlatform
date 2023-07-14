using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace GarageGenius.Modules.Customers.UnitTests.Helpers;

[ExcludeFromCodeCoverage]
internal class TestCustomerRepository : ICustomerRepository
{
    public List<Customer> Customers { get; set; }

    public TestCustomerRepository()
    {
        Customers = new List<Customer>();
    }

    public TestCustomerRepository(List<Customer> customers)
    {
        Customers = customers;
    }

    public Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        Customers.Add(customer);
        return Task.CompletedTask;
    }

    public Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Customers.Find(x => x.CustomerId == id));
    }

    public Task<Customer?> GetCustomerByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Customers.Find(x => x.UserId == userId));
    }

    public Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerToUpdate = Customers.Find(x => x.CustomerId == customer.CustomerId);
        if (customerToUpdate != null)
        {
            Customers.Remove(customerToUpdate);
            Customers.Add(customer);
        }
        return Task.CompletedTask;
    }
}
