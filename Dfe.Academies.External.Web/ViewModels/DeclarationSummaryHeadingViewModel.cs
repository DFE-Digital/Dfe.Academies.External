namespace Dfe.Academies.External.Web.ViewModels
{
	public class DeclarationSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Finances";
		public const string HeadingDetails = "Details";

		public DeclarationSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<DeclarationSummarySectionViewModel> Sections { get; set; }
	}
}
