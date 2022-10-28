namespace Dfe.Academies.External.Web.Services;

public interface IContributorEmailSenderService
{
	Task InvitationToContributorChair(string contributorEmailAddress, string contributorName, string schoolName, 
		string invitingUserName);

	Task InvitationToContributorNonChair(string contributorEmailAddress, string contributorName, string schoolName,
		string invitingUserName);
}
