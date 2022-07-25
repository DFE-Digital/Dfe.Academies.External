using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
    private readonly ILogger<ConversionApplicationCreationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string BaseUrl = "http://127.0.0.1:8000/";

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

    public async Task AddSchoolToApplication(int applicationId, int schoolUkUrn, string name)
    {
	    const string apiurl = $"{BaseUrl}/ConversionApplication/V1/AddSchool/";

	    try
	    {
		    ConversionApplicationApiPostResult result;
            ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));
		    SchoolApplyingToConvert school = new(name, schoolUkUrn, applicationId,null);

			// TODO: await API response from Academisation API
			result = await apiRequestProvider.PostAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, school);
	    }
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
            throw;
	    }
    }
}