using AutoMapper;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Factories;
using Dfe.Academies.External.Web.Models.Notifications;
using Notify.Interfaces;

namespace Dfe.Academies.External.Web.Services;

public sealed class ContributorEmailSenderService :  IContributorEmailSenderService
{
	private readonly IEmailNotificationService _emailNotificationService;
	private readonly IContributorNotifyTemplateFactory _contributorNotifyTemplateFactory;
	private readonly IMapper _mapper;
	public ContributorEmailSenderService(
		IEmailNotificationService emailNotificationService,
		IContributorNotifyTemplateFactory contributorNotifyTemplateFactory,
		IMapper mapper)
	{
		_contributorNotifyTemplateFactory = contributorNotifyTemplateFactory;
		_mapper = mapper;
		_emailNotificationService = emailNotificationService;
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

		var message = new MessageDto(contributorEmailAddress, template.TemplateId) { Personalisation = personalisation };
		await _emailNotificationService.SendAsync(message);
	}
}
