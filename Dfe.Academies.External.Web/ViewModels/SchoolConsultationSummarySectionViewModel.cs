namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolConsultationSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string HasTheGoverningBodyConsulted = "What is the school's published admissions number (PAN)?";
	public const string WhenDoesTheGoverningBodyPlanToConsult = "What is the school's published admissions number (PAN)?";

	public SchoolConsultationSummarySectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}