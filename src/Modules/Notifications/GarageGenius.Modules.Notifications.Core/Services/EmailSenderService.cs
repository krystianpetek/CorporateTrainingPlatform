namespace GarageGenius.Modules.Notifications.Core.Services;
internal class EmailSenderService : IEmailSenderService
{
	private readonly Serilog.ILogger _logger;

	public EmailSenderService(
		Serilog.ILogger logger)
	{
		_logger = logger;
	}

	public Task SendEmailAsync(string receiver, string subject, string message)
	{
		// TODO: create sending email
		_logger.Information(
			messageTemplate: "Email sent to {Receiver} with subject {Subject} and message {Message}",
			receiver,
			subject,
			message);

		return Task.CompletedTask;
	}
}
