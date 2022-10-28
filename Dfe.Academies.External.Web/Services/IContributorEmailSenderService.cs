namespace Dfe.Academies.External.Web.Services;

public interface IContributorEmailSenderService
{
	Task InvitationToContributorChair(string invitationEmailRecipient, string invitationName, string schoolName,  string invitingContributorName);

	Task InvitationToContributorNonChair(string invitationEmailRecipient, string invitationName, string schoolName,
		string invitingContributorName);
}
