namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public class TrustComponentViewModel
	{
		public TrustComponentViewModel()
		{
			TrustComponents = new ();
		}

		public int ApplicationId { get; set; }
		
		public List<ApplicationComponentViewModel> TrustComponents { get; set; }
	}
}
