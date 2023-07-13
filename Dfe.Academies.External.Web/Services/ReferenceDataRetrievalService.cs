using System.Web;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academisation.CorrelationIdMiddleware;

namespace Dfe.Academies.External.Web.Services;

public sealed class ReferenceDataRetrievalService : BaseService, IReferenceDataRetrievalService
{
	private readonly ILogger<ReferenceDataRetrievalService> _logger;
	private readonly ResilientRequestProvider _resilientRequestProvider;

	public ReferenceDataRetrievalService(IHttpClientFactory httpClientFactory, 
		ILogger<ReferenceDataRetrievalService> logger,
		ICorrelationContext correlationContext) : base(httpClientFactory, correlationContext, AcademiesAPIHttpClientName)
	{
		_logger = logger;
		_resilientRequestProvider = new ResilientRequestProvider(HttpClient, _logger);
	}

	///<inheritdoc/>
	public async Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch)
	{
		try
		{
			//{{api-host}}/establishments?api-version=V1&Urn=101934&ukprn=10006563&Name=wise
			string apiurl = $"{HttpClient.BaseAddress}/establishments?{BuildSchoolSearchRequestUri(schoolSearch, "V1")}";

			IList<SchoolSearchResultViewModel> schools = new List<SchoolSearchResultViewModel>();

			//// API returns list<SchoolsSearchDto>
			var schoolsSearchResults = await _resilientRequestProvider.GetAsync<List<SchoolsSearchDto>>(apiurl);

			// convert SchoolsSearchDto -> view model
			if (schoolsSearchResults.Any())
				schools = schoolsSearchResults.Select(c =>
					new SchoolSearchResultViewModel(name: c.Name, urn: int.Parse(c.Urn), ukprn: c.Ukprn)).ToList();

			return schools;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::SearchSchools::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task<EstablishmentResponse> GetSchool(int urn)
	{
		try
		{
			// {{api-host}}/establishment/urn/101934?api-version=V1
			string apiurl = $"{HttpClient.BaseAddress}/establishment/urn/{urn}?api-version=V1";

			//// API returns EstablishmentResponse
			var APIresult = await _resilientRequestProvider.GetAsync<EstablishmentResponse>(apiurl);

			return APIresult;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetSchool::Exception - {Message}", ex.Message);
			throw;
		}
	}

	//// Public method, so can write unit tests !!!!
	public string BuildSchoolSearchRequestUri(SchoolSearch schoolSearch, string apiVersionNumber)
	{
		var queryParams = HttpUtility.ParseQueryString(string.Empty);

		if (!string.IsNullOrEmpty(schoolSearch.Name))
		{
			queryParams.Add("name", schoolSearch.Name);
		}

		if (!string.IsNullOrEmpty(schoolSearch.Urn))
		{
			queryParams.Add("Urn", schoolSearch.Urn);
		}

		if (!string.IsNullOrEmpty(schoolSearch.Ukprn))
		{
			queryParams.Add("ukprn", schoolSearch.Ukprn);
		}

		queryParams.Add("api-version", apiVersionNumber);

		return HttpUtility.UrlEncode(queryParams.ToString());
	}

	///<inheritdoc/>
	public async Task<List<TrustSearchDto>> GetTrusts(TrustSearch trustSearch)
	{
		try
		{
			// {{api-host}}/trusts?api-version=V1&groupName=grammar
			string apiurl = $"{HttpClient.BaseAddress}/trusts?{BuildTrustSearchRequestUri(trustSearch)}&api-version=V1";

			// API returns ApiListWrapper<TrustSearchDto>
			var APIresult = await _resilientRequestProvider.GetAsync<List<TrustSearchDto>>(apiurl);

			return APIresult;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrusts::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task<List<TrustSummaryDto>> GetTrustByUkPrn(string ukPrn)
	{
		try
		{
			// MR:- api endpoint to build will look like this:-
			// {{api-host}}/trusts?ukprn=10058464&api-version=V1
			string apiurl = $"{HttpClient.BaseAddress}/trusts?ukprn={ukPrn}&api-version=V1";

			// API - returns ApiWrapper<TrustDetailsDto>
			var APIresult = await _resilientRequestProvider.GetAsync<List<TrustSummaryDto>>(apiurl);

			return APIresult;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrustByUkPrn::Exception - {Message}", ex.Message);
			throw;
		}
	}

	public async Task<TrustFullDetailsDto> GetTrustFullDetailsByUkPrn(string ukPrn)
	{
		try
		{
			string apiUrl = $"{HttpClient.BaseAddress}/trust/{ukPrn}?api-version=V1";


			var result = await _resilientRequestProvider.GetAsync<TrustFullDetailsDto>(apiUrl);

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrustFullDetailsByUkPrn::Exception - {Message}", ex.Message);
			throw;
		}
	}

	//// Public method, so can write unit tests !!!!
	public string BuildTrustSearchRequestUri(TrustSearch trustSearch)
	{
		var queryParams = HttpUtility.ParseQueryString(string.Empty);

		if (!string.IsNullOrEmpty(trustSearch.GroupName))
		{
			queryParams.Add("groupName", trustSearch.GroupName);
		}
		if (!string.IsNullOrEmpty(trustSearch.Ukprn))
		{
			queryParams.Add("ukprn", trustSearch.Ukprn);
		}
		if (!string.IsNullOrEmpty(trustSearch.CompaniesHouseNumber))
		{
			queryParams.Add("companiesHouseNumber", trustSearch.CompaniesHouseNumber);
		}

		queryParams.Add("page", trustSearch.Page.ToString());

		return HttpUtility.UrlEncode(queryParams.ToString());
	}
}
