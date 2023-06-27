using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
internal class GetVehicleReservationsQueryHandler : IQueryHandler<GetVehicleReservationsQuery, GetVehicleReservationsQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;

	public GetVehicleReservationsQueryHandler(
		Serilog.ILogger logger,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<GetVehicleReservationsQueryDto> HandleQueryAsync(GetVehicleReservationsQuery query, CancellationToken cancellationToken = default)
	{
		// TODO policy to check if user is allowed to get reservations for this vehicle
		GetVehicleReservationsQueryDto getVehicleReservationsQueryDto = await _reservationQueryStorage.GetVehicleReservationsAsync(query.VehicleId, cancellationToken);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicle reservations for vehicle with ID: {VehicleId}",
			nameof(GetVehicleReservationsQuery),
			nameof(Reservations),
			query);

		return getVehicleReservationsQueryDto;
	}
}
