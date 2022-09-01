namespace Dfe.Academies.External.Web.Constants;

internal static class FieldConstants
{
	public const string CurrentPage = "CurrentPage";
	public const string NextPage = "NextPage";
	public const string TrustName = "TrustName";
	public const string SchoolName = "SchoolName";
	public const string ViewProperties = "ViewProperties";
	public const string ViewFileProperties = "ViewFileProperties";
	public const string ApplicationTypeDescription = "ApplicationTypeDescription";
	public const string ApplicationWordCountMax = "ApplicationWordCountMax";
	public const string ApplicationFileSizeLimitMb = "ApplicationFileSizeLimitMb";
	public const string ApplicationFileExtensionsAllowed = "ApplicationFileExtensionsAllowed";

	public const string Name = "name";
	public const string TrustSearchEstablishmentGroupIds = "TrustSearchEstablishmentGroupIds";
	public const string SchoolSearchEstablishmentGroupIds = "SchoolSearchEstablishmentGroupIds";
	public const string AreYouSure = "AreYouSure";

	public const string SipEntity = "entity";
	public const string SipApplication = "sip_applications";
	public const string SipAccount = "accounts";
	public const string SipAccountId = "accountid";
	public const string SipApplyingSchool = "sip_applyingschoolses";
	public const string SipApplyingSchoolLoan = "sip_schoolloans";
	public const string SipApplyingSchoolLease = "sip_schoolleaseses";
	public const string SipApplicationRecordStatus = "sip_externalapplicationrecordstatuses";
	public const string SipEstablishmentGroupId = "_sip_establismenttypegroupid_value";
	public const string SipKeyPerson = "sip_applicationkeypersons";
	public const string SipContributor = "sip_contributors";

	public const string SipName = "sip_name";
	public const string SipCreatedOn = "createdon";
	public const string SipApplicationType = "sip_a2capplicationtype";
	public const string SipApplicationRole = "sip_a2capplicationrole";
	public const string SipApplicationRoleOtherDescription = "sip_a2capplicationroleotherdescription";
	public const string SipApplicationId = "sip_applicationid";
	public const string SipApplicationTypeId = "sip_applicationtypeid";
	public const string SipAddress1Composite = "address1_composite";
	public const string SipApplicationLeadAuthorId = "sip_leadauthorid";
	public const string SipApplicationLeadAuthorName = "sip_leadauthorname";
	public const string SipApplicationLeadEmail = "sip_leadauthoremail";
	public const string SipApplicationSubmitted = "sip_aplicationsubmitted";
	public const string SipApplicationStatusId = "sip_applicationstatusid";
	public const string SipApplicationSubmittedDate = "sip_submitteddate";
	public const string SipApplicationUrl = "sip_applicationurl";

	public const string SipApplicationRecordStatusId = "sip_externalapplicationrecordstatusid";
	public const string SipApplicationRecordStatusKey = "sip_statuskey";
	public const string SipApplicationRecordStatusValue = "sip_statusvalue";

	public const string SipApplicationVersion = "sip_applicationtypeversion";

	public const string SipContributorUserId = "sip_useridtext";
	public const string SipContributorUserName = "sip_contributornametext";
	public const string SipContributorAppIdText = "sip_applicationidtext";

	#region Trust Constants

	public const string SipTrustId = "sip_Trustid";
	public const string SipTrustReferenceNumber = "sip_trustreferencenumber";
	public const string SipTrustCompanyNumber = "sip_companieshousenumber";
	public const string SipTrustApproverName = "sip_dfesigninapprover";
	public const string SipTrustApproverEmail = "sip_dfesigninapproveremail";

	#region JoinTrust
	public const string SipChangesToTrust = "sip_changestothetrust";
	public const string SipChangesToTrustExplained = "sip_changestotrustexplained";
	public const string SipChangesToLAGovernance = "sip_changestolocalgovernancedueschooljoining";
	public const string SipChangesToLAGovernanceExplained = "sip_changestolocalgovernanceexplained";
	public const string SipChangesToTrustConsent = "sip_changestotrustconsent"; // File upload
	#endregion JoinTrust

	#region FormMat/SAT
	// Name
	public const string SipFormTrustProposedNameOfTrust = "sip_formtrustproposedname";

	// Opening Date
	public const string SipFormTrustOpeningDate = "sip_formtrustopeningdate";

	// Reasons for forming trust
	public const string SipFormTrustReasonForming = "sip_formtrustreasonforming";
	public const string SipFormTrustReasonVision = "sip_formtrustreasonvision";
	public const string SipFormTrustReasonGeoAreas = "sip_formtrustreasongeoareas";
	public const string SipFormTrustReasonFreedom = "sip_formtrustreasonfreedom";
	public const string SipFormTrustReasonImproveTeaching = "sip_formtrustreasonimproveteaching";
	public const string SipFormTrustReasonApprovaltoConvertasSAT = "sip_rscofficeapprovaltoconvertassat";
	public const string SipFormTrustReasonApprovedPerson = "sip_rscsatapprovedperson";


	// Plans for growing
	public const string SipFormTrustPlanForGrowth = "sip_formtrustplanforgrowth"; // File Upload
	public const string SipFormTrustPlansForNoGrowth = "sip_whyareyounotplanningtogrow";
	public const string SipFormTrustGrowthPlansYesNo = "sip_growthplanned";


	// School improvement strategy
	public const string SipFormTrustImprovementSupport = "sip_formtrustimprovementsupport";
	public const string SipFormTrustImprovementStrategy = "sip_formtrustimprovementstrategy";
	public const string SipFormTrustImprovementApprovedSponsor = "sip_formtrusiimprovementapprovedsponsor";

	// Trust governance
	public const string SipFormTrustGovernanceFile = "sip_formtrustgovernancefile";

	// Key People
	public const string SIPKeyPersonId = "sip_applicationkeypersonid";
	public const string SIPKeyPersonBiography = "sip_biography";
	public const string SIPKeyPersonCeoExecutive = "sip_ceoexecutive";
	public const string SIPKeyPersonChairOfTrust = "sip_chairoftrust";
	public const string SIPKeyPersonDateOfBirth = "sip_dateofbirth";
	public const string SIPKeyPersonFinancialDirector = "sip_financialdirector";
	public const string SIPKeyPersonFinancialDirectorTime = "sip_financialdirectortime";
	public const string SIPKeyPersonMember = "sip_member";
	public const string SIPKeyPersonName = "sip_name";
	public const string SIPKeyPersonOther = "sip_other";
	public const string SIPKeyPersonTrustee = "sip_trustee";


	#endregion FormMat/SAT

	#endregion Trust Constants

	#region School Constants
	public const string SipSchoolId = "sip_schoolid";
	public const string SipApplyingSchoolsId = "sip_applyingschoolsid";
	public const string SipApplyingSchoolId = "sip_applyingschoolid";
	public const string SipUrn = "sip_urn";
	public const string SipUpdatedTrustFields = "sip_updatedtrustfields";
	public const string SipUpdatedSchoolFields = "sip_updatedschoolfields";

	// Main Contact
	public const string SipSchoolConversionContactHeadName = "sip_nameofheadteacher";
	public const string SipSchoolConversionContactHeadEmail = "sip_headteacheremailaddress";
	public const string SipSchoolConversionContactHeadTel = "sip_headteachertelephonenumber";
	public const string SipSchoolConversionContactChairName = "sip_nameofthechairofthegoverningbody";
	public const string SipSchoolConversionContactChairEmail = "sip_chairsemailaddress";
	public const string SipSchoolConversionContactChairTel = "sip_chairstelephonenumber";
	public const string SipSchoolConversionContactOther = "sip_maincontactfortheconversionprocess";
	public const string SipSchoolConversionApproverContactName = "sip_newacademycontact";
	public const string SipSchoolConversionApproverContactEmail = "sip_newacademycontactemail";
	public const string SipSchoolConversionMainContact = "sip_schoolconversionmaincontact";
	public const string SipSchoolConversionMainContactOtherName = "sip_schoolconversionmaincontactothername";
	public const string SipSchoolConversionMainContactOtherEmail = "sip_schoolconversionmaincontactotheremail";
	public const string SipSchoolConversionMainContactOtherTelephone = "sip_schoolconversionmaincontactothertelephone";
	public const string SipSchoolConversionMainContactOtherRole = "sip_schoolconversionmaincontactotherrole";

	// Conversion Target Date
	public const string SipSchoolConversionTargetDateDifferent = "sip_ctddiferentdate";
	public const string SipSchoolConversionTargetDateDate = "sip_ctddiferentdatevalue";
	public const string SipSchoolConversionTargetDateExplained = "sip_ctddiferentdatevalueexplained";
	public const string SipSchoolConversionChangeName = "sip_ctdchangename";
	public const string SipSchoolConversionChangeNameValue = "sip_ctdchangenamevalue";

	// Conversion Rationale
	public const string SipSchoolConversionReasonsForJoining = "sip_ckdreasonsforjoining";

	// Further Info
	public const string SIPSchoolAdSchoolContributionToTrust = "sip_adschoolcontributiontotrust";
	public const string SIPSchoolAdInspectedButReportNotPublished = "sip_adinspectedbutreportnotpublishedyet";
	public const string SIPSchoolAdInspectedReportNotPublishedExplain = "sip_adinspectedbutreportnotpublishedyetexplai";
	public const string SIPSchoolLaReorganization = "sip_adschoolpartoflareorganization";
	public const string SIPSchoolLaReorganizationExplain = "sip_adschoolpartoflareorganizationexplained";
	public const string SIPSchoolLaClosurePlans = "sip_adschoolpartoflaclosureplans";
	public const string SIPSchoolLaClosurePlansExplain = "sip_adschoolpartoflaclosureplansexplained";
	public const string SIPSchoolPartOfFederation = "sip_adschoolpartoffederation";
	public const string SIPSchoolPartOfFederationExplained = "sip_adschoolpartoffederationplansexplained";
	public const string SIPSchoolFurtherInformation = "sip_adschoolfurtherinformation";
	public const string SIPSchoolAdSafeguarding = "sip_adschoolsafeguarding";
	public const string SIPSchoolAdSafeguardingExplained = "sip_adschoolsafeguardingexplained";
	public const string SIPSchoolSACREExemption = "sip_adschoolsacreexemption";
	public const string SIPSchoolSACREExemptionEndDate = "sip_adschoolsacreexemptionenddate";
	public const string SIPSchoolFaithSchool = "sip_adschoolfaithschool";
	public const string SIPSchoolFaithSchoolDioceseName = "sip_adschoolfaithschooldiocesename";
	public const string SIPSchoolFaithSchoolFile = "sip_adschoolfaithschoolfile";// File upload  
	public const string SIPSchoolSupportedFoundation = "sip_adschoolsupportedfoundation";
	public const string SIPSchoolSupportedFoundationBodyName = "sip_adschoolsupportedfoundationbodyname";
	public const string SIPSchoolSupportedFoundationFile = "sip_adschoolsupportedfoundationfile";// File upload
	public const string SIPSchoolAddFurtherInformation = "sip_adschooladdfurtherinformation";
	public const string SIPSchoolGoverningBodyConsent = "sip_adschoolgoverningbodyconsent"; // File upload
	public const string SIPSchoolAdFeederSchools = "sip_adschoolfeederschools";
	public const string SIPSchoolAdEqualitiesImpactAssessment = "sip_adschoolequalityimpactassessment";

	// Previous Financial Year
	public const string SIPSchoolPFYEndDate = "sip_pfyenddate";
	public const string SIPSchoolPFYRevenue = "sip_pfyrevenuepreviousfinancialyear";
	public const string SIPSchoolPFYRevenueStatus = "sip_pfyrevenuepreviousfinancialstatus";
	public const string SIPSchoolPFYRevenueStatusExplained = "sip_pfyrevenuepreviousfinancialstatusexplain";
	public const string SIPSchoolPFYRevenueStatusFile = "sip_pfyrevenuepreviousfinancialstatusfile";// File Upload
	public const string SIPSchoolPFYCapitalForward = "sip_pfyrevenuecapitalcarriedforward";
	public const string SIPSchoolPFYCapitalForwardStatus = "sip_pfyrevenuecapitalcarriedforwardstatus";
	public const string SIPSchoolPFYCapitalForwardStatusExplained = "sip_pfyrevenuecapitalcarriedforwardstatusexpl";
	public const string SIPSchoolPFYCapitalForwardStatusFile = "sip_pfyrevenuecapitalcarriedforwardfile";// File Upload

	// Current Financial Year
	public const string SIPSchoolCFYEndDate = "sip_cfyenddate";
	public const string SIPSchoolCFYRevenue = "sip_cfyrevenuecurrentfinancialyear";
	public const string SIPSchoolCFYRevenueStatus = "sip_cfyrevenuecurrentfinancialstatus";
	public const string SIPSchoolCFYRevenueStatusExplained = "sip_cfyrevenuecurrentfinancialstatusexplained";
	public const string SIPSchoolCFYRevenueStatusFile = "sip_cfyrevenuecurrentfinancialstatusfile";// File Upload
	public const string SIPSchoolCFYCapitalForward = "sip_cfyforecastcapitalcarriedforward";
	public const string SIPSchoolCFYCapitalForwardStatus = "sip_cfyforecastcapitalcarriedforwardstatus";
	public const string SIPSchoolCFYCapitalForwardStatusExplained = "sip_cfyforecastcapitalcarriedforwardstatusexp";
	public const string SIPSchoolCFYCapitalForwardFile = "sip_cfyforecastcapitalcarriedforwardfile";// File Upload

	// Next Financial Year
	public const string SIPSchoolNFYEndDate = "sip_nfyenddate";
	public const string SIPSchoolNFYRevenue = "sip_nfynextcurrentfinancialyear";
	public const string SIPSchoolNFYRevenueStatus = "sip_nfynextcurrentfinancialstatus";
	public const string SIPSchoolNFYRevenueStatusExplained = "sip_nfynextcurrentfinancialstatusexplained";
	public const string SIPSchoolNFYRevenueStatusFile = "sip_nfynextcurrentfinancialfile"; // File Upload
	public const string SIPSchoolNFYCapitalForward = "sip_nfyforecastcapitalcarriedforward";
	public const string SIPSchoolNFYCapitalForwardStatus = "sip_nfyforecastcapitalcarriedforwardstatus";
	public const string SIPSchoolNFYCapitalForwardStatusExplained = "sip_nfyforecastcapitalcarriedforwardstatusexp";
	public const string SIPSchoolNFYCapitalForwardFile = "sip_nfyforecastcapitalcarriedforwardfile"; // File Upload

	// Financial Investigations
	public const string SIPSchoolFinancialInvestigations = "sip_financialinvestigations";
	public const string SIPSchoolFinancialInvestigationsExplain = "sip_financialinvestigationsexplain";
	public const string SIPSchoolFinancialInvestigationsTrustAware = "sip_financialinvestigationstrustaware";

	// Loans
	public const string SIPSchoolLoanId = "sip_schoolloanid";
	public const string SIPSchoolLoanExists = "sip_loansexisting";
	public const string SIPSchoolLoanAmount = "sip_ldtotalamount";
	public const string SIPSchoolLoanPurpose = "sip_ldpurposeloan";
	public const string SIPSchoolLoanProvider = "sip_ldprovider";
	public const string SIPSchoolLoanInterestRate = "sip_ldinterestrate";
	public const string SIPSchoolLoanSchedule = "sip_ldscheduleofpaymentexplain";

	// Leases
	public const string SIPSchoolLeaseExists = "sip_financiallease";
	public const string SIPSchoolLeaseId = "sip_schoolleasesid";
	public const string SIPSchoolLeaseTerm = "sip_financialdetails";
	public const string SIPSchoolLeaseRepaymentValue = "sip_financialtotalamount";
	public const string SIPSchoolLeaseInterestRate = "sip_interestrate";
	public const string SIPSchoolLeasePaymentToDate = "sip_paymentsmadetodate";
	public const string SIPSchoolLeasePurpose = "sip_financialpurpose";
	public const string SIPSchoolLeaseValueOfAssets = "sip_valueofassets";
	public const string SIPSchoolLeaseResponsibleForAssets = "sip_responsibilityforinsurancerepair";

	// School capacity and pupil numbers on roll
	public const string SIPSchoolCapacityYear1 = "sip_scpnprojectedpupilonrollyear1";
	public const string SIPSchoolCapacityYear2 = "sip_scpnprojectedpupilonrollyear2";
	public const string SIPSchoolCapacityYear3 = "sip_scpnprojectedpupilonrollyear3";
	public const string SIPSchoolCapacityAssumptions = "sip_scpnassumptions";
	public const string SIPSchoolCapacityPublishedAdmissionsNumber = "sip_scppublishedadmissionnumber";

	// Land and buildings
	public const string SIPSchoolBuildLandOwnerExplained = "sip_lbbuildlandownerexplained";
	public const string SIPSchoolBuildLandSharedFacilities = "sip_lbfacilitiesshared";
	public const string SIPSchoolBuildLandSharedFacilitiesExplained = "sip_lbfacililtiessharedexplained";
	public const string SIPSchoolBuildLandWorksPlanned = "sip_lbworksplanned";
	public const string SIPSchoolBuildLandWorksPlannedExplained = "sip_lbworksplannedexplained";
	public const string SIPSchoolBuildLandWorksPlannedDate = "sip_lbworksplanneddate";
	public const string SIPSchoolBuildLandGrants = "sip_lbgrants";
	public const string SIPSchoolBuildLandGrantsExplained = "sip_lbgrantsexplained";
	public const string SIPSchoolBuildLandGrantsBody = "sip_whichbodyawardedgrants";
	public const string SIPSchoolBuildLandPFIScheme = "sip_partofpfischeme";
	public const string SIPSchoolBuildLandPFISchemeType = "sip_partofpfischemetype";
	public const string SIPSchoolBuildLandPriorityBuildingProgramme = "sip_partofpriorityschoolsbuildingprogramme";
	public const string SIPSchoolBuildLandFutureProgramme = "sip_partofbuildingschoolsforfutureprogramne";

	// Consultation  
	public const string SIPSchoolConsultationStakeholders = "sip_stuatoryconsultationstakeholders";
	public const string SIPSchoolConsultationStakeholdersConsult = "sip_statutoryconsultationplan";

	// Support grant
	public const string SIPSchoolSupportGrantFundsPaidTo = "sip_acsfundspaidto";

	// Declaration
	public const string SIPSchoolDeclarationBodyAgree = "sip_declargovernbodyagree";
	public const string SIPSchoolDeclarationTeacherChair = "sip_declarheadteacherchair";
	public const string SIPSchoolDeclarationSignedById = "sip_declarsignedbyid";
	public const string SIPSchoolDeclarationSignedByName = "sip_declarsignedbyname";
	public const string SIPSchoolDeclarationSignedByEmail = "sip_declarsignedbyemail";

	#endregion School Constants
}
