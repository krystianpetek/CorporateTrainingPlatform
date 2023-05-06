using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Customers.Api.Controllers;
public class CustomersController : BaseController
{
    private readonly IDispatcher _dispatcher;

    [HttpGet("health-check")]
    [SwaggerOperation("Health check ")]
    public ActionResult<string> HealthCheck()
    {
        var response = new { message = "Customers module - I'm alive." };
        return Ok(response);
    }

    public CustomersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
