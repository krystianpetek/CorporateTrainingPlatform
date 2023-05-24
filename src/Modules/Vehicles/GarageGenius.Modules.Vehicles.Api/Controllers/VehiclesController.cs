using GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.GetFilteredVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Modules.Vehicles.Core.Models;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> GetVehicleAsync(Guid vehicleId)
    {
        GetVehicleQuery getVehicleQuery = new GetVehicleQuery(vehicleId);
        GetVehicleQueryDto getVehicleQueryDto = await _dispatcher.DispatchQueryAsync(getVehicleQuery);
        return Ok(getVehicleQueryDto);
    }

    [HttpGet("{customerId:guid}/vehicles")]
    [Authorize]
    [SwaggerOperation("Get customer vehicles")]
    public async Task<ActionResult> GetCustomerVehiclesAsync(Guid customerId)
    {
        GetCustomerVehiclesQuery getCustomerVehiclesQuery = new GetCustomerVehiclesQuery(customerId);
        IReadOnlyList<GetCustomerVehiclesQueryDto> customerVehicles = await _dispatcher.DispatchQueryAsync(getCustomerVehiclesQuery);
        return Ok(customerVehicles);
    }

    [HttpGet("search")]
    [Authorize]
    [SwaggerOperation("Get filtered vehicles")]
    public async Task<ActionResult> GetFilteredVehicleAsync([FromQuery] GetVehicleFilterParameters getFilteredVehicleParameters)
    {
        GetVehicleFilterQuery getFilteredVehicleQuery = new GetVehicleFilterQuery(getFilteredVehicleParameters);
        GetVehicleFilterQueryDto getVehicleFilterQueryDto = await _dispatcher.DispatchQueryAsync(getFilteredVehicleQuery);

        return Ok(getVehicleFilterQueryDto);
    }

    [HttpPost("customers/{customerId:guid}/vehicle")]
    [Authorize]
    [SwaggerOperation("Add customer vehicle")]
    public async Task<ActionResult> AddVehicleAsync(Guid customerId, AddVehicleCommand addVehicleCommand)
    {
        addVehicleCommand.CustomerId = customerId;
        await _dispatcher.DispatchCommandAsync(addVehicleCommand);
        return Accepted();
    }

    // TODO search by Vin and license plate
    // TODO Controllers response types
}
