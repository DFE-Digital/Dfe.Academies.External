using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Services;

public interface IContributorEmailSenderService
{
	Task SendInvitationToContributor(
		ApplicationTypes applicationType,
		SchoolRoles contributorRole,
		string contributorEmailAddress,
		EmailVariablesDto emailVariables);
}
