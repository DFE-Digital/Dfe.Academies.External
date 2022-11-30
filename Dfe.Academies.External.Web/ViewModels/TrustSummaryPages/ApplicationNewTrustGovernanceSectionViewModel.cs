namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

public sealed class ApplicationNewTrustGovernanceSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string StructureDocument = "Upload an A4 diagram of the proposed governance structure of the trust";

	public ApplicationNewTrustGovernanceSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
