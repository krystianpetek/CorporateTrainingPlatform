using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Shared.Queries.GetVehicleById;
using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
internal class GetCustomerReservationsQueryHandler : IPagedQueryHandler<GetCustomerReservationsQuery, GetCustomerReservationsQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;
	private readonly IQueryDispatcher _queryDispatcher;

	public GetCustomerReservationsQueryHandler(
		Serilog.ILogger logger,
		IQueryDispatcher queryDispatcher,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_queryDispatcher = queryDispatcher;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<GetCustomerReservationsQueryDto> HandlePagedQueryAsync(GetCustomerReservationsQuery query, CancellationToken cancellationToken = default)
	{
		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = await _reservationQueryStorage.GetCustomerReservationsAsync(query, cancellationToken);
		foreach (var vehicle in getCustomerReservationsQueryDto?.CustomerReservationsDto?.Items)
		{
			// TODO - cache for vehicles
			var dispatchedVehicle = await _queryDispatcher.DispatchQueryAsync<GetVehicleByIdQueryDto>(new GetVehicleByIdQuery(vehicle.VehicleId), cancellationToken);
			vehicle.VehicleName = $"{dispatchedVehicle.Manufacturer} {dispatchedVehicle.Model}";
		}

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved customer reservations for customer with ID: {CustomerId}",
			nameof(GetCustomerReservationsQuery),
			nameof(Reservations),
			query.CustomerId);

		return getCustomerReservationsQueryDto;
	}
}
