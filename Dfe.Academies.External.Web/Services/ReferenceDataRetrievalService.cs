using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Base;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;
using Dfe.Academies.External.Web.ViewModels;
using System.Web;

namespace Dfe.Academies.External.Web.Services;

public sealed class ReferenceDataRetrievalService : BaseService, IReferenceDataRetrievalService
{
	private readonly ILogger<ReferenceDataRetrievalService> _logger;
	private readonly HttpClient _httpClient;
	private readonly ResilientRequestProvider _resilientRequestProvider;

	public ReferenceDataRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ReferenceDataRetrievalService> logger) : base(httpClientFactory)
	{
		_logger = logger;
		_httpClient = httpClientFactory.CreateClient(AcademiesAPIHttpClientName);
		_resilientRequestProvider = new ResilientRequestProvider(_httpClient);
	}

	///<inheritdoc/>
	public async Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch)
	{
		try
		{
			//{{api-host}}/establishments?api-version=V1&Urn=101934&ukprn=10006563&Name=wise
			// TODO MR:- buildURIfunc
			string apiurl = $"{_httpClient.BaseAddress}/establishments?api-version=V1";

			IList<SchoolSearchResultViewModel> schools = new List<SchoolSearchResultViewModel>();

			// TODO: Get data from Academisation API - returns ApiListWrapper<??>
			// var APIresult = await _resilientRequestProvider.GetAsync<EstablishmentResponse>(apiurl);
			/// API returns list<SchoolsSearchDto>

			// **** Mock Demo Data - as per Figma - to be removed ! ****
			IList<SchoolsSearchDto> schoolsSearchDtos = new List<SchoolsSearchDto>();
			schoolsSearchDtos.Add(new SchoolsSearchDto("Wise Owl primary school", 587634,"", "21 test road", "sheffield", "S1 2JF"));
			schoolsSearchDtos.Add(new SchoolsSearchDto("Wise Owl secondary school", 368489,"", "21 test road", "sheffield", "S1 2JF"));

			// do a bit of manual linqage
			IEnumerable<SchoolsSearchDto> schoolsSearchResults = new List<SchoolsSearchDto>();
			if (!string.IsNullOrWhiteSpace(schoolSearch.Name))
			{
				schoolsSearchResults =
					schoolsSearchDtos.Where(s => s.Name.ToLower().Trim().Contains(schoolSearch.Name)
					                             || s.Name.ToLower().Trim().EndsWith(schoolSearch.Name)).ToList();
			}
			else if (!string.IsNullOrWhiteSpace(schoolSearch.Urn) && !schoolsSearchResults.Any())
			{
				schoolsSearchResults =
					schoolsSearchDtos.Where(s => s.Urn == int.Parse(schoolSearch.Urn.ToLower().Trim())).ToList();
			}

			// Map SchoolsSearchDto to view model
			if (schoolsSearchResults.Any())
				schools = schoolsSearchResults.Select(c =>
					new SchoolSearchResultViewModel(schoolName: c.Name, urn: c.Urn, street: c.Street, town: c.Town, fullUkPostcode: c.FullUkPostcode)
					{
						// TODO MR:- others?? depends what we get back from API
					}).ToList();

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
			EstablishmentResponse result;

			// {{api-host}}/establishment/urn/101934?api-version=V1
			string apiurl = $"{_httpClient.BaseAddress}/establishment/urn/{urn}?api-version=V1";

			// TODO: Get data from Academisation API - returns EstablishmentResponse
			// APIresult = await _resilientRequestProvider.GetAsync<EstablishmentResponse>(apiurl);

			// **** Mock Demo Data - as per Figma ****
			if (urn == 587634)
			{
				result = new(name: "Wise owl primary school", urn: urn, ukprn: "GAT00123", "94 Forest Road", "Manchester", "MC4 3TR");
			}
			else if (urn == 368489)
			{
				result = new(name: "Wise owl secondary school", urn: urn, ukprn: "GAT00124", "99 Forest Road", "Manchester", "MC4 3TR");
			}
			else
			{
				result = new(name: "Chesterton primary school", urn: 101003, ukprn: null, "94 Forest Road", "stoke-on-trent", "ST4 3TR");
			}

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetSchool::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task<ApiListWrapper<TrustSearchDto>> GetTrustsByPagination(TrustSearch trustSearch)
	{
		try
		{
			ApiListWrapper<TrustSearchDto> result = null;
			string apiurl = $"{_httpClient.BaseAddress}/V1/trusts?{BuildTrustSearchRequestUri(trustSearch)}";

			// TODO MR:- api endpoint to build will look like this:-
			// {{api-host}}/trusts?api-version=V1&groupName=grammar

			// TODO: Get data from Academisation API - returns ApiListWrapper<TrustSearchDto>
			// var APIresult = await _resilientRequestProvider.GetAsync<ApiListWrapper<TrustSearchDto>>(apiurl);

			// **** Mock Demo Data - as per Figma ****
			//result.Data.Add(new TrustSearchDto());

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrustsByPagination::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task<TrustDetailsDto> GetTrustByUkPrn(string ukPrn)
	{
		try
		{
			TrustDetailsDto result = null;

			// MR:- api endpoint to build will look like this:-
			// {{api-host}}/trust/10058464?api-version=V1
			string apiurl = $"{_httpClient.BaseAddress}/trust/{ukPrn}?api-version=V1";

			// TODO: Get data from Academisation API - returns ApiWrapper<TrustDetailsDto>
			// var APIresult = await _resilientRequestProvider.GetAsync<ApiWrapper<TrustDetailsDto>>(apiurl);

			// **** Mock Demo Data - as per Figma ****

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrustByUkPrn::Exception - {Message}", ex.Message);
			throw;
		}
	}

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