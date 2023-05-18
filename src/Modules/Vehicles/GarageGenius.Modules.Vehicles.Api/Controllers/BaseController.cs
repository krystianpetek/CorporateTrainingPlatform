using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Vehicles.Api.Controllers;

[ApiController]
[Route($"{VehiclesModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }