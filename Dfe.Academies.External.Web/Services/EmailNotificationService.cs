using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Models.Notifications;
using Notify.Client;
using Notify.Models.Responses;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// see https://docs.notifications.service.gov.uk/net.html for further information
/// </summary>
public class EmailNotificationService : IEmailNotificationService
{
	private readonly NotificationClient _notificationClient;
	private readonly ILogger<BespokeExceptionHandlingMiddleware> _logger;

	public EmailNotificationService(IConfiguration configuration,
		ILogger<BespokeExceptionHandlingMiddleware> logger)
	{
		// grab api key from "emailnotifications":"key"
		string apiKey = configuration["emailnotifications:key"];

		_notificationClient = new NotificationClient(apiKey);
		_logger = logger;

		// MR:- alternative create client method spin up using HttpClient
		// TODO:- amend startupextensions to create new client - not sure, as no URI specified in https://docs.notifications.service.gov.uk/net.html ???
		//var httpClientWithProxy = new HttpClientWrapper(new HttpClient(...));
		//var client = new NotificationClient(httpClientWithProxy, apiKey);
	}

	public Task SendAsync(MessageDto message)
	{
		EmailNotificationResponse response = _notificationClient.SendEmail(message.EmailAddress, 
			message.TemplateId, message.Personalisation, 
			message.Reference, message.EmailReplyToId);

		// TODO:- handle response - 400 / 429 / 403 / 500
		//switch (response)
		//{
			
		//}

		// TODO:- log response?
		_logger.LogInformation($"Email successfully Sent to:- {message.EmailAddress}");
		
		return Task.CompletedTask;
    }
}
