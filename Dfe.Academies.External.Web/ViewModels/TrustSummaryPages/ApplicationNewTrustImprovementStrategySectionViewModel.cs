namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

public sealed class ApplicationNewTrustImprovementStrategySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string Support = "How will the trust support and improve the academies in the trust?";
	public const string Strategy = "How will the trust make sure that the school improvement strategy is fit for purpose and will improve standards?";
	public const string ApprovedSponsor = "If the trust wants to become an approved sponsor, when would it plan to apply and begin sponsoring other schools?";

	public ApplicationNewTrustImprovementStrategySectionViewModel(string title, string uRi) : base(title, uRi)
	{
	}
}
