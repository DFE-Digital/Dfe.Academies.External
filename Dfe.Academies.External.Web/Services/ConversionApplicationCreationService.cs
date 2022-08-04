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
        ResilientRequestProvider apiRequestProvider = new (_httpClientFactory.CreateClient(AcademisationAPIHttpClientName));

        // TODO: wire up Academisation API
        // await apiRequestProvider.PostAsync<>();
        application.Id = int.MaxValue;

        return application;
    }

    public Task UpdateDraftApplication(ConversionApplication application)
    {
        ResilientRequestProvider apiRequestProvider = new (_httpClientFactory.CreateClient(AcademisationAPIHttpClientName));

        // TODO: wire up Academisation API
        //await apiRequestProvider.PutAsync<>();

        return Task.CompletedTask;
    }

    public async Task AddSchoolToApplication(int applicationId, int schoolUkUrn, string name)
    {
	    try
	    {
            ResilientRequestProvider apiRequestProvider = new (_httpClientFactory.CreateClient(AcademisationAPIHttpClientName));
		    SchoolApplyingToConvert school = new(name, schoolUkUrn, applicationId,null);

            // TODO: wire up Academisation API
            // var result = await apiRequestProvider.PostAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, school);
        }
        catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
            throw;
	    }
    }

    public async Task ApplicationAddJoinTrustReasons(ConversionApplication application, string applicationJoinTrustReason)
    {
	    try
	    {
		    ResilientRequestProvider apiRequestProvider = new(_httpClientFactory.CreateClient(AcademisationAPIHttpClientName));

		    // TODO: wire up Academisation API
		    // var result = await apiRequestProvider.PostAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, school);
	    }
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationAddJoinTrustReasons::Exception - {Message}", ex.Message);
		    throw;
	    }
    }
}