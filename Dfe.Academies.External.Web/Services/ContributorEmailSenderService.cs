﻿using Dfe.Academies.External.Web.Enums;
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
		// TODO:- amend startupextensions to create new client - not sure, as no URI specified in https://docs.notifications.service.gov.uk/net.html ???
		_httpClient = httpClientFactory.CreateClient(AcademiesAPIHttpClientName);
		_emailNotificationService = new EmailNotificationService(configuration, logger);
	}

	///<inheritdoc/>
	public async Task SendInvitationToContributor(ApplicationTypes applicationType, SchoolRoles contributorRole,
		string contributorName, string contributorEmailAddress, 
		string schoolName,
		string invitingUserName)
	{
		// send email - diff templates for different roles
		// TODO:- && different email dependent on application type? i.e. grab FirstOrDefault() on school for JoinAMat ?
		if (contributorRole == SchoolRoles.ChairOfGovernors)
		{
			await InvitationToContributorChair(contributorName, contributorEmailAddress, schoolName, invitingUserName);
		}
		else
		{
			await InvitationToContributorNonChair(contributorName,contributorEmailAddress, schoolName, invitingUserName);
		}
	}
	
	private async Task InvitationToContributorChair(string contributorName, string contributorEmailAddress,  string schoolName,
		string invitingUserName)
	{
		Dictionary<string, dynamic> personalisation = new Dictionary<string, dynamic>
		{
			{ "invitee_name", contributorName },
			{ "school", schoolName },
			{ "inviting_contributor", invitingUserName },
		};

		MessageDto emailMessage = new MessageDto(contributorEmailAddress, InvitationToContributeChairTemplateId)
		{
			Personalisation = personalisation
		};

		await _emailNotificationService.SendAsync(emailMessage);
	}

	private async Task InvitationToContributorNonChair(string contributorName, string contributorEmailAddress,  string schoolName,
		string invitingUserName)
	{
		Dictionary<string, dynamic> personalisation = new Dictionary<string, dynamic>
		{
			{ "invitee_name", contributorName },
			{ "school", schoolName },
			{ "inviting_contributor", invitingUserName },
		};

		MessageDto emailMessage = new MessageDto(contributorEmailAddress, InvitationToContributeNonChairTemplateId)
		{
			Personalisation = personalisation
		};

		await _emailNotificationService.SendAsync(emailMessage);
	}
}