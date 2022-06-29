using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public interface IAcademisationCreationService
{
    Task<ConversionApplication> CreateNewApplication(ConversionApplication application);
}