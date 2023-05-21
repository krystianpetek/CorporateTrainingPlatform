using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Customers.Api.Controllers;
public class CustomersController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CustomersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [SwaggerOperation("Get customer id")]
    public async Task<ActionResult> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _dispatcher.DispatchQueryAsync<GetCustomerByIdDto>(new GetCustomerByIdQuery(id));
        return Ok(customer);
    }

    [HttpGet("User/{id:guid}")]
    [Authorize]
    [SwaggerOperation("Get customer by user id")]
    public async Task<ActionResult> GetCustomerByUserIdAsync(Guid id)
    {
        var customer = await _dispatcher.DispatchQueryAsync<GetCustomerByUserIdDto>(new GetCustomerByUserIdQuery(id));
        return Ok(customer);
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation("Create customer")]
    public async Task<ActionResult> CreateCustomerAsync(CreateCustomerCommand createCustomerCommand)
    {
        await _dispatcher.DispatchCommandAsync<CreateCustomerCommand>(createCustomerCommand);
        return Accepted();
    }

    [HttpPut]
    [Authorize]
    [SwaggerOperation("Update customer")]
    public async Task<ActionResult> UpdateCustomerAsync(UpdateCustomerCommand updateCustomerCommand)
    {
        await _dispatcher.DispatchCommandAsync<UpdateCustomerCommand>(updateCustomerCommand);
        return NoContent();
    }
}
