using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Notifications.Api.Controllers;

[ApiController]
[Route($"{NotificationsModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }