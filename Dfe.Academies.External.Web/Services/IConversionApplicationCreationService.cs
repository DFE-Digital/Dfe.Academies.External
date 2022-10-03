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
}
