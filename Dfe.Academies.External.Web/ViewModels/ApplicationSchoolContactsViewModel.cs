using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationSchoolContactsViewModel
{
	/// <summary>
	/// This is needed for model binding
	/// </summary>
	public ApplicationSchoolContactsViewModel()
	{

	}

	public ApplicationSchoolContactsViewModel(int applicationId, int urn)
	{
		ApplicationId = applicationId;
		Urn = urn;
	}

	public int ApplicationId { get; set; }
	public int Urn { get; set; }

	// contact details

	[Required(ErrorMessage = "You must provide details")]
	public string ContactHeadName { get; set; }

	[EmailAddress]
	[Required(ErrorMessage = "You must provide details")]
	public string ContactHeadEmail { get; set; }

	[Phone] // TODO MR:- what kind of phone validation?
	[Required(ErrorMessage = "You must provide a number")]
	public string ContactHeadTel { get; set; }

	[Required(ErrorMessage = "You must provide details")]
	public string ContactChairName { get; set; }

	[EmailAddress]
	[Required(ErrorMessage = "You must provide details")]
	public string ContactChairEmail { get; set; }

	[Required(ErrorMessage = "You must provide a number")]
	public string ContactChairTel { get; set; }

	[RequiredEnum(ErrorMessage = "You must provide a main contact")]
	public MainConversionContact ContactRole { get; set; }

	public string? MainContactOtherName { get; set; }

	public string? MainContactOtherEmail { get; set; }

	public string? MainContactOtherTelephone { get; set; }

	/// <summary>
	/// only applicable if ContactRole = other - no idea what this is
	/// </summary>
	public string? MainContactOtherRole { get; set; }

	// TODO MR:- no validation on ticket for below?

	public string? ApproverContactName { get; set; }
	public string? ApproverContactEmail { get; set; }
}
