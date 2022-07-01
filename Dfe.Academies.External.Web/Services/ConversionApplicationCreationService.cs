using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
    private readonly ILogger<ConversionApplicationCreationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public ConversionApplicationCreationService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationCreationService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
    {
        ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        // TODO await API response !
        // application.Id = 

        return application;
    }

    public Task UpdateDraftApplication(ConversionApplication application)
    {
        ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        // TODO await API response !

        return Task.CompletedTask;
    }
}