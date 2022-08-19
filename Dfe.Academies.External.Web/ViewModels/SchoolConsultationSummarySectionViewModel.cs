namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolConsultationSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string HasTheGoverningBodyConsulted = "Has the governing body consulted the relevant stakeholders?";
	public const string WhenDoesTheGoverningBodyPlanToConsult = "When does the governing body plan to consult?";

	public SchoolConsultationSummarySectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}