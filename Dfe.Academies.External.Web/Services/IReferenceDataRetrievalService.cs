using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Services;

public interface IReferenceDataRetrievalService
{
	Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch);

	Task<EstablishmentResponse> GetSchool(int schoolId);

	/// <summary>
	/// Method to call academies API Trusts Search endpoint
	/// </summary>
	/// <param name="trustSearch"></param>
	/// <returns></returns>
	Task<List<TrustSearchDto>> GetTrusts(TrustSearch trustSearch);

	/// <summary>
	/// Method to call academies API Get Trust endpoint
	/// </summary>
	/// <param name="ukPrn"></param>
	/// <returns></returns>
	Task<TrustDetailsDto> GetTrustByUkPrn(string ukPrn);
}