namespace GarageGenius.Modules.Notifications.Core.Services;
internal interface IEmailSenderService
{
	Task SendEmailAsync(string receiver, string subject, string message);
}
