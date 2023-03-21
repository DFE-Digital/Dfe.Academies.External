using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Dtos;

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
        public string TrustBenefitDetails  { get; set; }
        public string? OfstedInspectionDetails  { get; set; }
        public bool Safeguarding  { get; set; }
        public string? LocalAuthorityReorganisationDetails  { get; set; }
        public string? LocalAuthorityClosurePlanDetails  { get; set; }
        public string? DioceseName  { get; set; }
        public string? DioceseFolderIdentifier  { get; set; }
        public bool PartOfFederation { get; set; }
        public string? FoundationTrustOrBodyName { get; set; }
        public string? FoundationConsentFolderIdentifier { get; set; }
        public DateTimeOffset? ExemptionEndDate { get; set; }
        public string MainFeederSchools { get; set; }
        public string ResolutionConsentFolderIdentifier { get; set; }
        public SchoolEqualitiesProtectedCharacteristics? ProtectedCharacteristics { get; set; }
        public string? FurtherInformation { get; set; }
		public SchoolFinancialYear PreviousFinancialYear { get; set; } = new();
		public SchoolFinancialYear CurrentFinancialYear { get; set; } = new();
		public SchoolFinancialYear NextFinancialYear { get; set; } = new();
		public string SchoolContributionToTrust { get; set; }
        public string GoverningBodyConsentEvidenceDocumentLink { get; set; }
        public bool? AdditionalInformationAdded { get; set; }
        public string AdditionalInformation { get; set; }

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

        public string? ApplicationJoinTrustReason { get; set; }

		//// Pupil Numbers
		public int? ProjectedPupilNumbersYear1 { get; set; }
        public int? ProjectedPupilNumbersYear2 { get; set; }
        public int? ProjectedPupilNumbersYear3 { get; set; }
        public string? SchoolCapacityAssumptions { get; set; }
        public int? SchoolCapacityPublishedAdmissionsNumber { get; set; }

		// Pre-opening support grants
		public PayFundsTo? SchoolSupportGrantFundsPaidTo { get; set; }
		public bool? ConfirmPaySupportGrantToSchool { get; set; }

		// Finances Investigations
		public bool? FinanceOngoingInvestigations { get; set; }
		public string? FinancialInvestigationsExplain { get; set; }
		public bool? FinancialInvestigationsTrustAware { get; set; }

		// consultation details
		public bool? SchoolHasConsultedStakeholders { get; set; }
		public string? SchoolPlanToConsultStakeholders { get; set; }

		// declaration
		public bool? DeclarationBodyAgree { get; set; }
		public bool? DeclarationIAmTheChairOrHeadteacher { get; set; }
		public string? DeclarationSignedByName { get; set; }
	
		public List<SchoolLoan> Loans { get; set; } = new();

        public List<SchoolLease> Leases { get; set; } = new();

		public bool? HasLoans { get; set; }
		public bool? HasLeases { get; set; }
		public Guid EntityId { get; set; }
}
