namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationPreOpeningSupportGrantSectionViewModel(string name, string answer) : SchoolQuestionAndAnswerViewModel(name, answer)
{
	public const string FundsSchoolOrTrust = "Do you want these funds paid to the school or the trust the school is joining?";
}
