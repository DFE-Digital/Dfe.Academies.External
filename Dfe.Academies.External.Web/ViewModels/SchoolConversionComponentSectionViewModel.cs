namespace Dfe.Academies.External.Web.ViewModels;

public sealed class SchoolConversionComponentSectionViewModel
{
	public const string NoInfoAnswer = "You have not added any information";

	public SchoolConversionComponentSectionViewModel(string answer)
	{
		Answer = answer;
	}

	// Question = "Name",
	//// MR:- below sets text to public const string NoInfo = "You have not added any information";
	
	// Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherName]).DisplayNoInfoIfNullOrEmpty(),
	public string Answer { get; set; }

	// FieldName = FieldConstants.SipSchoolConversionMainContactOtherName // MR:- ??

	// TODO MR:- need a URI similar to ApplicationComponentViewModel - to render change link
}