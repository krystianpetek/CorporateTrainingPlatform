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
	public IActionResult GetReservationStates()
	{
		return Ok(new { ReservationStates = ReservationState.GetAvailableStates });
	}

	[HttpPut]
	[Authorize]
	[SwaggerOperation(Summary = "Update reservation", Description = "Allow customer to update reservation when its state is other than ...?")]
	public async Task<ActionResult> UpdateReservationAsync(UpdateReservationCommand command)
	{
		await _dispatcher.DispatchCommandAsync<UpdateReservationCommand>(command);
		return Ok();
	}

	[HttpPost]
	[Authorize]
	[SwaggerOperation("Add reservation")]
	public async Task<IActionResult> AddReservationAsync(AddReservationCommand command)
	{
		// TODO add reservation date to command parameters with date validation
		await _dispatcher.DispatchCommandAsync<AddReservationCommand>(command);
		return Ok();
	}

	[HttpGet("{reservationId:guid}")]
	[Authorize]
	[SwaggerOperation("Get reservation")]
	public async Task<ActionResult> GetReservationAsync(Guid reservationId)
	{
		GetReservationQuery query = new GetReservationQuery(reservationId);
		GetReservationQueryDto? getReservationQueryDto = await _dispatcher.DispatchQueryAsync(query);
		return Ok(getReservationQueryDto);
	}

	[HttpGet("{reservationId:guid}/history")]
	[Authorize]
	[SwaggerOperation("Get reservation history")]
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
	public async Task<ActionResult> GetCustomerReservationsAsync([FromQuery] GetCustomerReservationsQuery getCustomerReservationsQuery)
	{
		GetCustomerReservationsQueryDto getCustomerReservationsQueryDto = await _dispatcher.DispatchPagedQueryAsync(getCustomerReservationsQuery);
		return Ok(getCustomerReservationsQueryDto);
	}

	[HttpGet("vehicle/{vehicleId:guid}/reservations")]
	[Authorize]
	[SwaggerOperation("Get vehicle reservations")]
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
	public async Task<ActionResult> GetCurrentNotCompletedReservationsAsync([FromQuery] GetCurrentNotCompletedReservationsQuery getCurrentNotCompletedReservationsQuery)
	{
		GetCurrentNotCompletedReservationsQueryDto getCurrentNotCompletedReservationsQueryDto = await _dispatcher.DispatchPagedQueryAsync(getCurrentNotCompletedReservationsQuery);
		return Ok(getCurrentNotCompletedReservationsQueryDto);
	}

	[HttpPost("{reservationId:guid}/complete")]
	[Authorize]
	[SwaggerOperation(Summary = "Complete reservation", Description = "Mark reservation as completed, when all works with vehicle is done.")]
	public async Task<ActionResult> CompleteReservationAsync(Guid reservationId, CompleteReservationCommand completeReservationCommand)
	{
		completeReservationCommand.ReservationId = reservationId;
		await _dispatcher.DispatchCommandAsync<CompleteReservationCommand>(completeReservationCommand);
		return Ok();
	}

	// TODO - add description for all controller actions
}
