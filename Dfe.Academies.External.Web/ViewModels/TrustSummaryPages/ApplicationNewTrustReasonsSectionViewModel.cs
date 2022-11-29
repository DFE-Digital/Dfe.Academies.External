namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public class ApplicationNewTrustReasonsSectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string OpeningDate = "When do the schools plan to establish the new multi-academy trust?";
		public const string ApproverFullname = "Approver full name";
		public const string ApproverEmail = "Approver email address";

		public ApplicationNewTrustReasonsSectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
