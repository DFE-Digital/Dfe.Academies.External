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
	public int id { get; set; }
        public int URN { get; set; }
        public string SchoolName { get; set; }
        public string UKPRN { get; set; }
        public SchoolLandAndBuildings LandAndBuildings { get; set; }
        public SchoolPerformance Performance { get; set; }
        public LocalAuthority LocalAuthority { get; set; }
        public PartnershipsAndAffliations PartnershipsAndAffliations { get; set; }
        public SchoolReligiousEducation ReligiousEducation { get; set; }
        public SchoolFinancialYear PreviousFinancialYear { get; set; }
        public SchoolFinancialYear CurrentFinancialYear { get; set; }
        public SchoolFinancialYear NextFinancialYear { get; set; }
        public string SchoolContributionToTrust { get; set; }
        public string GoverningBodyConsentEvidenceDocumentLink { get; set; }
        public bool? AdditionalInformationAdded { get; set; }
        public string AdditionalInformation { get; set; }
        public string EqualitiesImpactAssessmentCompleted { get; set; }
        public string EqualitiesImpactAssessmentDetails { get; set; }
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
       
        public string ApplicationJoinTrustReason { get; set; }

        public string SchoolSupportGrantFundsPaidTo { get; set; }
        public bool? ConfirmPaySupportGrantToSchool { get; set; }
        
        /// <summary>
        /// TODO MR:- is below more a VM thing?
        /// </summary>
        public List<ConversionApplicationComponent> SchoolApplicationComponents { get; set; } = new();
        
        //// ApplicationChangeSchoolName
        public bool? ConversionChangeNamePlanned { get; set; }
        public string? ProposedNewSchoolName { get; set; }
        
        //// ApplicationConversionTargetDate
        public bool? SchoolConversionTargetDateSpecified { get; set; }
        public DateTime? SchoolConversionTargetDate { get; set; }
        public string SchoolConversionTargetDateExplained { get; set; }

        //// Pupil Numbers
        public int? ProjectedPupilNumbersYear1 { get; set; }
        public int? ProjectedPupilNumbersYear2 { get; set; }
        public int? ProjectedPupilNumbersYear3 { get; set; }
        public string? SchoolCapacityAssumptions { get; set; }
        public int? SchoolCapacityPublishedAdmissionsNumber { get; set; }
        
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
}
