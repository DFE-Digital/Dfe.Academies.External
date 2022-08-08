using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IConversionApplicationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

    Task UpdateDraftApplication(ConversionApplication application);

    Task AddSchoolToApplication(int applicationId, int schoolUrn, string name);

    Task AddTrustToApplication(int applicationId, int trustUkPrn, string name);

    Task ApplicationAddJoinTrustReasons(ConversionApplication application, string applicationJoinTrustReason);

    Task ApplicationChangeSchoolNameAndReason(ConversionApplication application, SelectOption changeName,
	    string changeSchoolNameReason);
}