namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

public sealed class ApplicationNewTrustGovernanceHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Details";

	public ApplicationNewTrustGovernanceHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<ApplicationNewTrustGovernanceSectionViewModel> Sections { get; set; }
}

