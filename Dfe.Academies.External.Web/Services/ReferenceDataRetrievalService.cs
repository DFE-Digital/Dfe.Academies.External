using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;
using Dfe.Academies.External.Web.Models;
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

	public async Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch)
	{
		IList<SchoolSearchResultViewModel> schools = new List<SchoolSearchResultViewModel>();

		// TODO: Get data from Academisation API
		//// _resilientRequestProvider.Get();
		/// API returns list<SchoolsSearchDto>

		// **** Mock Demo Data - as per Figma - to be removed ! ****
		IList<SchoolsSearchDto> schoolsSearchDtos = new List<SchoolsSearchDto>();
		schoolsSearchDtos.Add(new SchoolsSearchDto("Wise Owl primary school", 587634, "21 test road", "sheffield", "S1 2JF"));
		schoolsSearchDtos.Add(new SchoolsSearchDto("Wise Owl secondary school", 368489, "21 test road", "sheffield", "S1 2JF"));

		// do a bit of manual linqage
		IEnumerable<SchoolsSearchDto> schoolsSearchResults = new List<SchoolsSearchDto>();
		if (!string.IsNullOrWhiteSpace(schoolSearch.SchoolName))
		{
			schoolsSearchResults =
				schoolsSearchDtos.Where(s => s.SchoolName.ToLower().Trim().Contains(schoolSearch.SchoolName)
														|| s.SchoolName.ToLower().Trim().EndsWith(schoolSearch.SchoolName)).ToList();
		}
		else if (!string.IsNullOrWhiteSpace(schoolSearch.Urn) && !schoolsSearchResults.Any())
		{
			schoolsSearchResults =
				schoolsSearchDtos.Where(s => s.Urn == int.Parse(schoolSearch.Urn.ToLower().Trim())).ToList();
		}

		// Map SchoolsSearchDto to view model
		if (schoolsSearchResults.Any())
			schools = schoolsSearchResults.Select(c =>
				new SchoolSearchResultViewModel(schoolName: c.SchoolName, urn: c.Urn, street: c.Street, town: c.Town, fullUkPostcode: c.FullUkPostcode)
				{
					// TODO MR:- others?? depends what we get back from API
				}).ToList();

		return schools;
	}

	public async Task<EstablishmentResponse> GetSchool(int urn)
	{
		// TODO: Get data from Academisation API
		// _resilientRequestProvider.Get

		// **** Mock Demo Data - as per Figma ****
		if (urn == 587634)
		{
			return new(name: "Wise owl primary school", urn: urn, ukprn: "GAT00123", "94 Forest Road", "Manchester", "MC4 3TR");
		}
		else if (urn == 368489)
		{
			return new(name: "Wise owl secondary school", urn: urn, ukprn: "GAT00124", "99 Forest Road", "Manchester", "MC4 3TR");
		}
		else
		{
			return new(name: "Chesterton primary school", urn: 101003, ukprn: null, "94 Forest Road", "stoke-on-trent", "ST4 3TR");
		}
	}

	///<inheritdoc/>
	public async Task<List<TrustSearchDto>> GetTrusts(TrustSearch trustSearch)
	{
		try
		{
			// {{api-host}}/trusts?api-version=V1&groupName=grammar
			string apiurl = $"{_httpClient.BaseAddress}/trusts?{BuildTrustSearchRequestUri(trustSearch)}&api-version=V1";
			
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
	public async Task<TrustDetailsDto> GetTrustByUkPrn(string ukPrn)
	{
		try
		{
			// MR:- api endpoint to build will look like this:-
			// {{api-host}}/trust/10058464?api-version=V1
			string apiurl = $"{_httpClient.BaseAddress}/trust/{ukPrn}?api-version=V1";

			// API - returns ApiWrapper<TrustDetailsDto>
			var APIresult = await _resilientRequestProvider.GetAsync<TrustDetailsDto>(apiurl);
			
			return APIresult;
		}
		catch (Exception ex)
		{
			_logger.LogError("ReferenceDataRetrievalService::GetTrustByUkPrn::Exception - {Message}", ex.Message);
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