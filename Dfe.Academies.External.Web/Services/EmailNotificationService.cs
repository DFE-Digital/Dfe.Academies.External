using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Models.Notifications;
using Notify.Client;
using Notify.Interfaces;
using Notify.Models.Responses;

namespace Dfe.Academies.External.Web.Services;

/// <summary>
/// see https://docs.notifications.service.gov.uk/net.html for further information
/// </summary>
public class EmailNotificationService : IEmailNotificationService
{
	private readonly IAsyncNotificationClient _notificationClient;
	private readonly ILogger<BespokeExceptionHandlingMiddleware> _logger;
	private readonly bool TestMode;

	public EmailNotificationService(IConfiguration configuration,
		IAsyncNotificationClient notificationClient,
		ILogger<BespokeExceptionHandlingMiddleware> logger)
	{
		// grab api key from "emailnotifications":"key"
		string apiKey = configuration["emailnotifications:key"];
		this.TestMode = Boolean.Parse(configuration["emailnotifications:testmode"]);

		_notificationClient = notificationClient;
		_logger = logger;

		// MR:- alternative create client method spin up using HttpClient
		// TODO:- amend startupextensions to create new client - not sure, as no URI specified in https://docs.notifications.service.gov.uk/net.html ???
		//var httpClientWithProxy = new HttpClientWrapper(new HttpClient(...));
		//var client = new NotificationClient(httpClientWithProxy, apiKey);
	}

	public async Task SendAsync(MessageDto message)
	{
		if (TestMode) {
			 // Don't send email if test mode is true
			return;
		}

		EmailNotificationResponse response = await _notificationClient.SendEmailAsync(message.EmailAddress, 
			message.TemplateId, message.Personalisation, 
			message.Reference, message.EmailReplyToId);

		_logger.LogInformation($"Email successfully Sent to:- {message.EmailAddress}");	
    }
}
