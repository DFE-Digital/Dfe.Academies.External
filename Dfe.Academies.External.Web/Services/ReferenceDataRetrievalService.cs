using System.Web;
using Dfe.Academies.Contracts.V4;
using Dfe.Academies.Contracts.V4.Trusts;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
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
			string apiurl = $"{HttpClient.BaseAddress}V4/establishments?{BuildSchoolSearchRequestUri(schoolSearch)}";

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
			string apiurl = $"{HttpClient.BaseAddress}V4/establishment/urn/{urn}";

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
	public string BuildSchoolSearchRequestUri(SchoolSearch schoolSearch)
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

		//queryParams.Add("api-version", apiVersionNumber);

		return HttpUtility.UrlEncode(queryParams.ToString());
	}

	///<inheritdoc/>
	public async Task<PagedDataResponse<TrustDto>> GetTrusts(TrustSearch trustSearch)
	{
		try
		{
			string apiurl = $"{HttpClient.BaseAddress}V4/trusts?{BuildTrustSearchRequestUri(trustSearch)}";
			var APIresult = await _resilientRequestProvider.GetAsync<PagedDataResponse<TrustDto>>(apiurl);

			return APIresult;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrusts::Exception - {Message}", ex.Message);
			throw;
		}
	}

	public async Task<TrustDto> GetTrustByUkPrn(string ukPrn)
	{
		try
		{
			string apiUrl = $"{HttpClient.BaseAddress}V4/trust/{ukPrn}";


			var result = await _resilientRequestProvider.GetAsync<TrustDto>(apiUrl);

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
