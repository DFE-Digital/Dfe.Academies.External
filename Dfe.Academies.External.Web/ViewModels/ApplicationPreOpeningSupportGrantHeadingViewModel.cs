namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationPreOpeningSupportGrantHeadingViewModel(string title, string uRi) : SchoolQuestionAndAnswerSectionViewModel(title, uRi)
{
	public const string Heading = "Conversion support grant";

	public List<ApplicationPreOpeningSupportGrantSectionViewModel> Sections { get; set; } = new();
}
