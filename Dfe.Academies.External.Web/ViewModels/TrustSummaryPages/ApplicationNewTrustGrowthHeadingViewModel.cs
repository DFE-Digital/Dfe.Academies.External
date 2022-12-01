namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public class ApplicationNewTrustGrowthHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Details";

		public ApplicationNewTrustGrowthHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<ApplicationNewTrustGrowthSectionViewModel> Sections { get; set; }
	}
}
