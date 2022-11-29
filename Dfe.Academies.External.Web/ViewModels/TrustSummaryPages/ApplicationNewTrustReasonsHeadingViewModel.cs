namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public class ApplicationNewTrustReasonsHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Details";

		public ApplicationNewTrustReasonsHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<ApplicationNewTrustReasonsSectionViewModel> Sections { get; set; }
	}
}
