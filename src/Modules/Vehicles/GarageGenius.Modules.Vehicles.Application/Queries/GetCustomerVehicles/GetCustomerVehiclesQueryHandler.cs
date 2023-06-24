using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Shared;
using GarageGenius.Shared.Abstractions.Exceptions;
using GarageGenius.Shared.Abstractions.Queries.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles;
internal class GetCustomerVehiclesQueryHandler : IQueryHandler<GetCustomerVehiclesQuery, IReadOnlyList<GetCustomerVehiclesQueryDto>>
{
	private readonly Serilog.ILogger _logger;
	private readonly IVehicleQueryStorage _vehicleQueryStorage;
	private readonly IAuthorizationService _authorizationService;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public GetCustomerVehiclesQueryHandler(
		Serilog.ILogger logger,
		IAuthorizationService authorizationService,
		IVehicleQueryStorage vehicleQueryStorage,
		IHttpContextAccessor httpContextAccessor)
	{
		_logger = logger;
		_authorizationService = authorizationService;
		_vehicleQueryStorage = vehicleQueryStorage;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> HandleQueryAsync(GetCustomerVehiclesQuery query, CancellationToken cancellationToken = default)
	{
		AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, query.CustomerId, VehiclesPolicyConstants.GetCustomerVehiclesPolicy);
		if (!authorizationResult.Succeeded)
			throw new AuthorizationRequirementException(VehiclesPolicyConstants.GetCustomerVehiclesPolicy);

		IReadOnlyList<GetCustomerVehiclesQueryDto> customerVehicles = await _vehicleQueryStorage.GetCustomerVehiclesAsync(query.CustomerId, cancellationToken);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicles for customer with ID: {CustomerId}",
			nameof(GetCustomerVehiclesQuery),
			nameof(Vehicles),
			query.CustomerId);

		return customerVehicles;
	}
}
