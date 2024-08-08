namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationPreOpeningSupportGrantSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string FundsSchoolOrTrust = "Do you want the grant to be paid to the school or the trust?";
	public const string BankAccountDetails = "Bank account details provided?";
	public const string WillYouJoinTheTrustInAGroup = "Will you join the trust in a group of 3 or more?";

	// form a mat
	public const string FundsSchoolOrTrustFormAMat = "Do you want these funds paid to the school or the trust the school is joining?";

	public ApplicationPreOpeningSupportGrantSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
