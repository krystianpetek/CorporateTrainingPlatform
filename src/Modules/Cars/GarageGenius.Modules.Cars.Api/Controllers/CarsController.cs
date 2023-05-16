using GarageGenius.Modules.Cars.Application.Commands.AddCar;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Cars.Api.Controllers;
public class CarsController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CarsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("{customerId:guid}/car")]
    [Authorize]
    [SwaggerOperation("Add car")]
    public async Task<ActionResult> AddCarAsync(Guid customerId, AddCarCommand addCarCommand)
    {
        addCarCommand.CustomerId = customerId;
        await _dispatcher.SendAsync<AddCarCommand>(addCarCommand);
        return NoContent();
    }
}
