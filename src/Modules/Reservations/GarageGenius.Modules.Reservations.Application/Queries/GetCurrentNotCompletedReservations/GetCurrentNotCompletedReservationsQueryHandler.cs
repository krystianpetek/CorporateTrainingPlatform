using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries.PagedQuery;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
internal class GetCurrentNotCompletedReservationsQueryHandler : IPagedQueryHandler<GetCurrentNotCompletedReservationsQuery, GetCurrentNotCompletedReservationsQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;

	public GetCurrentNotCompletedReservationsQueryHandler(
		Serilog.ILogger logger,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<GetCurrentNotCompletedReservationsQueryDto> HandlePagedQueryAsync(GetCurrentNotCompletedReservationsQuery query, CancellationToken cancellationToken = default)
	{
		GetCurrentNotCompletedReservationsQueryDto getCurrentNotCompletedReservationsQueryDto = await _reservationQueryStorage.GetCurrentNotCompletedReservationsAsync(query, cancellationToken);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved all current not completed reservations",
			nameof(GetCustomerReservationsQuery),
			nameof(Reservations));

		return getCurrentNotCompletedReservationsQueryDto;
	}
}
