using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Models.Notifications;
using Notify.Client;
//// using Notify.Models;
using Notify.Models.Responses;

namespace Dfe.Academies.External.Web.Services;

public class EmailNotificationService : IEmailNotificationService
{
	private readonly NotificationClient _notificationClient;
	private readonly ILogger<BespokeExceptionHandlingMiddleware> _logger;

	public EmailNotificationService(IConfiguration configuration,
		ILogger<BespokeExceptionHandlingMiddleware> logger)
	{
		// grab api key from "emailnotifications":"key"
		var apiKey = configuration["emailnotifications:key"];

		_notificationClient = new NotificationClient(apiKey);
		_logger = logger;

		// MR:- alternative client spin up using HttpClient
		// TODO:- amend startupextensions to create new client ???
		//var httpClientWithProxy = new HttpClientWrapper(new HttpClient(...));
		//var client = new NotificationClient(httpClientWithProxy, apiKey);
	}

	public Task SendAsync(MessageDto message)
	{
		// TODO :-
		EmailNotificationResponse response = _notificationClient.SendEmail(message.EmailAddress, 
			message.TemplateId, message.Personalisation, 
			message.Reference, message.EmailReplyToId);

		// TODO:- log response?

		return Task.CompletedTask;
    }

	// TODO MR:- wham template Id's somewhere e.g. constants here ??
	// Template ID - template ID:858e5bea-9d49-442e-a89e-aaed2fb4ade6 // 'Invitation to contribute - chair'
	// Template ID - template ID:03a0ae16-27fe-425d-8aa7-cac43d79f040 // 'Invitation to contribute - someone else'
}
