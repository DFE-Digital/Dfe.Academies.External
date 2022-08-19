namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolConsultationSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string PAN = "What is the school's published admissions number (PAN)?";

	public SchoolConsultationSummarySectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}