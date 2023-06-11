using GarageGenius.Modules.Customers.Shared.Queries.GetUserIdByCustomerId;
using GarageGenius.Modules.Vehicles.Application.Queries.SearchVehicles;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Modules.Vehicles.Shared;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Exceptions;
using GarageGenius.Shared.Abstractions.Queries.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
internal class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly IVehicleRepository _vehiclesRepository;
	private readonly IVehicleQueryStorage _vehicleQueryStorage;
	private readonly IQueryDispatcher _queryDispatcher;
	private readonly IAuthorizationService _authorizationService;
	private readonly IHttpContextAccessor httpContextAccessor;

	public AddVehicleCommandHandler(
		Serilog.ILogger logger,
		IVehicleRepository vehiclesRepository,
		IVehicleQueryStorage vehicleQueryStorage,
		IQueryDispatcher queryDispatcher,
		IAuthorizationService authorizationService,
		IHttpContextAccessor httpContextAccessor
		)
	{
		_logger = logger;
		_vehiclesRepository = vehiclesRepository;
		_vehicleQueryStorage = vehicleQueryStorage;
		_queryDispatcher = queryDispatcher;
		_authorizationService = authorizationService;
		this.httpContextAccessor = httpContextAccessor;
	}

	public async Task HandleCommandAsync(AddVehicleCommand command, CancellationToken cancellationToken = default)
	{
		// query for fetch user id from Customers module by customerId
		GetUserIdByCustomerIdDto? userId = await _queryDispatcher.DispatchQueryAsync(new GetUserIdByCustomerIdQuery(command.CustomerId), cancellationToken);
		Vehicle vehicle = new Vehicle(command.CustomerId, command.Manufacturer, command.Model, command.LicensePlate, command.Vin, command.Year, userId.UserId);

		AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, vehicle, VehiclesPolicyConstants.AddVehiclePolicy);
		if (!authorizationResult.Succeeded) {
			throw new AuthorizationRequirementException(VehiclesPolicyConstants.AddVehiclePolicy);
		}

		IReadOnlyList<SearchVehiclesQueryDto> vehicleQuery = await _vehicleQueryStorage.SearchVehicleAsync(new Core.Models.SearchVehiclesParameters(command.Vin, null), cancellationToken);
		if (vehicleQuery.Any(x => x.Vin == command.Vin)) throw new VehicleAlreadyExistsException(command.Vin);
		
		await _vehiclesRepository.AddVehicleAsync(vehicle, cancellationToken);
		_logger.Information(
			messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new vehicle with ID: {VehicleId} to customer with ID: {CustomerId}",
			nameof(AddVehicleCommand),
			nameof(Vehicles),
			vehicle.VehicleId,
			command.CustomerId);
	}
}
