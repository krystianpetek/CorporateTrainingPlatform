using GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using GarageGenius.Shared.Abstractions.Helpers;
using GarageGenius.Shared.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.QueryStorage;
internal class ReservationQueryStorage : IReservationQueryStorage
{
	private readonly ReservationsDbContext _reservationsDbContext;
	private readonly ISystemDateService _systemDateService;

	public ReservationQueryStorage(
		ReservationsDbContext reservationsDbContext,
		ISystemDateService systemDateService)
	{
		_reservationsDbContext = reservationsDbContext;
		_systemDateService = systemDateService;
	}

	public async Task<GetReservationQueryDto?> GetReservationAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		GetReservationQueryDto? getReservationQueryDto = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where<Reservation>(reservation => reservation.ReservationId == reservationId)
		.Select<Reservation, GetReservationQueryDto>(reservation => new GetReservationQueryDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState))
		.SingleOrDefaultAsync<GetReservationQueryDto>(cancellationToken);

		return getReservationQueryDto;
	}

	public async Task<GetReservationHistoryQueryDtos> GetReservationHistoryAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<ReservationHistoriesDto> reservationHistoriesDto = await _reservationsDbContext.ReservationHistories
		.AsNoTracking()
		.AsQueryable()
		.Where<ReservationHistory>(reservationHistory => reservationHistory.ReservationId == reservationId)
		.Select<ReservationHistory, ReservationHistoriesDto>(reservationHistory => new ReservationHistoriesDto(reservationHistory.ReservationHistoryId, reservationHistory.Created ,reservationHistory.ReservationState, reservationHistory.Comment))
		.ToListAsync(cancellationToken);

		GetReservationHistoryQueryDtos getReservationHistoryQueryDto = new GetReservationHistoryQueryDtos(reservationId, reservationHistoriesDto);

		return getReservationHistoryQueryDto;

	}

	public async Task<GetCustomerReservationsQueryDto> GetCustomerReservationsAsync(GetCustomerReservationsQuery getCustomerReservationsQuery, CancellationToken cancellationToken = default)
	{
		PaginatedList<CustomerReservationsDto> customerReservationsDtos = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where<Reservation>(reservation => reservation.CustomerId == getCustomerReservationsQuery.CustomerId)
		.Select<Reservation, CustomerReservationsDto>(reservation => new CustomerReservationsDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState, reservation.ReservationDate.Value, reservation.ReservationNote))
		.PaginateAsync<CustomerReservationsDto>(getCustomerReservationsQuery.PageNumber, getCustomerReservationsQuery.PageSize, cancellationToken);

		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = new GetCustomerReservationsQueryDto(getCustomerReservationsQuery.CustomerId, customerReservationsDtos);

		return getCustomerReservationsQueryDto;
	}

	public async Task<GetVehicleReservationsQueryDto> GetVehicleReservationsAsync(Guid vehicleId, CancellationToken cancellationToken = default)
	{
		List<VehicleReservationsDto> vehicleReservationsDto = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where<Reservation>(reservation => reservation.VehicleId == vehicleId)
		.Select<Reservation, VehicleReservationsDto>(reservation => new VehicleReservationsDto(reservation.ReservationId, reservation.ReservationState, reservation.ReservationDate.Value, reservation.ReservationNote))
		.ToListAsync<VehicleReservationsDto>(cancellationToken);

		GetVehicleReservationsQueryDto getVehicleReservationsQueryDto = new GetVehicleReservationsQueryDto(vehicleId, vehicleReservationsDto);
		return getVehicleReservationsQueryDto;
	}

	public async Task<GetCurrentNotCompletedReservationsQueryDto> GetCurrentNotCompletedReservationsAsync(GetCurrentNotCompletedReservationsQuery getCurrentNotCompletedReservationsQuery, CancellationToken cancellationToken)
	{
		PaginatedList<CurrentNotCompletedReservationsDto> currentNotCompletedReservationsDtos = await _reservationsDbContext.Reservations
			.AsNoTracking()
			.AsQueryable()
			.Where(reservation => reservation.ReservationDate < _systemDateService.GetCurrentDate())
			.Where(reservation => !(reservation.ReservationDeleted))
			.Where(reservation =>
				reservation.ReservationState != ReservationState.Completed &&
				reservation.ReservationState != ReservationState.Canceled)
			.Select<Reservation, CurrentNotCompletedReservationsDto>(reservation =>
				new CurrentNotCompletedReservationsDto(
					reservation.ReservationId,
					reservation.VehicleId,
					reservation.CustomerId,
					reservation.ReservationState,
					reservation.ReservationDate.Value,
					reservation.ReservationNote))
			.PaginateAsync(getCurrentNotCompletedReservationsQuery.PageNumber, getCurrentNotCompletedReservationsQuery.PageSize, cancellationToken);

		GetCurrentNotCompletedReservationsQueryDto getCurrentNotCompletedReservationsQueryDto = new GetCurrentNotCompletedReservationsQueryDto(currentNotCompletedReservationsDtos);

		return getCurrentNotCompletedReservationsQueryDto;
	}
}
