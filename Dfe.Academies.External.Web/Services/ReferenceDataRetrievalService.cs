using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Schools;
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
			string apiurl = $"{_httpClient.BaseAddress}/establishments?{BuildSchoolSearchRequestUri(schoolSearch, "V1")}";

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
			string apiurl = $"{_httpClient.BaseAddress}/establishment/urn/{urn}?api-version=V1";

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
}