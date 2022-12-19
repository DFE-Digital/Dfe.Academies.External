using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
	Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

	Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

	Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);
	Task SetExistingTrustDetails(int applicationId, ExistingTrust existingTrust);

	Task PutSchoolApplicationDetails(int applicationId, int schoolUrn, Dictionary<string, dynamic> schoolProperties);
	//Task UpdateDraftApplication(ConversionApplication application);

	Task AddContributorToApplication(ConversionApplicationContributor contributor, int applicationId);

	Task RemoveContributorFromApplication(int contributorId, int applicationId);

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
		string? dioceseFolderIdentifier,
		bool partOfFederation,
		string? foundationTrustOrBodyName,
		string? foundationConsentFolderIdentifier, 
		DateTimeOffset? exemptionEndDate,
		string mainFeederSchools,
		string? resolutionConsentFolderIdentifier,
		SchoolEqualitiesProtectedCharacteristics? protectedCharacteristics,
		string? furtherInformation);

	/// <summary>
	/// Create / update application form a trust details
	/// </summary>
	/// <param name="applicationId"></param>
	/// <param name="famTrustProperties"></param>
	/// <returns></returns>
	Task PutApplicationFormAMatDetails(int applicationId, Dictionary<string, dynamic> famTrustProperties);

	/// <summary>
	/// Final application stage, submit, can only be done when everything is filled in.
	/// Access controlled by UI
	/// </summary>
	/// <param name="applicationId"></param>
	/// <returns></returns>
	Task SubmitApplication(int applicationId);

	Task CreateKeyPerson(int applicationId, NewTrustKeyPerson person);
	Task UpdateKeyPerson(int applicationId, NewTrustKeyPerson person);
	Task DeleteKeyPerson(int applicationId, int keyPersonId);

	Task RemoveSchoolFromApplication(int applicationId, int schoolUrn);
}
