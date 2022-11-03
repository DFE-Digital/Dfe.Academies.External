namespace Dfe.Academies.External.Web.ViewModels;

public class FurtherInformationSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string SchoolTrustBenefit = "What will the school bring to the trust they are joining?";
	public const string OfstedInspection = "Have Ofsted recently inspected the school but not published the report yet?";
	public const string SafeguardingInvestigations = "Are there any safeguarding investigations ongoing at the school?";
	public const string LocalAuthorityReorganisation = "Is the school part of a local authority reorganisation?";
	public const string LocalAuthorityClosurePlans = "Is the school part of any local authority closure plans?";
	public const string Diocese = "Is your school linked to a diocese?";
	public const string Federation = "Is the school part of a federation?";
	public const string FoundationTrustOrBody = "Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?";
	public const string ExemptionSACRE = "Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?";
	public const string MainFeederSchools = "Provide a list of your main feeder schools";
	public const string Resolution = "The school's Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school's consent to converting and joining the trust.";
	public const string EqualitiesImpactAssessment = "Has an equalities impact assessment been carried out and considered by the governing body?";
	public const string FurtherInformation = "Do you want to add any further information?";
	
	public FurtherInformationSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
