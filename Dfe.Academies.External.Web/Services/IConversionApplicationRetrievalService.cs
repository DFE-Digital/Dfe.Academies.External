using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;
public interface IConversionApplicationRetrievalService
{
	List<ConversionApplication> GetCompletedApplications(string? username);
	List<ConversionApplication> GetPendingApplications(string? username);

	Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(int applicationId);

	Task<List<ConversionApplicationComponent>> GetSchoolApplicationComponents(int schoolId);

	Task<List<ConversionApplicationContributor>> GetConversionApplicationContributors(int applicationId);

	Task<SchoolApplyingToConvert> GetSchool(int schoolId);

	Task<ConversionApplication> GetApplication(int applicationId, ApplicationTypes applicationType);
}
