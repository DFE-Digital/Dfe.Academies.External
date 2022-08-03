using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Services;

public interface IReferenceDataRetrievalService
{
	/// <summary>
	/// Method to call academies API Establishments Search endpoint
	/// </summary>
	/// <param name="schoolSearch"></param>
	/// <returns></returns>
	Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch);

	/// <summary>
	/// Method to call academies API Get Establishment endpoint
	/// </summary>
	/// <param name="urn"></param>
	/// <returns></returns>
	Task<EstablishmentResponse> GetSchool(int urn);
}