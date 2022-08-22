namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolLandAndBuildingsSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Land and buildings";

	public SchoolLandAndBuildingsSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<SchoolLandAndBuildingsSummarySectionViewModel> Sections { get; set; }
}