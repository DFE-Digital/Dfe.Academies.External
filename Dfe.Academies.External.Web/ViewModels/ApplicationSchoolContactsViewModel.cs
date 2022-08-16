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
	public string ContactHeadName { get; set; }
	public string ContactHeadEmail { get; set; }
	public string ContactHeadTel { get; set; }
	public string ContactChairName { get; set; }
	public string ContactChairEmail { get; set; }
	public string ContactChairTel { get; set; }

	// "headteacher", "chair of governing body", "someone else"
	public string ContactRole { get; set; }
	public string MainContactOtherName { get; set; }
	public string MainContactOtherEmail { get; set; }
	public string MainContactOtherTelephone { get; set; }
	public string MainContactOtherRole { get; set; }
	public string ApproverContactName { get; set; }
	public string ApproverContactEmail { get; set; }
}
