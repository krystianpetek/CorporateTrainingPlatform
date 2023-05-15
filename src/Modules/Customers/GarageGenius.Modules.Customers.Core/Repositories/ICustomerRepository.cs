using GarageGenius.Modules.Customers.Core.Entities;

namespace GarageGenius.Modules.Customers.Core.Repositories;
internal interface ICustomerRepository
{
    Task<Customer?> GetAsync(Guid id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);

}
