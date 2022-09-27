using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;
public interface IConversionApplicationRetrievalService
{
	/// <summary>
	/// This depends on the concept of contributor and filtering by their username
	/// </summary>
	/// <param name="username"></param>
	/// <returns></returns>
	Task<List<ConversionApplication>> GetCompletedApplications(string? username);

	/// <summary>
	/// This depends on the concept of contributor and filtering by their username
	/// </summary>
	/// <param name="username"></param>
	/// <returns></returns>
	Task<List<ConversionApplication>> GetPendingApplications(string? username);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="applicationId"></param>
	/// <returns></returns>
	Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(int applicationId);

	/// <summary>
	/// Return a list<viewmodel> to render application progress on screen
	/// </summary>
	/// <param name="schoolId"></param>
	/// <returns></returns>
	Task<List<ConversionApplicationComponent>> GetSchoolApplicationComponents(int applicationId, int schoolId);

	/// <summary>
	/// Not sure whether this is needed for V1.5
	/// </summary>
	/// <param name="applicationId"></param>
	/// <returns></returns>
	Task<List<ConversionApplicationContributor>> GetConversionApplicationContributors(int applicationId);

	/// <summary>
	/// Main getApplication method
	/// </summary>
	/// <param name="applicationId"></param>
	/// <returns></returns>
	Task<ConversionApplication?> GetApplication(int applicationId);
}
