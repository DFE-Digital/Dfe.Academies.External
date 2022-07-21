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

        // TODO: await API response from Academisation API
        // await apiRequestProvider.PostAsync<>();
        application.Id = int.MaxValue;

        return application;
    }

    public Task UpdateDraftApplication(ConversionApplication application)
    {
        ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        // TODO: await API response from Academisation API
        //await apiRequestProvider.PutAsync<>();

        return Task.CompletedTask;
    }

    public Task AddSchoolToApplication(int applicationId, int schoolUkUrn)
    {
	    ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        // TODO: await API response from Academisation API
        // await apiRequestProvider.PostAsync<>();


        return Task.CompletedTask;
    }
}