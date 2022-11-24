namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public sealed class ApplicationNewTrustOpeningDateHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Details";

		public ApplicationNewTrustOpeningDateHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<ApplicationNewTrustOpeningDateSectionViewModel> Sections { get; set; }
	}
}
