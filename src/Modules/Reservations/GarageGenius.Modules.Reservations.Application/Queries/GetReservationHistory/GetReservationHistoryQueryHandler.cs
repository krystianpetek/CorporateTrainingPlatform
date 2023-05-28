using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
internal class GetReservationHistoryQueryHandler : IQueryHandler<GetReservationHistoryQuery, IReadOnlyList<GetReservationHistoryQueryDto>>
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationQueryStorage _reservationQueryStorage;

	public GetReservationHistoryQueryHandler(
		Serilog.ILogger logger,
		IReservationQueryStorage reservationQueryStorage)
	{
		_logger = logger;
		_reservationQueryStorage = reservationQueryStorage;
	}

	public async Task<IReadOnlyList<GetReservationHistoryQueryDto>> HandleQueryAsync(GetReservationHistoryQuery query, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<GetReservationHistoryQueryDto>? getReservationHistoryQueryDtos = await _reservationQueryStorage.GetReservationHistoryAsync(query.ReservationId, cancellationToken) ?? throw new ReservationNotFoundException(query.ReservationId);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved history for reservation with ID: {ReservationId}",
			nameof(GetReservationHistoryQuery),
			nameof(Reservations),
			query.ReservationId);

		return getReservationHistoryQueryDtos;
	}
}
