using Dfe.Academies.Contracts.V4;
using Dfe.Academies.Contracts.V4.Trusts;
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

	/// <summary>
	/// Method to call academies API Trusts Search endpoint
	/// </summary>
	/// <param name="trustSearch"></param>
	/// <returns></returns>
	Task<PagedDataResponse<TrustDto>> GetTrusts(TrustSearch trustSearch);

	Task<TrustDto> GetTrustByUkPrn(string ukPrn);
}
