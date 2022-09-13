namespace Dfe.Academies.External.Web.Models;

public class SchoolApplyingToConvert
{
	public SchoolApplyingToConvert(string schoolName, int urn, string? ukprn)
	{
		SchoolName = schoolName;
		UKPRN = ukprn;
		URN = urn;
		LandAndBuildings = new(
								null, null, null,
								null, null, null,
								null, null, null,
								null, null, null);
	}

	public int Id { get; set; }

	public int ApplicationId { get; set; }

	/// <summary>
	/// Unique school Id (6 digit number) e.g. 101934
	/// </summary>
	public int URN { get; set; }

	/// <summary>
	/// GIAS unique school Id ? e.g. GAT00123
	/// </summary>
	public string? UKPRN { get; set; }

	public string SchoolName { get; set; }

	//// School Contacts / Key people

	public string SchoolConversionContactHeadName { get; set; }
	public string SchoolConversionContactHeadEmail { get; set; }
	public string SchoolConversionContactHeadTel { get; set; }
	public string SchoolConversionContactChairName { get; set; }
	public string SchoolConversionContactChairEmail { get; set; }
	public string SchoolConversionContactChairTel { get; set; }
	public string SchoolConversionContactRole { get; set; }
	public string SchoolConversionMainContactOtherName { get; set; }
	public string SchoolConversionMainContactOtherEmail { get; set; }
	public string SchoolConversionMainContactOtherTelephone { get; set; }
	public string SchoolConversionMainContactOtherRole { get; set; }
	public string SchoolConversionApproverContactName { get; set; }
	public string SchoolConversionApproverContactEmail { get; set; }

	//// ApplicationConversionTargetDate
	public bool? SchoolConversionTargetDateSpecified { get; set; }
	public DateTime? SchoolConversionTargetDate { get; set; }
	public string SchoolConversionTargetDateExplained { get; set; }

	//// ApplicationChangeSchoolName
	public bool? ConversionChangeNamePlanned { get; set; }
	public string? ProposedNewSchoolName { get; set; }

	//// ApplicationJoinTrustReasons 
	public string ApplicationJoinTrustReason { get; set; }

	//// Pupil Numbers
	public int? ProjectedPupilNumbersYear1 { get; set; }
	public int? ProjectedPupilNumbersYear2 { get; set; }
	public int? ProjectedPupilNumbersYear3 { get; set; }
	public string? SchoolCapacityAssumptions { get; set; }
	public int? SchoolCapacityPublishedAdmissionsNumber { get; set; }

	/// <summary>
	/// TODO MR:- is below more a VM thing?
	/// </summary>
	public List<ConversionApplicationComponent> SchoolApplicationComponents { get; set; } = new();

	//// MR:- below props from A2C-SIP - ApplyingSchool object
	public bool? SchoolOfstedInspectedButReportNotPublished { get; set; }

	public string? SchoolOfstedInspectedReportNotPublishedExplain { get; set; }

	public bool? SchoolLocalAuthorityReorganisation { get; set; }

	public string? SchoolLocalAuthorityReorganisationExplain { get; set; }

	public bool? SchoolLocalAuthorityClosurePlans { get; set; }

	public string? SchoolLocalAuthorityClosurePlansExplain { get; set; }

	public bool? SchoolAdSafeguarding { get; set; }

	public string? SchoolAdSafeguardingExplain { get; set; }

	public List<SchoolLoan> Loans { get; set; } = new();

	public List<SchoolLease> Leases { get; set; } = new();

	public SchoolFinances Finances { get; set; } = new();

	public SchoolLandAndBuildings LandAndBuildings { get; set; }
}
