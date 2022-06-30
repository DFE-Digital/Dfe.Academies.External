using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IAcademisationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);

    Task UpdateDraftApplication(ConversionApplication application);
}