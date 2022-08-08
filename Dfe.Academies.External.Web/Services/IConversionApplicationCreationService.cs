using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

    Task UpdateDraftApplication(ConversionApplication application);

    Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

    Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);
}