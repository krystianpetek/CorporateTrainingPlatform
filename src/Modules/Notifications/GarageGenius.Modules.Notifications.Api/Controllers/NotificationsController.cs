using System.Security.Claims;
using GarageGenius.Modules.Notifications.Application.Queries;
using GarageGenius.Modules.Notifications.Application.Queries.GetUserNotifications;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Notifications.Api.Controllers;

public class NotificationsController : BaseController
{
	private readonly IDispatcher _dispatcher;

	public NotificationsController(IDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
	}

	[Authorize]
	[HttpGet("notifications")]
	[SwaggerOperation("Get logged user notifications")]
	[SwaggerResponse(StatusCodes.Status200OK, "User notifications found", typeof(GetUserNotificationsQuery))]
	public async Task<ActionResult> GetNotificationsForUserAsync(CancellationToken cancellationToken)
	{
		var identifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var userId = Guid.Parse(identifier);
		var notifications = await _dispatcher.DispatchQueryAsync(new GetUserNotificationsQuery(userId), cancellationToken);

		return Ok(notifications);
	}
}
// TODO - handling for timeout / cancel request
