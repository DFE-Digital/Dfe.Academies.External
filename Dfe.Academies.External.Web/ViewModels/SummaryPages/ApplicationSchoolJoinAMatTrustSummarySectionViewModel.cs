namespace Dfe.Academies.External.Web.ViewModels.SummaryPages
{
	public sealed class ApplicationSchoolJoinAMatTrustSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string NameOfTheTrust = "The name of the trust";
		public const string TrustConsentEvidenceDoc = "Upload evidence that the trust consents to the school joining";

		//ChangesToLaGovernance

		public ApplicationSchoolJoinAMatTrustSummarySectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
