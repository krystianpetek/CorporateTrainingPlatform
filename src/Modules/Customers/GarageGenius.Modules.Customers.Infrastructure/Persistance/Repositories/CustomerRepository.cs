using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Exceptions;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Customer> GetAsync(Guid id)
    {
        Customer customer = await _customersDbContext.Customers.SingleOrDefaultAsync(x => x.Id == id) ?? throw new CustomerNotFoundException(id);
        return customer;

    }

    public async Task UpdateAsync(Customer customer)
    {
        _customersDbContext.Customers.Update(customer);
        await _customersDbContext.SaveChangesAsync();
    }
}
