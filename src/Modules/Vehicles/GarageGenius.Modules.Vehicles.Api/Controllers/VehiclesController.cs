using GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
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

    [HttpPost("{customerId:guid}/vehicle")]
    [Authorize]
    [SwaggerOperation("Add vehicle")]
    public async Task<ActionResult> AddVehicleAsync(Guid customerId, AddVehicleCommand addVehicleCommand)
    {
        addVehicleCommand.CustomerId = customerId;
        await _dispatcher.SendAsync(addVehicleCommand);
        return Accepted(); // TODO Controllers response types
    }

    [HttpGet("{customerId:guid}/vehicles")]
    [Authorize]
    [SwaggerOperation("Get customer vehicles")]
    public async Task<ActionResult> GetCustomerVehiclesAsync(Guid customerId)
    {
        GetCustomerVehiclesQuery getCustomerVehiclesQuery = new GetCustomerVehiclesQuery(customerId);
        IReadOnlyList<GetVehicleQueryDto> customerVehicles = await _dispatcher.QueryAsync(getCustomerVehiclesQuery);
        return Ok(customerVehicles);
    }

    [HttpGet("{vehicleId:guid}")]
    [Authorize]
    [SwaggerOperation("Get vehicle")]
    public async Task<ActionResult> GetVehicleAsync(Guid vehicleId)
    {
        GetVehicleQuery getVehicleQuery = new GetVehicleQuery(vehicleId);
        GetVehicleQueryDto vehicleDto = await _dispatcher.QueryAsync(getVehicleQuery);
        return Ok(vehicleDto);
    }
}
