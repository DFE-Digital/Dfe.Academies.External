namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

public sealed class ApplicationNewTrustImprovementStrategyHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Details";

	public ApplicationNewTrustImprovementStrategyHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<ApplicationNewTrustImprovementStrategySectionViewModel> Sections { get; set; }
}
