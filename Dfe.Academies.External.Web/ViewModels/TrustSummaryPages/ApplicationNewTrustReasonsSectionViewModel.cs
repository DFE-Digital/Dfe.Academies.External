namespace Dfe.Academies.External.Web.ViewModels.TrustSummaryPages;

public sealed class ApplicationNewTrustReasonsSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string WhyForming = "Why are the schools forming the trust";
	public const string Vision = "What vision and aspiration have the schools agreed for the trust?";
	public const string GeoAreas = "What geographical areas and communities will the trust service?";
	public const string Freedom = "How will the schools make the most of the freedoms that academies have?";
	public const string ImproveTeaching = "How will the schools work together to improve teaching and learning in the trust and potentially support other schools beyond the trust?";

	public ApplicationNewTrustReasonsSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
