using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerByIdDto> HandleQueryAsync(GetCustomerByIdQuery query, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(query.Id, cancellationToken);
        return new GetCustomerByIdDto(customer.CustomerId, customer.FirstName, customer.LastName, customer.PhoneNumber, customer.EmailAddress);
    }
}
