namespace Dfe.Academies.External.Web.ViewModels
{
	public class LeaseViewModel
	{
		public int Id { get; set; }
		public string LeaseTerm { get; set; }
		public decimal RepaymentAmount { get; set; }
		public decimal InterestRate { get; set; }
		public decimal PaymentsToDate { get; set; }
		public string Purpose { get; set; }
		public string ValueOfAssets { get; set; }
		public string ResponsibleForAssets { get; set; }

	}
}
