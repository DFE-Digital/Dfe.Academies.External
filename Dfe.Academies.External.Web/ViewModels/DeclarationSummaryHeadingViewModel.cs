namespace Dfe.Academies.External.Web.ViewModels
{
	public class DeclarationSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "Details";
		public const string HeadingDetails = "I agree with all of these statements, and believe that the facts stated in this application are true";

		public DeclarationSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<DeclarationSummarySectionViewModel> Sections { get; set; }
	}
}
