using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
internal class GetCustomerByUserIdQueryHandler : IQueryHandler<GetCustomerByUserIdQuery, GetCustomerByUserIdDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerByUserIdDto> HandleQueryAsync(GetCustomerByUserIdQuery query, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByUserIdAsync(query.UserId);
        return new GetCustomerByUserIdDto(customer.CustomerId, customer.FirstName, customer.LastName, customer.PhoneNumber, customer.EmailAddress);
    }
}
