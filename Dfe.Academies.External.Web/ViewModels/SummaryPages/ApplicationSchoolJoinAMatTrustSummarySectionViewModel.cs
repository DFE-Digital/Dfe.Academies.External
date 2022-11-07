namespace Dfe.Academies.External.Web.ViewModels.SummaryPages
{
	public sealed class ApplicationSchoolJoinAMatTrustSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string NameOfTheTrust = "The name of the trust";
		public const string TrustConsentEvidenceDoc = "Upload evidence that the trust consents to the school joining";
		public const string ChangesToTrustGovernance = "Will there be any changes to the governance of the trust due to the school joining?";
		public const string ChangesToLaGovernance = "Will there be any changes at a local level due to this school joining?";

		//ChangesToLaGovernance

		public ApplicationSchoolJoinAMatTrustSummarySectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
