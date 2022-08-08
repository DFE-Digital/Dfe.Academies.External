using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class SchoolConversionComponentHeadingViewModel
{
	public SchoolConversionComponentHeadingViewModel()
	{
		Sections = new();
	}

	// Question = "The name of the school", // MR:- this will change dependent on whether 'formamat' / 'joinamat'
	// Answer = ((string) ViewData[FieldConstants.SchoolName]).DisplayNoInfoIfNullOrEmpty(),
	// FieldName = FieldConstants.SchoolName, // TODO MR:- sort out some constants. Drop in here !

	// Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionMainContact.ChangeReference}"];

	/// <summary>
	/// MR:- this is overall status i.e. if headteacher && chair && main contact not filled in !!
	/// </summary>
	public SchoolConversionComponentStatus Status { get; set; }

	public List<SchoolConversionComponentSectionViewModel> Sections { get; set; }
}