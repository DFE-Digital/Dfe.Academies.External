namespace Dfe.Academies.External.Web.Models;

public class SchoolApplyingToConvert
{
	public SchoolApplyingToConvert(string schoolName)
	{
		SchoolName = schoolName;
	}

    public int Id { get; set; }

    /// <summary>
    /// This would be existing Id from GIAS (?). 6 digit URN?
    /// </summary>
    public int SchoolId { get; set; }

    public string SchoolName { get; set; }

    public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; } = new();

    // TODO MR:- contact head - or list<Contact> with a type?

    // TODO MR:- contact chair - or list<Contact> with a type?

    //// MR:- below props from A2C-SIP - ApplyingSchool object
    public bool? SchoolOfstedInspectedButReportNotPublished { get; set; }

    public string? SchoolOfstedInspectedReportNotPublishedExplain { get; set; }

    public bool? SchoolLocalAuthorityReorganisation { get; set; }

    public string? SchoolLocalAuthorityReorganisationExplain { get; set; }

    public bool? SchoolLocalAuthorityClosurePlans { get; set; }

    public string? SchoolLocalAuthorityClosurePlansExplain { get; set; }

    public bool? SchoolAdSafeguarding { get; set; }

    public string? SchoolAdSafeguardingExplain { get; set; }

    // TODO:- other props from A2C-SIP - ApplyingSchool object

    public List<SchoolLoan> SchoolLoans { get; set; } = new();

    public List<SchoolLease> SchoolLeases { get; set; } = new();

    public List<SchoolContact> SchoolContacts { get; set; } = new();

    public SchoolFinances SchoolFinances { get; set; } = new();

    public SchoolPupils SchoolPupils { get; set; } = new();

    public SchoolLandAndBuildings SchoolLandAndBuildings { get; set; } = new();
}
