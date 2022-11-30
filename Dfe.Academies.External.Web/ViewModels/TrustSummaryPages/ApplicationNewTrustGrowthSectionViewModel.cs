namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public class ApplicationNewTrustGrowthSectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string Growth = "Do you plan to grow the trust over the next 5 years?";

		public ApplicationNewTrustGrowthSectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
