using GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
using GarageGenius.Modules.Vehicles.Application.Commands.UpdateVehicleOwner;
using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.SearchVehicles;
using GarageGenius.Modules.Vehicles.Core.Models;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Vehicles.Api.Controllers;
public class VehiclesController : BaseController
{
	private readonly IDispatcher _dispatcher;

	public VehiclesController(IDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
	}

	[HttpGet("{vehicleId:guid}")]
	[Authorize]
	[SwaggerOperation("Get vehicle")]
	[SwaggerResponse(StatusCodes.Status200OK, "Vehicle details", typeof(GetVehicleQueryDto))]
	public async Task<ActionResult> GetVehicleAsync(Guid vehicleId)
	{
		GetVehicleQuery getVehicleQuery = new GetVehicleQuery(vehicleId);
		GetVehicleQueryDto getVehicleQueryDto = await _dispatcher.DispatchQueryAsync(getVehicleQuery);
		return Ok(getVehicleQueryDto);
	}

	[HttpGet("{customerId:guid}/vehicles")]
	[Authorize]
	[SwaggerOperation("Get customer vehicles")]
	[SwaggerResponse(StatusCodes.Status200OK, "Customer vehicles", typeof(IReadOnlyList<GetCustomerVehiclesQueryDto>))]
	public async Task<ActionResult> GetCustomerVehiclesAsync(Guid customerId)
	{
		GetCustomerVehiclesQuery getCustomerVehiclesQuery = new GetCustomerVehiclesQuery(customerId);
		IReadOnlyList<GetCustomerVehiclesQueryDto> customerVehicles = await _dispatcher.DispatchQueryAsync(getCustomerVehiclesQuery);
		return Ok(customerVehicles);
	}

	[HttpGet("search")]
	[Authorize]
	[SwaggerOperation("Search vehicles by VIN number and license plate")]
	[SwaggerResponse(StatusCodes.Status200OK, "Vehicles", typeof(IReadOnlyList<SearchVehiclesQueryDto>))]
	public async Task<ActionResult> SearchVehiclesAsync([FromQuery] SearchVehiclesParameters searchVehiclesParameters)
	{
		SearchVehiclesQuery searchVehiclesQuery = new SearchVehiclesQuery(searchVehiclesParameters);
		IReadOnlyList<SearchVehiclesQueryDto> searchVehiclesQueryDto = await _dispatcher.DispatchQueryAsync(searchVehiclesQuery);

		return Ok(searchVehiclesQueryDto);
	}

	[HttpPost("customers/{customerId:guid}/vehicle")]
	[Authorize]
	[SwaggerOperation("Add customer vehicle")]
	[SwaggerResponse(StatusCodes.Status202Accepted, "Vehicle added")]
	public async Task<ActionResult> AddVehicleAsync(Guid customerId, AddVehicleCommand addVehicleCommand)
	{
		addVehicleCommand.CustomerId = customerId;
		await _dispatcher.DispatchCommandAsync<AddVehicleCommand>(addVehicleCommand);
		return Accepted();
	}

	[HttpPatch("{vehicleId:guid}/customer")]
	[Authorize]
	[SwaggerOperation("Update vehicle owner")]
	[SwaggerResponse(StatusCodes.Status200OK, "Vehicle owner updated")]
	public async Task<ActionResult> UpdateVehicleOwner(Guid vehicleId, UpdateVehicleOwnerCommand updateVehicleOwnerCommand)
	{
		updateVehicleOwnerCommand.VehicleId = vehicleId;
		await _dispatcher.DispatchCommandAsync<UpdateVehicleOwnerCommand>(updateVehicleOwnerCommand);
		return Ok();
	}
}
