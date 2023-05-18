using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Booking.Api.Controllers;

[ApiController]
[Route($"{BookingModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }