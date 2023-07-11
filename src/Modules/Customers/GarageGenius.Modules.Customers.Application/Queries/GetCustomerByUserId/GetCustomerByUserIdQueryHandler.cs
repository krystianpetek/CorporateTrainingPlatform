using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
internal class GetCustomerByUserIdQueryHandler : IQueryHandler<GetCustomerByUserIdQuery, GetCustomerByUserIdDto>
{
	private readonly ICustomerRepository _customerRepository;
	private readonly ICustomerMapperService _customerMapperService;

	public GetCustomerByUserIdQueryHandler(
		ICustomerRepository customerRepository,
		ICustomerMapperService customerMapperService)
	{
		_customerRepository = customerRepository;
		_customerMapperService = customerMapperService;
	}

	public async Task<GetCustomerByUserIdDto> HandleQueryAsync(GetCustomerByUserIdQuery query, CancellationToken cancellationToken = default)
	{
		Customer? customer = await _customerRepository.GetCustomerByUserIdAsync(query.UserId);
		GetCustomerByUserIdDto getCustomerByUserIdDto = _customerMapperService.MapToGetCustomerByUserIdDto(customer);
		return getCustomerByUserIdDto;
	}
}
