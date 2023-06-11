using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Shared.Queries.GetUserIdByCustomerId;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetUserIdByCustomerId;
internal class GetUserIdByCustomerIdQueryHandler : IQueryHandler<GetUserIdByCustomerIdQuery, GetUserIdByCustomerIdDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly ICustomerRepository _customerRepository;

	public GetUserIdByCustomerIdQueryHandler(
		Serilog.ILogger logger,
		ICustomerRepository customerRepository)
	{
		_logger = logger;
		_customerRepository = customerRepository;
	}
	public async Task<GetUserIdByCustomerIdDto> HandleQueryAsync(GetUserIdByCustomerIdQuery query, CancellationToken cancellationToken = default)
	{
		Customer? customer = await _customerRepository.GetCustomerByIdAsync(query.Id, cancellationToken);

		GetUserIdByCustomerIdDto getUserIdByCustomerIdDto = new GetUserIdByCustomerIdDto(customer.UserId);
		_logger.Information(
			"Handled {QueryName} in {ModuleName} module, retrieved user with ID: {UserId}",
			nameof(GetUserIdByCustomerId), nameof(Customers), customer.UserId);

		return getUserIdByCustomerIdDto;
	}
}
