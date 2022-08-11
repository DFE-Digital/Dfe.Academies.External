namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolPupilNumbersSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Future pupil numbers";

	public SchoolPupilNumbersSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<SchoolPupilNumbersSummarySectionViewModel> Sections { get; set; }
}