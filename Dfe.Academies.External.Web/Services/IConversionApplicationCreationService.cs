using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
	Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

	Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

	Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);
	
	Task PutSchoolApplicationDetails(int applicationId, int schoolUrn, Dictionary<string, dynamic> schoolProperties);
	//Task UpdateDraftApplication(ConversionApplication application);

	Task AddContributorToApplication(ConversionApplicationContributor contributor, int applicationId);

	Task CreateLoan(int applicationId, int schoolId, SchoolLoan loan);
	Task UpdateLoan(int applicationId, int schoolId, SchoolLoan loan);
	Task DeleteLoan(int applicationId, int schoolId, int loanId);
	Task CreateLease(int applicationId, int schoolId, SchoolLease lease);
	Task UpdateLease(int applicationId, int schoolId, SchoolLease lease);
	Task DeleteLease(int applicationId, int schoolId, int leaseId);
	Task SetAdditionalDetails(int applicationId, 
		int schoolId,
		string trustBenefitDetails, 
		string? ofstedInspectionDetails, 
		string? safeguardingDetails, 
		string? localAuthorityReorganisationDetails,
		string? localAuthorityClosurePlanDetails,
		string? dioceseName,
		string dioceseFolderIdentifier,
		bool partOfFederation,
		string? foundationTrustOrBodyName,
		string foundationConsentFolderIdentifier, 
		DateTimeOffset? exemptionEndDate,
		string mainFeederSchools,
		string resolutionConsentFolderIdentifier,
		SchoolEqualitiesProtectedCharacteristics? protectedCharacteristics,
		string? furtherInformation);
}
