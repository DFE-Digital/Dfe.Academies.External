namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationPreOpeningSupportGrantSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string FundsSchoolOrTrust = "Do you want these funds paid to the school or the trust the school is joining?";

	public ApplicationPreOpeningSupportGrantSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
