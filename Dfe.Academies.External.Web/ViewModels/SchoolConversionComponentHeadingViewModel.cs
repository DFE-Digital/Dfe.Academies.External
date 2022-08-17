namespace Dfe.Academies.External.Web.ViewModels;

public sealed class SchoolConversionComponentHeadingViewModel: SchoolQuestionAndAnswerSectionViewModel
{
	public const string HeadingApplicationSchool = "The school joining the trust";
	public const string HeadingApplicationContactDetails = "Contact details";
	public const string HeadingApplicationPreferredDateForConversion = "Date for conversion";
	public const string HeadingApplicationJoinTrustReason = "Reasons for joining";
	public const string HeadingApplicationSchoolNameChange = "Changing the name of the school";

	public SchoolConversionComponentHeadingViewModel(string title, string uRi) : base(title, uRi)
	{
		Sections = new();
	}

	public List<SchoolConversionComponentSectionViewModel> Sections { get; set; }
}