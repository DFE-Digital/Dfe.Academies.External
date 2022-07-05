using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;
public interface IConversionApplicationRetrievalService
{
    List<ConversionApplication> GetCompletedApplications(string? username);
    List<ConversionApplication> GetPendingApplications(string? username);

    Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(long id);

    Task<List<ConversionApplicationComponent>> GetConversionApplicationComponentStatuses(long id);
}
