using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Models.Notifications;

namespace Dfe.Academies.External.Web.Services;

public sealed class ContributorEmailSenderService : BaseService, IContributorEmailSenderService 
{
	private readonly HttpClient _httpClient;
	private readonly EmailNotificationService _emailNotificationService;

	private const string InvitationToContributeChairTemplateId = "858e5bea-9d49-442e-a89e-aaed2fb4ade6"; // template id from gov uk notify !
	private const string InvitationToContributeNonChairTemplateId = "03a0ae16-27fe-425d-8aa7-cac43d79f040"; // template id from gov uk notify !

	public ContributorEmailSenderService(IHttpClientFactory httpClientFactory, ILogger<BespokeExceptionHandlingMiddleware> logger,
		IConfiguration configuration) 
		: base(httpClientFactory)
	{
		// TODO:- amend startupextensions to create new client ???
		_httpClient = httpClientFactory.CreateClient(AcademiesAPIHttpClientName);
		_emailNotificationService = new EmailNotificationService(configuration, logger);
	}

	///<inheritdoc/>
	public async Task InvitationToContributorChair(string invitationEmailRecipient, string invitationName, string schoolName,
		string invitingContributorName)
	{
		Dictionary<string, dynamic> personalisation = new Dictionary<string, dynamic>
		{
			{ "invitee_name", invitationName },
			{ "school", schoolName },
			{ "inviting_contributor", invitingContributorName },
		};

		MessageDto emailMessage = new MessageDto(invitationEmailRecipient, InvitationToContributeChairTemplateId)
		{
			Personalisation = personalisation
		};

		await _emailNotificationService.SendAsync(emailMessage);
	}

	///<inheritdoc/>
	public async Task InvitationToContributorNonChair(string invitationEmailRecipient, string invitationName, string schoolName,
		string invitingContributorName)
	{
		Dictionary<string, dynamic> personalisation = new Dictionary<string, dynamic>
		{
			{ "invitee_name", invitationName },
			{ "school", schoolName },
			{ "inviting_contributor", invitingContributorName },
		};

		MessageDto emailMessage = new MessageDto(invitationEmailRecipient, InvitationToContributeNonChairTemplateId)
		{
			Personalisation = personalisation
		};

		await _emailNotificationService.SendAsync(emailMessage);
	}
}
