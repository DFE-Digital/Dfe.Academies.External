using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Services;

public sealed class ReferenceDataRetrievalService : BaseService, IReferenceDataRetrievalService
{
	private readonly ILogger<ReferenceDataRetrievalService> _logger;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ResilientRequestProvider _resilientRequestProvider;

	public ReferenceDataRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ReferenceDataRetrievalService> logger) : base(httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
		_logger = logger;
		_resilientRequestProvider = new ResilientRequestProvider(httpClientFactory.CreateClient(HttpClientName));
	}

	public async Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch)
	{
		IList<SchoolSearchResultViewModel> schools = new List<SchoolSearchResultViewModel>();

		// TODO: Get data from Academisation API
		//// _resilientRequestProvider.Get();
		/// API returns list<SchoolsSearchDto>

		// **** Mock Demo Data - as per Figma - to be removed ! ****
		IList<SchoolsSearchDto> schoolsSearchDtos = new List<SchoolsSearchDto>();
		schoolsSearchDtos.Add(new SchoolsSearchDto("Wise Owl primary school", 587634 , "21 test road", "sheffield", "S1 2JF"));
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
}