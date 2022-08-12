using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
    private readonly ILogger<ConversionApplicationCreationService> _logger;
    private readonly HttpClient _httpClient;
    private readonly ResilientRequestProvider _resilientRequestProvider;

	public ConversionApplicationCreationService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationCreationService> logger) : base(httpClientFactory)
    {
	    _httpClient = httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		_logger = logger;
		_resilientRequestProvider = new ResilientRequestProvider(_httpClient);
	}

	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
    {
		//https://academies-academisation-api-dev.azurewebsites.net/application/99
		string apiurl = $"{_httpClient.BaseAddress}/application/?api-version=V1";

		// TODO: wire up Academisation API
		// var result = await _resilientRequestProvider.PostAsync<ConversionApplicationApiPostResult, ConversionApplication>(apiurl, application);
		application.ApplicationId = int.MaxValue; // result.ApplicationId;

        return application;
    }

    public async Task UpdateDraftApplication(ConversionApplication application)
    {
		// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
		// to grab current application data
		// before then patching ConversionApplication returned with data from application object

		//https://academies-academisation-api-dev.azurewebsites.net/application/99
		string apiurl = $"{_httpClient.BaseAddress}/application/{application.ApplicationId}?api-version=V1";

		// TODO: wire up Academisation API / what object does a PUT return
		// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, ConversionApplication>(apiurl, application);
    }

    public async Task AddSchoolToApplication(int applicationId, int schoolUrn, string name)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}/application/{applicationId}?api-version=V1";

			SchoolApplyingToConvert school = new(name, schoolUrn, applicationId,null);

			//application.Schools.Add(school);

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
            throw;
	    }
    }

    public async Task ApplicationAddJoinTrustReasons(int applicationId, string applicationJoinTrustReason, int schoolUrn)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}/application/{applicationId}?api-version=V1";

			// application can contain multiple schools so need to grab one being changed via linqage
			//var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			//schoolUpdating.ApplicationJoinTrustReason = applicationJoinTrustReason;

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, ConversionApplication>(apiurl, application);
	    }
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationAddJoinTrustReasons::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

    public async Task AddTrustToApplication(int applicationId, int trustUkPrn, string name)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}/application/{applicationId}?api-version=V1";

			// TODO MR:- add to existing / form new route to think about here !
			// application.Trust = new trust();

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddTrustToApplication::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

    public async Task ApplicationChangeSchoolNameAndReason(ConversionApplication application, SelectOption changeName,
	    string changeSchoolNameReason, int schoolUrn)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			// application can contain multiple schools so need to grab one being changed via linqage
			var schoolUpdating = application.Schools.FirstOrDefault( s=> s.URN == schoolUrn);
			//schoolUpdating.ProposedNewSchoolName
			//schoolUpdating.ChangeSchoolNameReason = changeSchoolNameReason

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationChangeSchoolNameAndReason::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

    public async Task ApplicationSchoolTargetConversionDate(int applicationId, int schoolUrn, DateTime targetDate,
	    string targetDateExplained)
    {
	    try
	    {
		    // MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
		    // to grab current application data
		    // before then patching ConversionApplication returned with data from application object

		    // application can contain multiple schools so need to grab one being changed via linqage
		    //var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			//schoolUpdating.SchoolConversionTargetDate = targetDate
			//schoolUpdating.SchoolConversionTargetDateExplained = targetDateExplained

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationSchoolTargetConversionDate::Exception - {Message}", ex.Message);
		    throw;
	    }
	}

    public async Task ApplicationSchoolFuturePupilNumbers(int applicationId, int schoolUrn, int projectedPupilNumbersYear1,
	    int projectedPupilNumbersYear2, int projectedPupilNumbersYear3, string schoolCapacityAssumptions,
	    int schoolCapacityPublishedAdmissionsNumber)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			// application can contain multiple schools so need to grab one being changed via linqage
			//var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			//schoolUpdating.ProjectedPupilNumbersYear1 = projectedPupilNumbersYear1
			//schoolUpdating.ProjectedPupilNumbersYear2 = projectedPupilNumbersYear2
			//schoolUpdating.ProjectedPupilNumbersYear3 = projectedPupilNumbersYear3
			//schoolUpdating.SchoolCapacityAssumptions = schoolCapacityAssumptions
			//schoolUpdating.SchoolCapacityPublishedAdmissionsNumber = schoolCapacityPublishedAdmissionsNumber

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplicationApiPostResult, SchoolApplyingToConvert>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationSchoolFuturePupilNumbers::Exception - {Message}", ex.Message);
		    throw;
	    }
	}
}