using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
internal class GetCustomerReservationsQueryHandler : IQueryHandler<GetCustomerReservationsQuery, GetCustomerReservationsQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;

	public GetCustomerReservationsQueryHandler(
		Serilog.ILogger logger,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<GetCustomerReservationsQueryDto> HandleQueryAsync(GetCustomerReservationsQuery query, CancellationToken cancellationToken = default)
	{
		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = await _reservationQueryStorage.GetCustomerReservationsAsync(query.CustomerId, cancellationToken);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved customer reservations for customer with ID: {CustomerId}",
			nameof(GetCustomerReservationsQuery),
			nameof(Reservations),
			query.CustomerId);

		return getCustomerReservationsQueryDto;
	}
}
