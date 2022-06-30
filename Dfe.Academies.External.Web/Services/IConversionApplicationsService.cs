using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;
public interface IConversionApplicationsService
{
    List<ConversionApplication> GetCompletedApplications(string? username);
    List<ConversionApplication> GetPendingApplications(string? username);
}
