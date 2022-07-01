using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class AcademisationCreationService : BaseService, IAcademisationCreationService
{
    private readonly ILogger<AcademisationCreationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public AcademisationCreationService(IHttpClientFactory httpClientFactory, ILogger<AcademisationCreationService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
    {
        ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        // TODO: await API response from Academisation API
        // application.Id = 

        return application;
    }
}