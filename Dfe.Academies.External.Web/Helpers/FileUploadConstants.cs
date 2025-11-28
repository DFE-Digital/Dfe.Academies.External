namespace Dfe.Academies.External.Web.Helpers;

public static class FileUploadConstants
{
	public const string TopLevelApplicationFolderName = "sip_application";
	public const string TopLevelSchoolFolderName = "sip_applyingschools";
	public const string DioceseFilePrefixFieldName = "sip_adschoolfaithschoolfile";
	public const string FoundationConsentFilePrefixFieldName = "sip_adschoolsupportedfoundationfile";
	public const string ResolutionConsentfilePrefixFieldName = "sip_adschoolgoverningbodyconsent";
	public const string SchoolPFYRevenueStatusFile = "sip_pfyrevenuepreviousfinancialstatusfile";
	public const string SchoolPFYCapitalForwardStatusFile = "sip_pfyrevenuecapitalcarriedforwardfile";
	public const string SchoolCFYRevenueStatusFile = "sip_cfyrevenuecurrentfinancialstatusfile";
	public const string SchoolCFYCapitalForwardFile = "sip_cfyforecastcapitalcarriedforwardfile";
	public const string NFYForecastedRevenueFilePrefixFieldName = "sip_nfynextcurrentfinancialfile";
	public const string NFYForecastedCapitalFilePrefixFieldName = "sip_nfyforecastcapitalcarriedforwardfile";
	public const string JoinAMatTrustConsentFilePrefixFieldName = "sip_changestotrustconsent";
	public const string JoinAMatTrustGovernanceFilePrefixFieldName = "sip_formtrustgovernancefile";
	public const int MaxFileUploadSizeInBytes = 8 * 1024 * 1024; // 8 MB
	public const string MaxFileUploadErrorMessage = "The file must be smaller than 8MB";
}
