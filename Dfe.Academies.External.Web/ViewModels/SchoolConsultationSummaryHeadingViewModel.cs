namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolConsultationSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Consultation";

	public SchoolConsultationSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<SchoolConsultationSummarySectionViewModel> Sections { get; set; }
}