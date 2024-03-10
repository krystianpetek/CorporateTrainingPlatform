using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Modules.Reservations.Application.Commands.CompleteReservation;
using GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Modules.Reservations.Application.Queries.GetVehicleReservations;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Reservations.Api.Controllers;
public class ReservationsController : BaseController
{
	private readonly IDispatcher _dispatcher;

	public ReservationsController(IDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
	}

	[HttpGet("states")]
	[SwaggerOperation(Summary = "Get reservation states", Description = "Get all available reservation states, that can be used to update reservation state.)")]
	[SwaggerResponse(StatusCodes.Status200OK, "Reservation states", typeof(IReadOnlyCollection<ReservationState>))]
	public IActionResult GetReservationStates()
	{
		return Ok(new { ReservationStates = ReservationState.GetAvailableStates });
	}

	[HttpPut]
	[Authorize]
	[SwaggerOperation(Summary = "Update reservation", Description = "Allow customer to update reservation when its state is other than ...?")]
	[SwaggerResponse(StatusCodes.Status200OK, "Reservation updated")]
	public async Task<ActionResult> UpdateReservationAsync(UpdateReservationCommand command)
	{
		await _dispatcher.DispatchCommandAsync<UpdateReservationCommand>(command);
		return Ok();
	}

	[HttpPost]
	[Authorize]
	[SwaggerOperation("Add reservation")]
	[SwaggerResponse(StatusCodes.Status202Accepted, "Reservation added")]
	public async Task<IActionResult> AddReservationAsync(AddReservationCommand command)
	{
		// TODO add reservation date to command parameters with date validation
		await _dispatcher.DispatchCommandAsync<AddReservationCommand>(command);
		return Ok();
	}

	[HttpGet("{reservationId:guid}")]
	[Authorize]
	[SwaggerOperation("Get reservation")]
	[SwaggerResponse(StatusCodes.Status200OK, "Reservation details", typeof(GetReservationQueryDto))]
	public async Task<ActionResult> GetReservationAsync(Guid reservationId)
	{
		GetReservationQuery query = new GetReservationQuery(reservationId);
		GetReservationQueryDto? getReservationQueryDto = await _dispatcher.DispatchQueryAsync(query);
		return Ok(getReservationQueryDto);
	}

	[HttpGet("{reservationId:guid}/history")]
	[Authorize]
	[SwaggerOperation("Get reservation history")]
	[SwaggerResponse(StatusCodes.Status200OK, "Reservation history", typeof(GetReservationHistoryQueryDtos))]
	public async Task<ActionResult> GetReservationHistoryAsync(Guid reservationId)
	{
		// TODO - date
		GetReservationHistoryQuery query = new GetReservationHistoryQuery(reservationId);
		GetReservationHistoryQueryDtos getReservationHistoryQueryDto = await _dispatcher.DispatchQueryAsync(query);
		return Ok(getReservationHistoryQueryDto);
	}

	[HttpGet("customer")]
	[Authorize]
	[SwaggerOperation("Get customer reservations")]
	[SwaggerResponse(StatusCodes.Status200OK, "Customer reservations", typeof(GetCustomerReservationsQueryDto))]
	public async Task<ActionResult> GetCustomerReservationsAsync([FromQuery] GetCustomerReservationsQuery getCustomerReservationsQuery)
	{
		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = await _dispatcher.DispatchPagedQueryAsync(getCustomerReservationsQuery);
		return Ok(getCustomerReservationsQueryDto);
	}

	[HttpGet("vehicle/{vehicleId:guid}/reservations")]
	[Authorize]
	[SwaggerOperation("Get vehicle reservations")]
	[SwaggerResponse(StatusCodes.Status200OK, "Vehicle reservations", typeof(GetVehicleReservationsQueryDto))]
	public async Task<ActionResult> GetVehicleReservationsAsync(Guid vehicleId)
	{
		// TODO - order by reservation date
		GetVehicleReservationsQuery getVehicleReservationsQuery = new GetVehicleReservationsQuery(vehicleId);
		GetVehicleReservationsQueryDto getVehicleReservationsQueryDto = await _dispatcher.DispatchQueryAsync(getVehicleReservationsQuery);
		return Ok(getVehicleReservationsQueryDto);
	}

	[HttpGet("not-completed")]
	[Authorize]
	[SwaggerOperation("Get current not completed reservations")]
	[SwaggerResponse(StatusCodes.Status200OK, "Current not completed reservations", typeof(GetCurrentNotCompletedReservationsQueryDto))]
	public async Task<ActionResult> GetCurrentNotCompletedReservationsAsync([FromQuery] GetCurrentNotCompletedReservationsQuery getCurrentNotCompletedReservationsQuery)
	{
		GetCurrentNotCompletedReservationsQueryDto getCurrentNotCompletedReservationsQueryDto = await _dispatcher.DispatchPagedQueryAsync(getCurrentNotCompletedReservationsQuery);
		return Ok(getCurrentNotCompletedReservationsQueryDto);
	}

	[HttpPost("{reservationId:guid}/complete")]
	[Authorize]
	[SwaggerOperation(Summary = "Complete reservation", Description = "Mark reservation as completed, when all works with vehicle is done.")]
	[SwaggerResponse(StatusCodes.Status200OK, "Reservation completed")]
	public async Task<ActionResult> CompleteReservationAsync(Guid reservationId, CompleteReservationCommand completeReservationCommand)
	{
		completeReservationCommand.ReservationId = reservationId;
		await _dispatcher.DispatchCommandAsync<CompleteReservationCommand>(completeReservationCommand);
		return Ok();
	}

	// TODO - add description for all controller actions
}
