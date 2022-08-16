using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationSchoolContactsViewModel
{
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

	[Required(ErrorMessage = "You must provide details")]
	public string ContactHeadEmail { get; set; }

	[Required(ErrorMessage = "You must provide a number")]
	public string ContactHeadTel { get; set; }

	[Required(ErrorMessage = "You must provide details")]
	public string ContactChairName { get; set; }

	[Required(ErrorMessage = "You must provide details")]
	public string ContactChairEmail { get; set; }

	[Required(ErrorMessage = "You must provide a number")]
	public string ContactChairTel { get; set; }

	[RequiredEnum(ErrorMessage = "You must provide a main contact")]
	public MainConversionContact ContactRole { get; set; }

	[Required(ErrorMessage = "You must provide details")]
	public string MainContactOtherName { get; set; }

	[Required(ErrorMessage = "You must provide details")]
	public string MainContactOtherEmail { get; set; }

	[Required(ErrorMessage = "You must provide a number")]
	public string MainContactOtherTelephone { get; set; }

	/// <summary>
	/// only applicable if ContactRole = other
	/// </summary>
	public string? MainContactOtherRole { get; set; }

	// TODO MR:- no validation on ticket?

	public string ApproverContactName { get; set; }
	public string ApproverContactEmail { get; set; }
}
