using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
internal class CustomerRepository : ICustomerRepository
{
    public Task AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
