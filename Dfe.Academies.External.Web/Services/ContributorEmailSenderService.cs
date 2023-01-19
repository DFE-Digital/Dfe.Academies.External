using AutoMapper;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Factories;
using Notify.Interfaces;

namespace Dfe.Academies.External.Web.Services;

public sealed class ContributorEmailSenderService :  IContributorEmailSenderService
{
	private readonly IAsyncNotificationClient _notificationClient;
	private readonly IContributorNotifyTemplateFactory _contributorNotifyTemplateFactory;
	private readonly IMapper _mapper;
	public ContributorEmailSenderService(
		IAsyncNotificationClient notificationClient,
		IContributorNotifyTemplateFactory contributorNotifyTemplateFactory,
		IMapper mapper)
	{
		_contributorNotifyTemplateFactory = contributorNotifyTemplateFactory;
		_mapper = mapper;
		_notificationClient = notificationClient;
	}

	///<inheritdoc/>
	public async Task SendInvitationToContributor(
		ApplicationTypes applicationType,
		SchoolRoles contributorRole, 
		string contributorEmailAddress,
		EmailVariablesDto emailVariables)
	{
		var template = _contributorNotifyTemplateFactory.Get(applicationType, contributorRole);
		var personalisation = _mapper.Map<Dictionary<string, dynamic>>(emailVariables);
		await _notificationClient.SendEmailAsync(contributorEmailAddress, template.TemplateId, personalisation);
	}
}
