using GarageGenius.Modules.Cars.Application.Commands.AddCar;
using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
using GarageGenius.Modules.Cars.Application.Queries.GetCustomerCarsQuery;
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
        return Accepted(); // TODO Controllers response types
    }

    [HttpGet("{customerId:guid}/cars")]
    [Authorize]
    [SwaggerOperation("Get customer cars")]
    public async Task<ActionResult> GetCustomerCarsAsync(Guid customerId)
    {
        GetCustomerCarsQuery getCustomerCarsQuery = new GetCustomerCarsQuery(customerId);
        IReadOnlyList<GetCarDto> customerCars = await _dispatcher.QueryAsync<IReadOnlyList<GetCarDto>>(getCustomerCarsQuery);
        return Ok(customerCars);
    }

    [HttpGet("{carId:guid}")]
    [Authorize]
    [SwaggerOperation("Get car")]
    public async Task<ActionResult> GetCarAsync(Guid carId)
    {
        GetCarQuery getCarQuery = new GetCarQuery(carId);
        GetCarDto carDto = await _dispatcher.QueryAsync<GetCarDto>(getCarQuery);
        return Ok(carDto);
    }
}
