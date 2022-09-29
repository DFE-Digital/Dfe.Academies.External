namespace Dfe.Academies.External.Web.ViewModels
{
	public class DeclarationSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Details";
		public const string HeadingDetails = "Q";

		public DeclarationSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<DeclarationSummarySectionViewModel> Sections { get; set; }
	}
}
