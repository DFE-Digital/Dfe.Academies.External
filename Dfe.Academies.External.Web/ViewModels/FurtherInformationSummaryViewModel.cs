namespace Dfe.Academies.External.Web.ViewModels;

public class FurtherInformationSummaryViewModel : SchoolQuestionAndAnswerSectionViewModel
{
	public const string Heading = "Further information";
	public const string AdditionalDetailsHeading = "Additional details";
	
	
	public FurtherInformationSummaryViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}
	
	public List<FurtherInformationSectionViewModel> Sections { get; set; }
}
