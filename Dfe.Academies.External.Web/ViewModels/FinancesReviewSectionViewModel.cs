namespace Dfe.Academies.External.Web.ViewModels
{
	public class FinancesReviewSectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string Status = "Surplus or Deficit?";

		// previous financial year
		public const string PFYEndDate = "End of previous financial year end date?";
		public const string PFYRevenue = "Revenue carry forward at end of the previous financial year (31 March)";
		public const string PFYRevenueStatusExplained = ""; // ??
		public const string PFYCapitalCarryForward = "Capital carry forward at end of the previous financial year (31 March)";
		public const string PFYCapitalCarryForwardExplained = ""; // ??

		// current financial year
		// TODO MR:- question string defs

		// next financial year
		// TODO MR:- question string defs

		// loans
		// TODO MR:- question string defs

		// leases
		// TODO MR:- question string defs

		// financial investigations
		// TODO MR:- question string defs

		public FinancesReviewSectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
