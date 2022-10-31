using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Services;

public interface IContributorEmailSenderService
{
	Task SendInvitationToContributor(ApplicationTypes applicationType, SchoolRoles contributorRole,
		string contributorName, string contributorEmailAddress,
		string schoolName,
		string invitingUserName);
}
