namespace Dfe.Academies.External.Web.ViewModels
{
	public class LoanViewModel
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public decimal TotalAmount { get; set; }
		public string Purpose { get; set; }
		public string Provider { get; set; }
		public decimal InterestRate { get; set; }
		public string RepaymentSchedule { get; set; }

	}
}
