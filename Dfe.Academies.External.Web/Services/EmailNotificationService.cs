using Dfe.Academies.External.Web.Models.Notifications;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;

namespace Dfe.Academies.External.Web.Services;

public class EmailNotificationService : IEmailNotificationService
{
	public EmailNotificationService()
	{
		// TODO:- grab api key from "emailnotifications":"key"
		//var client = new NotificationClient(apiKey);

		var httpClientWithProxy = new HttpClientWrapper(new HttpClient(...));
		var client = new NotificationClient(httpClientWithProxy, apiKey);
	}

    public Task SendAsync(MessageDto message)
    {
		// TODO MR:-
		//EmailNotificationResponse response = client.SendEmail(emailAddress, templateId, personalisation, reference, emailReplyToId);
		return Task.CompletedTask;
    }

	// TODO MR:- wham template Id's somewhere e.g. constants here ??
	// Template ID - template ID:858e5bea-9d49-442e-a89e-aaed2fb4ade6 // 'Invitation to contribute - chair'
	// Template ID - template ID:03a0ae16-27fe-425d-8aa7-cac43d79f040 // 'Invitation to contribute - someone else'
}
