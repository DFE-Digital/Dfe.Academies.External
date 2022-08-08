using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class SchoolConversionComponentHeadingViewModel
{
	public const string HeadingApplicationSchool = "The school joining the trust";
	public const string HeadingApplicationContactDetails = "Contact details";
	public const string HeadingApplicationPreferredDateForConversion = "Date for conversion";
	public const string HeadingApplicationJoinTrustReason = "Reasons for joining";
	public const string HeadingApplicationSchoolNameChange = "Changing the name of the school";

	public SchoolConversionComponentHeadingViewModel(string title, string uRi)
	{
		Title = title;
		Sections = new();
		URI = uRi;
	}

	/// <summary>
	/// e.g. Title = "The school joining the trust"
	/// </summary>
	public string Title { get; set; }
	
	// Answer = ((string) ViewData[FieldConstants.SchoolName]).DisplayNoInfoIfNullOrEmpty(),

	//// Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionMainContact.ChangeReference}"];

	/// <summary>
	/// MR:- this is overall status i.e. if headteacher && chair && main contact not filled in !!
	/// </summary>
	public SchoolConversionComponentStatus Status { get; set; }

	// MR:- need a URI similar to ApplicationComponentViewModel - to render 'start section' button
	public string URI { get; set; }

	public List<SchoolConversionComponentSectionViewModel> Sections { get; set; }
}