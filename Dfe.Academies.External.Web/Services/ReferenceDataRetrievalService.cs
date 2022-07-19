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

    public Task<IEnumerable<string>> SearchSchools(string inputText)
	{
		// TODO: Get data from Academisation API
		//// _resilientRequestProvider.Get();
		
		// **** Mock Demo Data - as per Figma ****
		// TODO MR:- have a list<> of 3 schools. wise owl as per figma

		throw new NotImplementedException();
	}
}