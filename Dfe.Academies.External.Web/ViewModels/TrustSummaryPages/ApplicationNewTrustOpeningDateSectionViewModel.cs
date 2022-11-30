namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages
{
	public sealed class ApplicationNewTrustOpeningDateSectionViewModel : SchoolQuestionAndAnswerViewModel
	{
		public const string OpeningDate = "When do the schools plan to establish the new multi-academy trust?";
		public const string ApproverFullname = "Approver's full name";
		public const string ApproverEmail = "Approver's email address";

		public ApplicationNewTrustOpeningDateSectionViewModel(string name, string answer) : base(name, answer)
		{
		}
	}
}
