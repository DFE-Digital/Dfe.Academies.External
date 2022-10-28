namespace Dfe.Academies.External.Web.Services;

public interface IContributorEmailSenderService
{
	Task InvitationToContributeChair(string invitationEmailRecipient, string invitationName, string schoolName,  string invitingContributorName);
	
	
}
