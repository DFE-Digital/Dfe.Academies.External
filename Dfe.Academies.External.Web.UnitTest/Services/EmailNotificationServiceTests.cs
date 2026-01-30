using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Models.Notifications;
using Dfe.Academies.External.Web.Services; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Notify.Interfaces;
using Notify.Models.Responses;
using NUnit.Framework; 

namespace Dfe.Academies.External.Web.UnitTest.Services;

public class EmailNotificationServiceTests
{
	private static Dictionary<string, object> GetPersonalisedMessage(string email, string reference = "A2B_123")
	{
		return new Dictionary<string, object>
		{
			{ "what_do_you_need_help_with", "Help summary" },
			{ "help_email_address", email},
			{ "app_ref", reference }
		};
	}
	[Test]
	public async Task SendAsync_WhenTestModeTrue_DoesNotCallNotificationClientOrLog()
	{
		// arrange
		var configurationMock = new Mock<IConfiguration>();
		configurationMock.Setup(c => c["emailnotifications:testmode"]).Returns("true");
		configurationMock.Setup(c => c["emailnotifications:key"]).Returns("dummy");
		string email = "test@example.com";
		string templateId = "template-id";
		var personalisedMessage = GetPersonalisedMessage(email);

		var notificationClientMock = new Mock<IAsyncNotificationClient>(MockBehavior.Strict);
		var loggerMock = new Mock<ILogger<BespokeExceptionHandlingMiddleware>>(MockBehavior.Strict);

		var svc = new EmailNotificationService(configurationMock.Object, notificationClientMock.Object, loggerMock.Object);

		var message = new MessageDto(email, templateId);

		// act
		await svc.SendAsync(message);

		// assert - notification client should never be called
		notificationClientMock.Verify(
			x => x.SendEmailAsync(email, templateId, personalisedMessage, null, null, null),
			Times.Never);

		// assert - logger should never be invoked
		loggerMock.Verify(
			x => x.Log(
				It.IsAny<LogLevel>(),
				It.IsAny<EventId>(),
				It.IsAny<It.IsAnyType>(),
				It.IsAny<Exception>(),
				(It.IsAny<Func<It.IsAnyType, Exception, string>>()!)
			),
			Times.Never);
	}

	[Test]
	public async Task SendAsync_WhenTestModeFalse_CallsNotificationClientAndLogsInformation()
	{
		// arrange
		var configurationMock = new Mock<IConfiguration>();
		configurationMock.Setup(c => c["emailnotifications:testmode"]).Returns("false");
		configurationMock.Setup(c => c["emailnotifications:key"]).Returns("dummy");

		string email = "test@example.com";
		string templateId = "template-id";
		string reference = "A2B_123";
		var personalisedMessage = GetPersonalisedMessage(email, reference);
		var message = new MessageDto(email, templateId)
		{
			Personalisation = personalisedMessage,
			Reference = reference,
			EmailReplyToId = "reply-id"
		};

		var notificationClientMock = new Mock<IAsyncNotificationClient>();
		notificationClientMock
			.Setup(x => x.SendEmailAsync(message.EmailAddress, message.TemplateId, message.Personalisation, message.Reference, message.EmailReplyToId, null))
			.ReturnsAsync(new EmailNotificationResponse());

		var loggerMock = new Mock<ILogger<BespokeExceptionHandlingMiddleware>>();

		var svc = new EmailNotificationService(configurationMock.Object, notificationClientMock.Object, loggerMock.Object); 

		// act
		await svc.SendAsync(message);

		// assert - notification client should be called once with the provided message values
		notificationClientMock.Verify(
			x => x.SendEmailAsync(message.EmailAddress, message.TemplateId, message.Personalisation, message.Reference, message.EmailReplyToId, null),
			Times.Once); 
	}
}
