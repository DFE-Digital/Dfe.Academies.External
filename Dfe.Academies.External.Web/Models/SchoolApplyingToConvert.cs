namespace Dfe.Academies.External.Web.Models;

public class SchoolApplyingToConvert
{
	public SchoolApplyingToConvert(string schoolName, int urn, string? ukprn, string street, string town, string fullUkPostcode)
	{
		SchoolName = schoolName;
		UKPRN = ukprn;
		URN = urn;
        Street = street;
		Town = town;
		FullUkPostcode = fullUkPostcode;
    }

    /// <summary>
    /// This would be existing Id from GIAS (?). 6 digit URN?
    /// </summary>
    public int SchoolId { get; set; }

    /// <summary>
    /// Another unique school Id (6 digit number) e.g. 587634
    /// </summary>
    public int URN { get; set; }

    /// <summary>
    /// Not nullable - GIAS unique school Id ? e.g. GAT00123
    /// </summary>
    public string? UKPRN { get; set; }

    public string SchoolName { get; set; }

    // TODO MR:- should this be an address object ??
    // registered address
    public string Street { get; set; }

    public string? Locality { get; set; }

    public string? Address3 { get; set; }

    public string Town { get; set; }

    public string? CountyDescription { get; set; }

    public string FullUkPostcode { get; set; }

    public List<ConversionApplicationComponent> SchoolApplicationComponents { get; set; } = new();

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
