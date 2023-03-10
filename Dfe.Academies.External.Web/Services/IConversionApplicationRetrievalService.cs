using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;

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
	Task<List<ApplicationComponentViewModel>> GetSchoolApplicationComponents(int applicationId, int schoolId);

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

	Task<List<ApplicationSchoolSharepointServiceModel>> GetAllApplications();

	/// <summary>
	/// Calculate whether all trust sections of application have been filled in.
	/// Could return InProgress or Completed or NotStarted because inner method will return that
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

	Status CalculateSchoolStatus(List<ApplicationComponentViewModel> schoolComponents);

	/// <summary>
	/// Calculate overall application status based on whether all sections have been completed
	/// INCLUDING trust
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	Status CalculateApplicationStatus(ConversionApplication? conversionApplication,
		IEnumerable<SchoolComponentsViewModel> schoolComponents);

	/// <summary>
	/// Return a list<viewmodel> to render FAM application progress on screen
	/// </summary>
	/// <param name="applicationId"></param>
	/// <returns></returns>
	Task<List<ApplicationComponentViewModel>> GetFormAMatTrustComponents(int applicationId);

	/// <summary>
	/// Calculate FAM opening date section status
	/// </summary>
	/// <param name="applicationFormTrustDetails"></param>
	/// <returns></returns>
	Status CalculateOpeningDateSectionStatus(NewTrust applicationFormTrustDetails);
}
