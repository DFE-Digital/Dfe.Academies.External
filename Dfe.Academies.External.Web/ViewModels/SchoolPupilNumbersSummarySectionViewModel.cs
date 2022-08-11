namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolPupilNumbersSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string PupilNumberYr1 = "Projected pupil numbers on roll in the year the academy opens (year 1)";
	public const string PupilNumberYr2 = "Projected pupil numbers on roll in the following year after the academy has opened (year 2)";
	public const string PupilNumberYr3 = "Projected pupil numbers on roll in the following year (year 3)";
	public const string NumbersBasedUpon = "What do you base these projected numbers on?";
	public const string PAN = "What is the school's published admissions number (PAN)?";

	public SchoolPupilNumbersSummarySectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}