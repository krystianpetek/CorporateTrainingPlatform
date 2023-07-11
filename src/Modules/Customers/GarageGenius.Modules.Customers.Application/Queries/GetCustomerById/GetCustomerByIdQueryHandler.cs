using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
{
	private readonly ICustomerRepository _customerRepository;
	private readonly ICustomerMapperService _customerMapperService;

	public GetCustomerByIdQueryHandler(
		ICustomerRepository customerRepository,
		ICustomerMapperService customerMapperService)
	{
		_customerRepository = customerRepository;
		_customerMapperService = customerMapperService;
	}

	public async Task<GetCustomerByIdDto> HandleQueryAsync(GetCustomerByIdQuery query, CancellationToken cancellationToken = default)
	{
		Customer? customer = await _customerRepository.GetCustomerByIdAsync(query.Id, cancellationToken);

		GetCustomerByIdDto getCustomerByIdDto = _customerMapperService.MapToGetCustomerByIdDto(customer);
		return getCustomerByIdDto;
	}
}
