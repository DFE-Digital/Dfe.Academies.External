namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationPreOpeningSupportGrantHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Pre-opening support grant";

	public ApplicationPreOpeningSupportGrantHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<ApplicationPreOpeningSupportGrantSectionViewModel> Sections { get; set; }
}
