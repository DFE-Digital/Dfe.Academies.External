namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts
{
	public class TrustFullDetailsDto
	{
		public Ifddata ifdData { get; set; }
		public Giasdata giasData { get; set; }
		public Establishment[] establishments { get; set; }
	}

	public class Ifddata
	{
		public string trustOpenDate { get; set; }
		public string leadRSCRegion { get; set; }
		public string trustContactPhoneNumber { get; set; }
		public string performanceAndRiskDateOfMeeting { get; set; }
		public string prioritisedAreaOfReview { get; set; }
		public string currentSingleListGrouping { get; set; }
		public string dateOfGroupingDecision { get; set; }
		public string dateEnteredOntoSingleList { get; set; }
		public string trustReviewWriteup { get; set; }
		public string dateOfTrustReviewMeeting { get; set; }
		public string followupLetterSent { get; set; }
		public string dateActionPlannedFor { get; set; }
		public string wipSummaryGoesToMinister { get; set; }
		public string externalGovernanceReviewDate { get; set; }
		public string efficiencyICFPreviewCompleted { get; set; }
		public string efficiencyICFPreviewOther { get; set; }
		public string linkToWorkplaceForEfficiencyICFReview { get; set; }
		public string numberInTrust { get; set; }
		public string trustType { get; set; }
		public Trustaddress trustAddress { get; set; }
	}

	public class Trustaddress
	{
		public string street { get; set; }
		public string locality { get; set; }
		public string additionalLine { get; set; }
		public string town { get; set; }
		public string county { get; set; }
		public string postcode { get; set; }
	}

	public class Giasdata
	{
		public string groupId { get; set; }
		public string groupName { get; set; }
		public string groupType { get; set; }
		public string companiesHouseNumber { get; set; }
		public Groupcontactaddress groupContactAddress { get; set; }
		public string ukprn { get; set; }
	}

	public class Groupcontactaddress
	{
		public string street { get; set; }
		public string locality { get; set; }
		public string additionalLine { get; set; }
		public string town { get; set; }
		public string county { get; set; }
		public string postcode { get; set; }
	}

	public class Establishment
	{
		public string urn { get; set; }
		public string localAuthorityCode { get; set; }
		public string localAuthorityName { get; set; }
		public string establishmentNumber { get; set; }
		public string establishmentName { get; set; }
		public Establishmenttype establishmentType { get; set; }
		public Establishmenttypegroup establishmentTypeGroup { get; set; }
		public Establishmentstatus establishmentStatus { get; set; }
		public Reasonestablishmentopened reasonEstablishmentOpened { get; set; }
		public string openDate { get; set; }
		public Reasonestablishmentclosed reasonEstablishmentClosed { get; set; }
		public string closeDate { get; set; }
		public Phaseofeducation phaseOfEducation { get; set; }
		public string statutoryLowAge { get; set; }
		public string statutoryHighAge { get; set; }
		public Boarders boarders { get; set; }
		public string nurseryProvision { get; set; }
		public Officialsixthform officialSixthForm { get; set; }
		public Gender gender { get; set; }
		public Religiouscharacter religiousCharacter { get; set; }
		public string religiousEthos { get; set; }
		public Diocese diocese { get; set; }
		public Admissionspolicy admissionsPolicy { get; set; }
		public string schoolCapacity { get; set; }
		public Specialclasses specialClasses { get; set; }
		public Census census { get; set; }
		public Trustschoolflag trustSchoolFlag { get; set; }
		public Trusts trusts { get; set; }
		public string schoolSponsorFlag { get; set; }
		public string schoolSponsors { get; set; }
		public string federationFlag { get; set; }
		public Federations federations { get; set; }
		public string ukprn { get; set; }
		public string feheiIdentifier { get; set; }
		public string furtherEducationType { get; set; }
		public string ofstedLastInspection { get; set; }
		public Ofstedspecialmeasures ofstedSpecialMeasures { get; set; }
		public string lastChangedDate { get; set; }
		public Address address { get; set; }
		public string schoolWebsite { get; set; }
		public string telephoneNumber { get; set; }
		public string headteacherTitle { get; set; }
		public string headteacherFirstName { get; set; }
		public string headteacherLastName { get; set; }
		public string headteacherPreferredJobTitle { get; set; }
		public string inspectorateName { get; set; }
		public string inspectorateReport { get; set; }
		public string dateOfLastInspectionVisit { get; set; }
		public string dateOfNextInspectionVisit { get; set; }
		public string teenMoth { get; set; }
		public string teenMothPlaces { get; set; }
		public string ccf { get; set; }
		public string senpru { get; set; }
		public string ebd { get; set; }
		public string placesPRU { get; set; }
		public string ftProv { get; set; }
		public string edByOther { get; set; }
		public string section14Approved { get; set; }
		public string seN1 { get; set; }
		public string seN2 { get; set; }
		public string seN3 { get; set; }
		public string seN4 { get; set; }
		public string seN5 { get; set; }
		public string seN6 { get; set; }
		public string seN7 { get; set; }
		public string seN8 { get; set; }
		public string seN9 { get; set; }
		public string seN10 { get; set; }
		public string seN11 { get; set; }
		public string seN12 { get; set; }
		public string seN13 { get; set; }
		public string typeOfResourcedProvision { get; set; }
		public string resourcedProvisionOnRoll { get; set; }
		public string resourcedProvisionOnCapacity { get; set; }
		public string senUnitOnRoll { get; set; }
		public string senUnitCapacity { get; set; }
		public Gor gor { get; set; }
		public Districtadministrative districtAdministrative { get; set; }
		public Administractiveward administractiveWard { get; set; }
		public Parliamentaryconstituency parliamentaryConstituency { get; set; }
		public Urbanrural urbanRural { get; set; }
		public string gsslaCode { get; set; }
		public string easting { get; set; }
		public string northing { get; set; }
		public string censusAreaStatisticWard { get; set; }
		public Msoa msoa { get; set; }
		public Lsoa lsoa { get; set; }
		public string senStat { get; set; }
		public string senNoStat { get; set; }
		public string boardingEstablishment { get; set; }
		public string propsName { get; set; }
		public Previouslocalauthority previousLocalAuthority { get; set; }
		public string previousEstablishmentNumber { get; set; }
		public string ofstedRating { get; set; }
		public string rscRegion { get; set; }
		public string country { get; set; }
		public string uprn { get; set; }
		public Misestablishment misEstablishment { get; set; }
		public Misfurthereducationestablishment misFurtherEducationEstablishment { get; set; }
		public Viewacademyconversion viewAcademyConversion { get; set; }
		public Smartdata smartData { get; set; }
		public Financial financial { get; set; }
		public Concerns concerns { get; set; }
	}

	public class Establishmenttype
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Establishmenttypegroup
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Establishmentstatus
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Reasonestablishmentopened
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Reasonestablishmentclosed
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Phaseofeducation
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Boarders
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Officialsixthform
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Gender
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Religiouscharacter
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Diocese
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Admissionspolicy
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Specialclasses
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Census
	{
		public string censusDate { get; set; }
		public string numberOfPupils { get; set; }
		public string numberOfBoys { get; set; }
		public string numberOfGirls { get; set; }
		public string percentageSen { get; set; }
		public string percentageFsm { get; set; }
		public string percentageEnglishNotFirstLanguage { get; set; }
		public string perceantageEnglishFirstLanguage { get; set; }
		public string percentageFirstLanguageUnclassified { get; set; }
		public string numberEligableForFSM { get; set; }
		public string numberEligableForFSM6Years { get; set; }
		public string percentageEligableForFSM6Years { get; set; }
	}

	public class Trustschoolflag
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Trusts
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Federations
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Ofstedspecialmeasures
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Address
	{
		public string street { get; set; }
		public string locality { get; set; }
		public string additionalLine { get; set; }
		public string town { get; set; }
		public string county { get; set; }
		public string postcode { get; set; }
	}

	public class Gor
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Districtadministrative
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Administractiveward
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Parliamentaryconstituency
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Urbanrural
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Msoa
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Lsoa
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Previouslocalauthority
	{
		public string name { get; set; }
		public string code { get; set; }
	}

	public class Misestablishment
	{
		public string siteName { get; set; }
		public string webLink { get; set; }
		public string laestab { get; set; }
		public string schoolName { get; set; }
		public string ofstedPhase { get; set; }
		public string typeOfEducation { get; set; }
		public string schoolOpenDate { get; set; }
		public string sixthForm { get; set; }
		public string designatedReligiousCharacter { get; set; }
		public string religiousEthos { get; set; }
		public string faithGrouping { get; set; }
		public string ofstedRegion { get; set; }
		public string region { get; set; }
		public string localAuthority { get; set; }
		public string parliamentaryConstituency { get; set; }
		public string postcode { get; set; }
		public string incomeDeprivationAffectingChildrenIndexQuintile { get; set; }
		public string totalNumberOfPupils { get; set; }
		public string latestSection8InspectionNumberSinceLastFullInspection { get; set; }
		public string section8InspectionRelatedToCurrentSchoolUrn { get; set; }
		public string urnAtTimeOfSection8Inspection { get; set; }
		public string schoolNameAtTimeOfSection8Inspection { get; set; }
		public string schoolTypeAtTimeOfSection8Inspection { get; set; }
		public string numberOfSection8InspectionsSinceLastFullInspection { get; set; }
		public string dateOfLatestSection8Inspection { get; set; }
		public string section8InspectionPublicationDate { get; set; }
		public string latestSection8InspectionConvertedToFullInspection { get; set; }
		public string section8InspectionOverallOutcome { get; set; }
		public string inspectionNumberOfLatestFullInspection { get; set; }
		public string inspectionType { get; set; }
		public string inspectionTypeGrouping { get; set; }
		public string inspectionStartDate { get; set; }
		public string inspectionEndDate { get; set; }
		public string publicationDate { get; set; }
		public string latestFullInspectionRelatesToCurrentSchoolUrn { get; set; }
		public string schoolUrnAtTimeOfLastFullInspection { get; set; }
		public string laestabAtTimeOfLastFullInspection { get; set; }
		public string schoolNameAtTimeOfLastFullInspection { get; set; }
		public string schoolTypeAtTimeOfLastFullInspection { get; set; }
		public string overallEffectiveness { get; set; }
		public string categoryOfConcern { get; set; }
		public string qualityOfEducation { get; set; }
		public string behaviourAndAttitudes { get; set; }
		public string personalDevelopment { get; set; }
		public string effectivenessOfLeadershipAndManagement { get; set; }
		public string safeguardingIsEffective { get; set; }
		public string earlyYearsProvision { get; set; }
		public string sixthFormProvision { get; set; }
		public string previousFullInspectionNumber { get; set; }
		public string previousInspectionStartDate { get; set; }
		public string previousInspectionEndDate { get; set; }
		public string previousPublicationDate { get; set; }
		public string previousFullInspectionRelatesToUrnOfCurrentSchool { get; set; }
		public string urnAtTheTimeOfPreviousFullInspection { get; set; }
		public string laestabAtTheTimeOfPreviousFullInspection { get; set; }
		public string schoolNameAtTheTimeOfPreviousFullInspection { get; set; }
		public string schoolTypeAtTheTimeOfPreviousFullInspection { get; set; }
		public string previousFullInspectionOverallEffectiveness { get; set; }
		public string previousCategoryOfConcern { get; set; }
		public string previousQualityOfEducation { get; set; }
		public string previousBehaviourAndAttitudes { get; set; }
		public string previousPersonalDevelopment { get; set; }
		public string previousEffectivenessOfLeadershipAndManagement { get; set; }
		public string previousIsSafeguardingEffective { get; set; }
		public string previousEarlyYearsProvision { get; set; }
		public string previousSixthFormProvision { get; set; }
	}

	public class Misfurthereducationestablishment
	{
		public Provider provider { get; set; }
		public string localAuthority { get; set; }
		public string region { get; set; }
		public string ofstedRegion { get; set; }
		public string dateOfLatestShortInspection { get; set; }
		public string numberOfShortInspectionsSinceLastFullInspectionRAW { get; set; }
		public string numberOfShortInspectionsSinceLastFullInspection { get; set; }
		public string inspectionNumber { get; set; }
		public string inspectionType { get; set; }
		public string firstDayOfInspection { get; set; }
		public string lastDayOfInspection { get; set; }
		public string datePublished { get; set; }
		public string overallEffectivenessRAW { get; set; }
		public string overallEffectiveness { get; set; }
		public string qualityOfEducationRAW { get; set; }
		public string qualityOfEducation { get; set; }
		public string behaviourAndAttitudesRAW { get; set; }
		public string behaviourAndAttitudes { get; set; }
		public string personalDevelopmentRAW { get; set; }
		public string personalDevelopment { get; set; }
		public string effectivenessOfLeadershipAndManagementRAW { get; set; }
		public string effectivenessOfLeadershipAndManagement { get; set; }
		public string isSafeguardingEffective { get; set; }
		public string previousInspectionNumber { get; set; }
		public string previousLastDayOfInspection { get; set; }
		public string previousOverallEffectivenessRAW { get; set; }
		public string previousOverallEffectiveness { get; set; }
	}

	public class Provider
	{
		public int urn { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string group { get; set; }
		public int ukprn { get; set; }
	}

	public class Viewacademyconversion
	{
		public string viabilityIssue { get; set; }
		public string pfi { get; set; }
		public string pan { get; set; }
		public string deficit { get; set; }
	}

	public class Smartdata
	{
		public string probabilityOfDeclining { get; set; }
		public string probabilityOfStayingTheSame { get; set; }
		public string probabilityOfImproving { get; set; }
		public string predictedChangeInProgress8Score { get; set; }
		public string predictedChanceOfChangeOccurring { get; set; }
		public string totalNumberOfRisks { get; set; }
		public string totalRiskScore { get; set; }
		public string riskRatingNum { get; set; }
	}

	public class Financial
	{
		public string urn { get; set; }
		public string ukprn { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string group { get; set; }
	}

	public class Concerns
	{
		public string urn { get; set; }
		public string ukprn { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string group { get; set; }
	}

}
