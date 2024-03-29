﻿namespace Dfe.Academies.External.Web.ViewModels
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
		public const string CFYEndDate = "End of current financial year end date?";
		public const string CFYRevenue = "Revenue carry forward at end of the current financial year (31 March)";
		public const string CFYRevenueStatusExplained = ""; // ??
		public const string CFYCapitalCarryForward = "Capital carry forward at end of the current financial year (31 March)";
		public const string CFYCapitalCarryForwardExplained = ""; // ??

		// next financial year
		public const string NFYEndDate = "End of next financial year end date?";
		public const string NFYRevenue = "Revenue carry forward at end of the next financial year (31 March)";
		public const string NFYRevenueStatusExplained = ""; // ??
		public const string NFYCapitalCarryForward = "Capital carry forward at end of the next financial year (31 March)";
		public const string NFYCapitalCarryForwardExplained = ""; // ??

		// loans
		public const string ExistingLoans = "Are there any existing loans?";

		// leases
		public const string ExistingLeases = "Are there any existing leases?";

		// financial investigations
		public const string FinanceOngoingInvestigations = "Finance ongoing investigations?";
		public const string FinancialInvestigationsExplain = "Explained";
		public const string FinancialInvestigationsTrustAware = "Trust aware?";

		public FinancesReviewSectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
