using Dfe.Academies.External.Web.Enums;
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
	/// Not sure whether this is needed for V1.5
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

	/// <summary>
	/// calculate whether all trust sections of application have been filled in
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	Status CalculateTrustStatus(ConversionApplication? conversionApplication);

	/// <summary>
	/// calc JAM trust status - JAM specific components = 6 sections - could return InProgress or Completed or NotStarted
	/// Contains same logic in here as ApplicationSchoolJoinAMatTrustSummary page.
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	Status CalculateJoinAMatTrustStatus(ConversionApplication? conversionApplication);

	/// <summary>
	/// calc FAM trust status - FAM specific components !! could return InProgress or Completed or NotStarted
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	Status CalculateFormAMatTrustStatus(ConversionApplication? conversionApplication);

	/// <summary>
	/// Calc whether school declaration has been filled in
	/// Will only return Completed or NotStarted as only one logic check !
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	Status CalculateApplicationDeclarationStatus(ConversionApplication? conversionApplication);
}
