using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
    private readonly ILogger<ConversionApplicationCreationService> _logger;
    private readonly HttpClient _httpClient;
    private readonly ResilientRequestProvider _resilientRequestProvider;
    private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

	public ConversionApplicationCreationService(IHttpClientFactory httpClientFactory, 
												ILogger<ConversionApplicationCreationService> logger,
												IConversionApplicationRetrievalService conversionApplicationRetrievalService) : base(httpClientFactory)
    {
	    _httpClient = httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		_logger = logger;
		_resilientRequestProvider = new ResilientRequestProvider(_httpClient);
		_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
    }

	///<inheritdoc/>
	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
    {
	    try
	    {
		    // guard clause - CANNOT create an application without a contributor
		    if (!application.Contributors.Any())
		    {
			    throw new ArgumentException("Mandatory Contributor Missing");
		    }

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/?api-version=V1";
			CreateApplicationApiModel createApplicationApiModel;

			// Push data into Academisation API
			// JsonSerializerOptions = done within _resilientRequestProvider
			var contributor = application.Contributors.FirstOrDefault();
			
			createApplicationApiModel =
				new(application.ApplicationType.ToString(),
					new ApplicationContributorApiModel(contributor.FirstName,
						contributor.LastName,
						contributor.EmailAddress,
						contributor.Role.ToString(),
						contributor.OtherRoleName)
					);
			
			var result = await _resilientRequestProvider.PostAsync<ConversionApplication, CreateApplicationApiModel>(apiurl, createApplicationApiModel);

		    return result;
		}
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::CreateNewApplication::Exception - {Message}", ex.Message);
		    throw;
		}
    }
	
	///<inheritdoc/>
	public async Task AddSchoolToApplication(int applicationId, int schoolUrn, string name)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			SchoolApplyingToConvert school = new(name, schoolUrn, null);
			application.Schools.Add(school);

			//// structure of JSON in body is having a 'contributors' prop - same as ConversionApplication() obj
			// MR:- no response from Academies API - Just an OK
			var result = await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
            throw;
	    }
    }

	///<inheritdoc/>
	public async Task ApplicationAddJoinTrustReasons(int applicationId, string applicationJoinTrustReason, int schoolUrn)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// application can contain multiple schools so need to grab one being changed via linqage
			//var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			//schoolUpdating.ApplicationJoinTrustReason = applicationJoinTrustReason;

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
	    }
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationAddJoinTrustReasons::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

	///<inheritdoc/>
	public async Task AddTrustToApplication(int applicationId, int trustUkPrn, string name)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// TODO MR:- add to existing / form new route to think about here !
			// application.Trust = new trust();

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::AddTrustToApplication::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

	///<inheritdoc/>
	public async Task ApplicationChangeSchoolNameAndReason(ConversionApplication application, SelectOption changeName,
	    string changeSchoolNameReason, int schoolUrn)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{application.ApplicationId}?api-version=V1";

			// application can contain multiple schools so need to grab one being changed via linqage
			var schoolUpdating = application.Schools.FirstOrDefault( s=> s.URN == schoolUrn);
			//schoolUpdating.ProposedNewSchoolName
			//schoolUpdating.ChangeSchoolNameReason = changeSchoolNameReason

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationChangeSchoolNameAndReason::Exception - {Message}", ex.Message);
		    throw;
	    }
    }

	///<inheritdoc/>
	public async Task ApplicationSchoolTargetConversionDate(int applicationId, int schoolUrn, DateTime targetDate,
		string targetDateExplained)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			// application can contain multiple schools so need to grab one being changed via linqage
			var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);

			if (schoolUpdating == null)
			{
				throw new ArgumentException("School not found");
			}

			schoolUpdating.SchoolConversionTargetDate = targetDate;
			schoolUpdating.SchoolConversionTargetDateExplained = targetDateExplained;

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// MR:- no response from Academies API - Just an OK
			var result = await _resilientRequestProvider.PutAsync(apiurl, application);
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::ApplicationSchoolTargetConversionDate::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task ApplicationSchoolFuturePupilNumbers(int applicationId, int schoolUrn, int projectedPupilNumbersYear1,
	    int projectedPupilNumbersYear2, int projectedPupilNumbersYear3, string schoolCapacityAssumptions,
	    int schoolCapacityPublishedAdmissionsNumber)
    {
	    try
	    {
		    var application = await GetApplication(applicationId);

		    if (application.ApplicationId != applicationId)
		    {
			    throw new ArgumentException("Application not found");
		    }

		    var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			
			if (schoolUpdating == null)
			{
				throw new ArgumentException("School not found");
			}
			
			schoolUpdating.ProjectedPupilNumbersYear1 = projectedPupilNumbersYear1;
			schoolUpdating.ProjectedPupilNumbersYear2 = projectedPupilNumbersYear2;
			schoolUpdating.ProjectedPupilNumbersYear3 = projectedPupilNumbersYear3;
			schoolUpdating.SchoolCapacityAssumptions = schoolCapacityAssumptions;
			schoolUpdating.SchoolCapacityPublishedAdmissionsNumber = schoolCapacityPublishedAdmissionsNumber;

			string apiUrl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";
			await _resilientRequestProvider.PutAsync<ConversionApplication>(apiUrl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationSchoolFuturePupilNumbers::Exception - {Message}", ex.Message);
		    throw;
	    }
	}

	///<inheritdoc/>
	public async Task ApplicationSchoolContacts(ApplicationSchoolContacts schoolContacts, int applicationId)
    {
	    try
	    {
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// application can contain multiple schools so need to grab one being changed via linqage
			//var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolContacts.Urn);
			//schoolUpdating.SchoolConversionContactHeadName = schoolContacts.SchoolConversionContactHeadName;
			//schoolUpdating.SchoolConversionContactHeadEmail = schoolContacts.SchoolConversionContactHeadEmail;
			//schoolUpdating.SchoolConversionContactHeadTel = schoolContacts.SchoolConversionContactHeadTel;
			//schoolUpdating.SchoolConversionContactChairName = schoolContacts.SchoolConversionContactChairName;
			//schoolUpdating.SchoolConversionContactChairEmail = schoolContacts.SchoolConversionContactChairEmail;
			// ETC.....

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<SchoolApplyingToConvert>(apiurl, application);
		}
		catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationCreationService::ApplicationSchoolContacts::Exception - {Message}", ex.Message);
		    throw;
	    }
	}

	///<inheritdoc/>
	public async Task ApplicationSchoolLandAndBuildings(SchoolLandAndBuildings schoolLandAndBuildings, int applicationId, int schoolUrn)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			var school = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
			if (school == null)
			{
				throw new ArgumentException("School not found");
			}
			
			application.Schools.First(s => s.URN == schoolUrn).LandAndBuildings = schoolLandAndBuildings;
			
			await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::ApplicationSchoolLandAndBuildings::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task ApplicationPreOpeningSupportGrantUpdate(PayFundsTo schoolSupportGrantFundsPaidTo, int applicationId)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			//string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// application can contain multiple schools so need to grab one being changed via linqage
			//var schoolUpdating = application.Schools.FirstOrDefault(s => s.URN == schoolLandAndBuildings.Urn);

			if (schoolSupportGrantFundsPaidTo == PayFundsTo.School)
			{
				//schoolUpdating.SchoolBuildLandOwnerExplained = schoolLandAndBuildings.SchoolBuildLandOwnerExplained;
			}
			else
			{
				//schoolUpdating.SchoolBuildLandOwnerExplained = schoolLandAndBuildings.SchoolBuildLandOwnerExplained;	
			}

			// TODO: wire up Academisation API / what object does a PUT return
			// var result = await _resilientRequestProvider.PutAsync<SchoolApplyingToConvert>(apiurl, application);
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::ApplicationPreOpeningSupportGrantUpdate::Exception - {Message}", ex.Message);
			throw;
		}
	}

	private async Task<ConversionApplication> GetApplication(int applicationId)
	{
		return await _conversionApplicationRetrievalService.GetApplication(applicationId);
	}

	/////<inheritdoc/>
	//public async Task UpdateDraftApplication(ConversionApplication application)
	//   {
	//    try
	//    {
	//	    // MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
	//	    // to grab current application data
	//	    // before then patching ConversionApplication returned with data from application object

	//	    //https://academies-academisation-api-dev.azurewebsites.net/application/99
	//	    string apiurl = $"{_httpClient.BaseAddress}application/{application.ApplicationId}?api-version=V1";

	//	    // var result = await _resilientRequestProvider.PutAsync<ConversionApplication>(apiurl, application);
	//    }
	//    catch (Exception ex)
	//    {
	//	    _logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}", ex.Message);
	//	    throw;
	//    }
	//}
}
