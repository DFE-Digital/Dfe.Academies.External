namespace Dfe.Academies.External.Web.ViewModels
{
	public class FinancesReviewHeadingViewModel: SchoolQuestionAndAnswerSectionViewModel

	{
		public const string Heading = "Finances";
		public const string HeadingPreviousFinancialYear = "Previous financial year";
		public const string HeadingCurrentFinancialYear = "Current financial year";
		public const string HeadingNextFinancialYear = "Next financial year";
		public const string HeadingLoans = "Loans";
		public const string HeadingLeases = "Financial Leases";
		public const string HeadingFinancialInvestigations = "Financial investigations";

		public FinancesReviewHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<FinancesReviewSectionViewModel> Sections { get; set; }
	}
}
