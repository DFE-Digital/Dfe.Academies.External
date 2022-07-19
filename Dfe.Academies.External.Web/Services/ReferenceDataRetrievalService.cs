using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Services;

public sealed class ReferenceDataRetrievalService : BaseService, IReferenceDataRetrievalService
{
	private readonly ILogger<ConversionApplicationRetrievalService> _logger;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ResilientRequestProvider _resilientRequestProvider;

	public ReferenceDataRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationRetrievalService> logger) : base(httpClientFactory)
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
		/// API retuns list<SchoolsSearchDto>

		// **** Mock Demo Data - as per Figma ****
		// TODO MR:- have a list<> of 3 schools. wise owl as per figma

		// Map SchoolsSearchDto to view model

		return schools;
	}
}