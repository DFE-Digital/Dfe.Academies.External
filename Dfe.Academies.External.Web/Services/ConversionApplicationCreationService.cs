using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
    private readonly ILogger<ConversionApplicationCreationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ResilientRequestProvider _resilientRequestProvider;

    public ConversionApplicationCreationService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationCreationService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _resilientRequestProvider = new ResilientRequestProvider(httpClientFactory.CreateClient(AcademisationAPIHttpClientName));
    }

	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
    {
        // TODO: await API response from Academisation API
        // await _resilientRequestProvider.PostAsync<>();
        application.Id = int.MaxValue;

        return application;
    }

    public Task UpdateDraftApplication(ConversionApplication application)
    {
        // TODO: await API response from Academisation API
        //await _resilientRequestProvider.PutAsync<>();

        return Task.CompletedTask;
    }

    public async Task AddSchoolToApplication(int applicationId, int schoolUkUrn, string name)
    {
	    try
	    {
		    ConversionApplicationApiPostResult result;
		    var httpClient = _httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		    SchoolApplyingToConvert school = new(name, schoolUkUrn, applicationId,null);
		    string apiurl = $"{httpClient.BaseAddress}/ConversionApplication/V1/AddSchool/";
            
            // TODO: await API response from Academisation API
            result = await _resilientRequestProvider.PostAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, school);
	    }
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
            throw;
	    }
    }
}