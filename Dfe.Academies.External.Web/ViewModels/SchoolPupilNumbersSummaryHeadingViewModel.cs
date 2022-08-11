namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolPupilNumbersSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Changing the name of the school";

	public SchoolPupilNumbersSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<SchoolPupilNumbersSummarySectionViewModel> Sections { get; set; }
}