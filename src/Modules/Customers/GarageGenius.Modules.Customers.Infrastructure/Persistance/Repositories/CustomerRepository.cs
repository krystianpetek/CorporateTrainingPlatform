using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
internal class CustomerRepository : ICustomerRepository
{
    private readonly CustomersDbContext _customersDbContext;

    public CustomerRepository(CustomersDbContext customersDbContext)
    {
        _customersDbContext = customersDbContext;
    }

    public async Task AddAsync(Customer customer)
    {
        await _customersDbContext.Customers.AddAsync(customer);
        await _customersDbContext.SaveChangesAsync();
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
