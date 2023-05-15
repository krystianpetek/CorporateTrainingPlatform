using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
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

    [HttpPost]
    [Authorize]
    [SwaggerOperation("Create customer")]
    public async Task<ActionResult> CreateCustomer(CreateCustomerCommand createCustomerCommand)
    {
        await _dispatcher.SendAsync<CreateCustomerCommand>(createCustomerCommand);
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [SwaggerOperation("Update customer")]
    public async Task<ActionResult> UpdateCustomer(UpdateCustomerCommand updateCustomerCommand)
    {
        await _dispatcher.SendAsync<UpdateCustomerCommand>(updateCustomerCommand);
        return NoContent();
    }
}
