
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using GovUK.Dfe.CoreLibs.Contracts.Academies.V4.Establishments;
using GovUK.Dfe.CoreLibs.Contracts.Academies.V4.Trusts;

namespace Dfe.Academies.External.Web.Services;

public interface IReferenceDataRetrievalService
{
	/// <summary>
	/// Method to call academies API Establishments Search endpoint
	/// </summary>
	/// <param name="schoolSearch"></param>
	/// <returns></returns>
	Task<IEnumerable<EstablishmentDto>> SearchSchools(SchoolSearch schoolSearch);

	/// <summary>
	/// Method to call academies API Get Establishment endpoint
	/// </summary>
	/// <param name="urn"></param>
	/// <returns></returns>
	Task<EstablishmentDto> GetSchool(int urn);

	/// <summary>
	/// Method to call academies API Trusts Search endpoint
	/// </summary>
	/// <param name="trustSearch"></param>
	/// <returns></returns>
	Task<IEnumerable<TrustDto>> GetTrusts(TrustSearch trustSearch);

	Task<TrustDto> GetTrustByUkPrn(string ukPrn);
	Task<TrustDto> GetTrustByCompaniesHouseNumber(string companiesHouseNumber);
}
