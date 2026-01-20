using System.Globalization;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.FeatureManagement;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.ViewModels;
using Dfe.Academisation.CorrelationIdMiddleware;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationRetrievalService : BaseService, IConversionApplicationRetrievalService
{
	private readonly ILogger<ConversionApplicationRetrievalService> _logger;
	private readonly ResilientRequestProvider _resilientRequestProvider;
	private readonly IFileUploadService _fileUploadService;
	private readonly IConversionGrantExpiryFeature _conversionGrantExpiryFeature;
	public ConversionApplicationRetrievalService(IHttpClientFactory httpClientFactory, 
		ILogger<ConversionApplicationRetrievalService> logger, 
		IFileUploadService fileUploadService,
		ICorrelationContext correlationContext,
		IConversionGrantExpiryFeature conversionGrantExpiryFeature) : base(httpClientFactory, correlationContext, AcademisationAPIHttpClientName)
	{
		_logger = logger;
		_fileUploadService = fileUploadService;
		_resilientRequestProvider = new ResilientRequestProvider(HttpClient, _logger);
		_conversionGrantExpiryFeature = conversionGrantExpiryFeature;
	}

    // Updated the logging statements in catch blocks to pass the caught exception as a parameter.

    public async Task<List<ConversionApplication>> GetCompletedApplications(string? email)
    {
        try
        {
            string apiurl = $"{HttpClient.BaseAddress}application/contributor/{email}?api-version=V1";

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                            }
            };

            // Get data from Academisation API
            var existingApplications = await _resilientRequestProvider.GetAsync<List<ConversionApplication>>(apiurl, options);

            return existingApplications.Where(a => a.ApplicationStatus == ApplicationStatus.Submitted).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConversionApplicationRetrievalService::GetCompletedApplications::Exception - {Message}", ex.Message);
            return new List<ConversionApplication>();
        }
    }

	///<inheritdoc/>
	  public async Task<List<ConversionApplication>> GetPendingApplications(string? email)
    {
        try
        {
            string apiurl = $"{HttpClient.BaseAddress}application/contributor/{email}?api-version=V1";

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                            }
            };

            // Get data from Academisation API
            var existingApplications = await _resilientRequestProvider.GetAsync<List<ConversionApplication>>(apiurl, options);

            return existingApplications.Where(a => a.ApplicationStatus == ApplicationStatus.InProgress).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConversionApplicationRetrievalService::GetPendingApplications::Exception - {Message}", ex.Message);
            return new List<ConversionApplication>();
        }
    }

	///<inheritdoc/>
	  public async Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(int applicationId)
    {
        try
        {
            DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat;
            List<ConversionApplicationAuditEntry> auditEntries = new()
                        {
                            new(createdBy:"Phillip Frond", typeOfChange: "change", entityChanged: "Application", propertyChanged: "school")
                                {Id = 99, ApplicationId = applicationId, DateCreated = Convert.ToDateTime("25/05/2022", dtfi)},
                            new(createdBy: "Peter Parker", typeOfChange: "change", entityChanged: "Application", propertyChanged: "trust")
                                {Id = 98, ApplicationId = applicationId, DateCreated = Convert.ToDateTime("20/05/2022", dtfi)},
                            new(createdBy: "Richard Dickenson", typeOfChange: "add", entityChanged: "Application", propertyChanged: "started")
                                {Id = 97, ApplicationId = applicationId, DateCreated = Convert.ToDateTime("15/05/2022", dtfi)},
                        };

            return auditEntries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConversionApplicationRetrievalService::GetConversionApplicationAuditEntries::Exception - {Message}", ex.Message);
            return new List<ConversionApplicationAuditEntry>();
        }
    }

	///<inheritdoc/>
	  public async Task<List<ApplicationComponentViewModel>> GetSchoolApplicationComponents(int applicationId, int schoolId)
    {
        try
        {
            var application = await GetApplication(applicationId);

            if (application?.ApplicationId != applicationId)
            {
                throw new ArgumentException("Application not found");
            }

            var school = application.Schools.FirstOrDefault(s => s.URN == schoolId);
            if (school == null)
            {
                throw new ArgumentException("School not found");
            }

            List<ApplicationComponentViewModel> conversionApplicationComponents = new()
                        {
                            new("About the conversion", UriFormatter.SetSchoolApplicationComponentUriFromName("About the conversion"), CalculateAboutTheConversionSectionStatus(school)),
                            new("Further information", UriFormatter.SetSchoolApplicationComponentUriFromName("Further information"), CalculateFurtherInformationSectionStatus(school, application.ApplicationReference)),
                            new("Finances", UriFormatter.SetSchoolApplicationComponentUriFromName("Finances"), CalculateFinanceSectionStatus(school)),
                            new("Future pupil numbers", UriFormatter.SetSchoolApplicationComponentUriFromName("Future pupil numbers"), CalculateFuturePupilNumbersSectionStatus(school)),
                            new("Land and buildings", UriFormatter.SetSchoolApplicationComponentUriFromName("Land and buildings"),CalculateLandAndBuildingsSectionStatus(school)),
                            new("Consultation", UriFormatter.SetSchoolApplicationComponentUriFromName("Consultation"),CalculateConsultationSectionStatus(school))
                        };

            var conversionSupportGrantTask = "Conversion support grant";
            if (!await _conversionGrantExpiryFeature.IsEnabledAsync())
            {
                conversionApplicationComponents.Add(new(conversionSupportGrantTask, UriFormatter.SetSchoolApplicationComponentUriFromName(conversionSupportGrantTask), CalculatePreOpeningSupportGrantSectionStatus(school)));
            }
            if (await _conversionGrantExpiryFeature.IsEnabledAsync() && !_conversionGrantExpiryFeature.IsNewApplication(application.CreatedOn))
            {
                conversionApplicationComponents.Add(new(conversionSupportGrantTask, UriFormatter.SetSchoolApplicationComponentUriFromName(conversionSupportGrantTask), CalculatePreOpeningSupportGrantSectionStatus(school)));
            }
            conversionApplicationComponents.Add(new("Declaration", UriFormatter.SetSchoolApplicationComponentUriFromName("Declaration"), CalculateDeclarationSectionStatus(school)));

            return conversionApplicationComponents;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConversionApplicationRetrievalService::GetSchoolApplicationComponents::Exception - {Message}", ex.Message);
            return new List<ApplicationComponentViewModel>();
        }
    }
	
	public async Task<List<ApplicationComponentViewModel>> GetFormAMatTrustComponents(int applicationId)
	{
		try
		{
			var application = await GetApplication(applicationId);

			if (application?.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}
			

			List<ApplicationComponentViewModel> conversionApplicationComponents = new()
			{
				new("Name of the trust", UriFormatter.SetSchoolApplicationComponentUriFromName("About the conversion"), CalculateNameOfTheTrustSectionStatus(application.FormTrustDetails)),
				new("Opening date", UriFormatter.SetSchoolApplicationComponentUriFromName("Further information"), CalculateOpeningDateSectionStatus(application.FormTrustDetails)),
				new("Reasons for forming the trust", UriFormatter.SetSchoolApplicationComponentUriFromName("Finances"), CalculateReasonsForFormingTrustSectionStatus(application.FormTrustDetails)),
				new("Plans for growth", UriFormatter.SetSchoolApplicationComponentUriFromName("Future pupil numbers"), CalculatePlansForGrowthSectionStatus(application.FormTrustDetails)),
				new("School improvement strategy", UriFormatter.SetSchoolApplicationComponentUriFromName("Land and buildings"),CalculateSchoolImprovementStrategyStatus(application.FormTrustDetails)),
				new("Governance structure", UriFormatter.SetSchoolApplicationComponentUriFromName("Land and buildings"),CalculateGovernanceStructureSectionStatus(application)),
				new("Key people", UriFormatter.SetSchoolApplicationComponentUriFromName("Land and buildings"),CalculateKeyPeopleSectionStatus(application.FormTrustDetails))
			};

			return conversionApplicationComponents;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetSchoolApplicationComponents::Exception - {Message}", ex.Message);
			return new List<ApplicationComponentViewModel>();
		}
	}

	private Status CalculateKeyPeopleSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return applicationFormTrustDetails.KeyPeople.Any() ? Status.Completed : Status.NotStarted;
	}

	private Status CalculateGovernanceStructureSectionStatus(ConversionApplication application)
	{
		try
		{
			var result = _fileUploadService.GetFiles(FileUploadConstants.TopLevelApplicationFolderName, application.EntityId.ToString(), application.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName).Result;
			return result.Any() ? Status.Completed : Status.NotStarted;
		}
		catch (Exception e)
		{
			return Status.NotStarted;
		}
	}

	private Status CalculateSchoolImprovementStrategyStatus(NewTrust applicationFormTrustDetails)
	{
		return !string.IsNullOrWhiteSpace(applicationFormTrustDetails.FormTrustImprovementStrategy)
			? Status.Completed
			: Status.NotStarted;
	}

	private Status CalculatePlansForGrowthSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return !string.IsNullOrWhiteSpace(applicationFormTrustDetails.FormTrustPlanForGrowth) || !string.IsNullOrWhiteSpace(applicationFormTrustDetails.FormTrustPlansForNoGrowth)
			? Status.Completed
			: Status.NotStarted;
	}

	private Status CalculateReasonsForFormingTrustSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return !string.IsNullOrWhiteSpace(applicationFormTrustDetails.FormTrustReasonForming)
			? Status.Completed
			: Status.NotStarted;
	}

	///<inheritdoc/>
	public Status CalculateOpeningDateSectionStatus(NewTrust applicationFormTrustDetails)
	{
		// MR:- now have partial completion in this section, so need to check all 3 component parts !
		var part1 = applicationFormTrustDetails.FormTrustOpeningDate.HasValue ? Status.Completed : Status.NotStarted;
		var part2 = !string.IsNullOrWhiteSpace(applicationFormTrustDetails.TrustApproverName) ? Status.Completed : Status.NotStarted;
		var part3 = !string.IsNullOrWhiteSpace(applicationFormTrustDetails.TrustApproverEmail) ? Status.Completed : Status.NotStarted;

		var boolList = new List<bool>
		{
			part1 == Status.Completed,
			part2 == Status.Completed,
			part3 == Status.Completed
		};

		if (boolList.All(x => x))
			return Status.Completed;

		return boolList.All(x => !x) ? Status.NotStarted : Status.InProgress;
	}

	private Status CalculateNameOfTheTrustSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return !string.IsNullOrWhiteSpace(applicationFormTrustDetails.FormTrustProposedNameOfTrust)
			? Status.Completed
			: Status.NotStarted;
	}
	
	///<inheritdoc/>
	public async Task<List<ConversionApplicationContributor>> GetConversionApplicationContributors(int applicationId)
	{
		try
		{
			var application = await GetApplication(applicationId);

			return application?.Contributors ?? new List<ConversionApplicationContributor>();
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetConversionApplicationContributors::Exception - {Message}", ex.Message);
			return new List<ConversionApplicationContributor>();
		}
	}

	///<inheritdoc/>
	public async Task<ConversionApplication?> GetApplication(int applicationId)
	{
		try
		{
			// baseaddress has a backslash at the end to be a valid URI !!!
			// https://academies-academisation-api-dev.azurewebsites.net/application/99
			// endpoint will return 404 if id NOT found !
			string apiurl = $"{HttpClient.BaseAddress}application/{applicationId}?api-version=V1";

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				Converters = {
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				},
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			// Get data from Academisation API
			var conversionApplication = await _resilientRequestProvider.GetAsync<ConversionApplication?>(apiurl, options);

			return conversionApplication;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetApplication::Exception - {Message}", ex.Message);
			return new ConversionApplication();
		}
	}

	public async Task<List<ApplicationSchoolSharepointServiceModel>> GetAllApplications()
	{
		try
		{
			// baseaddress has a backslash at the end to be a valid URI !!!
			// https://academies-academisation-api-dev.azurewebsites.net/application/99
			// endpoint will return 404 if id NOT found !
			string apiurl = $"{HttpClient.BaseAddress}application/all?api-version=V1";

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				Converters = {
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				},
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			// Get data from Academisation API
			var sharepointServiceModels = await _resilientRequestProvider.GetAsync<List<ApplicationSchoolSharepointServiceModel>>(apiurl, options);

			return sharepointServiceModels;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetAllApplications::Exception - {Message}", ex.Message);
			return new ();
		}
	}

	/// <summary>
	/// About the conversion = 4 sections - so could return 'In Progress' or Completed or NotStarted
	/// Same logic in here as summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateAboutTheConversionSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		// need 4 bools to represent each sub-section. completed = yes/no
		var schoolMainContacts = !string.IsNullOrEmpty(selectedSchool.SchoolConversionContactHeadName);
		var applicationConversionTargetDate = selectedSchool.SchoolConversionTargetDateSpecified.HasValue;
		var applicationJoinTrustReasons = !string.IsNullOrWhiteSpace(selectedSchool.ApplicationJoinTrustReason);
		var applicationChangeSchoolName = selectedSchool.ConversionChangeNamePlanned.HasValue;
		var approversFullName = !string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionApproverContactName);
		var approversEmailAddress = !string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionApproverContactEmail);
		var boolList = new List<bool>
		{
			schoolMainContacts,
			applicationConversionTargetDate,
			applicationJoinTrustReasons,
			applicationChangeSchoolName,
			approversFullName,
			approversEmailAddress
		};

		if (boolList.All(x => x))
			return Status.Completed;
		
		return boolList.All(x => !x) ? Status.NotStarted : Status.InProgress;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateFurtherInformationSectionStatus(SchoolApplyingToConvert? selectedSchool, string applicationReference)
	{
		var dioceseFileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, selectedSchool!.EntityId.ToString(),  applicationReference, FileUploadConstants.DioceseFilePrefixFieldName).Result ?? [];
		var foundationConsentFileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, selectedSchool.EntityId.ToString(),  applicationReference, FileUploadConstants.FoundationConsentFilePrefixFieldName).Result ?? [];
		var resolutionConsentFileNames = _fileUploadService.GetFiles(FileUploadConstants.TopLevelSchoolFolderName, selectedSchool.EntityId.ToString(),  applicationReference, FileUploadConstants.ResolutionConsentfilePrefixFieldName).Result ?? [];
		if (!string.IsNullOrEmpty(selectedSchool?.TrustBenefitDetails) &&
		    ((selectedSchool?.DioceseName == null) == (!dioceseFileNames.Any()) &&
		     (selectedSchool?.FoundationTrustOrBodyName == null) == (!foundationConsentFileNames.Any())) &&
		    resolutionConsentFileNames.Any())
			return Status.Completed;

		if (!string.IsNullOrEmpty(selectedSchool?.TrustBenefitDetails) ||
		    resolutionConsentFileNames.Any())
			return Status.InProgress;

		return Status.NotStarted;
	}

	/// <summary>
	/// Finance = 6 sections - so could return 'In Progress' or Completed or NotStarted
	/// Same logic in here as summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateFinanceSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		// need 6 bools to represent each sub-section. completed = yes/no
		bool? previousFinancialYear = selectedSchool.PreviousFinancialYear.FinancialYearEndDate.HasValue ? true : null;
		bool? currentFinancialYear = selectedSchool.CurrentFinancialYear.FinancialYearEndDate.HasValue ? true : null;
		bool? nextFinancialYear = selectedSchool.NextFinancialYear.FinancialYearEndDate.HasValue ? true : null;
		bool? loans = selectedSchool.HasLoans;
		bool? leases = selectedSchool.HasLeases;
		bool? financialInvestigations = selectedSchool.FinanceOngoingInvestigations;

		var validationList = new List<bool?> { previousFinancialYear, currentFinancialYear, nextFinancialYear, loans, leases, financialInvestigations };

		if (validationList.All(x => x.HasValue))
			return Status.Completed;
		
		return validationList.All(x => !x.HasValue) ? Status.NotStarted : Status.InProgress;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as future pupil numbers summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateFuturePupilNumbersSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.ProjectedPupilNumbersYear1.HasValue == false
			? Status.NotStarted
			: Status.Completed;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as Land and buildings summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateLandAndBuildingsSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return !string.IsNullOrWhiteSpace(selectedSchool?.LandAndBuildings.OwnerExplained)
			? Status.Completed
			: Status.NotStarted;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as consultation summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateConsultationSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.SchoolHasConsultedStakeholders.HasValue == false
			? Status.NotStarted
			: Status.Completed;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as Conversion support grant summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculatePreOpeningSupportGrantSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.SchoolSupportGrantFundsPaidTo.HasValue == false
			? Status.NotStarted
			: Status.Completed;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as declaration summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateDeclarationSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		if (selectedSchool?.DeclarationBodyAgree != null)
		{
			return Status.Completed;
		}
		else
		{
			return Status.NotStarted;
		}
	}

	///<inheritdoc/>
	public Status CalculateTrustStatus(ConversionApplication? conversionApplication)
	{
		if (conversionApplication != null)
		{
			switch (conversionApplication.ApplicationType)
			{
				case ApplicationTypes.JoinAMat:
					return CalculateJoinAMatTrustStatus(conversionApplication);
				case ApplicationTypes.FormAMat:
					return CalculateFormAMatTrustStatus(conversionApplication);
				default:
					return Status.NotStarted;
			}
		}

		return Status.NotStarted;
	}
	
	///<inheritdoc/>
	public Status CalculateJoinAMatTrustStatus(ConversionApplication? conversionApplication)
	{
		if (!string.IsNullOrWhiteSpace(conversionApplication?.JoinTrustDetails?.TrustName)
		    && conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue
		    && conversionApplication.JoinTrustDetails.ChangesToLaGovernance.HasValue)
			return Status.Completed;
		
		if ((conversionApplication != null && conversionApplication.JoinTrustDetails != null) && (!string.IsNullOrWhiteSpace(conversionApplication?.JoinTrustDetails?.TrustName)
		    || conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue
		    || conversionApplication.JoinTrustDetails.ChangesToLaGovernance.HasValue))
			return Status.InProgress;

		return Status.NotStarted;
	}

	///<inheritdoc/>
	public Status CalculateFormAMatTrustStatus(ConversionApplication? conversionApplication)
	{
		if (conversionApplication?.FormTrustDetails != null)
		{
			var applicationFormTrustDetails = conversionApplication.FormTrustDetails;

			bool nameOfTrustStatus = CalculateNameOfTheTrustSectionStatus(applicationFormTrustDetails) == Status.Completed;
			bool openingDateStatus = CalculateOpeningDateSectionStatus(applicationFormTrustDetails) == Status.Completed;
			bool trustReasonsStatus = CalculateReasonsForFormingTrustSectionStatus(applicationFormTrustDetails) == Status.Completed;
			bool plansForGrowthStatus = CalculatePlansForGrowthSectionStatus(applicationFormTrustDetails) == Status.Completed;
			bool improvementStatus = CalculateSchoolImprovementStrategyStatus(applicationFormTrustDetails) == Status.Completed;
			bool governanceStatus = CalculateGovernanceStructureSectionStatus(conversionApplication) == Status.Completed;
			bool keyPeopleStatus = CalculateKeyPeopleSectionStatus(applicationFormTrustDetails) == Status.Completed;

			var boolList = new List<bool>
			{
				nameOfTrustStatus,
				openingDateStatus,
				trustReasonsStatus,
				plansForGrowthStatus,
				improvementStatus,
				governanceStatus,
				keyPeopleStatus
			};

			if (boolList.All(x => x))
				return Status.Completed;

			return boolList.All(x => !x) ? Status.NotStarted : Status.InProgress;
		}

		return Status.NotStarted;
	}

	///<inheritdoc/>
	public Status CalculateApplicationDeclarationStatus(ConversionApplication? conversionApplication)
	{
		if (conversionApplication != null)
		{
			if (conversionApplication.ApplicationType == ApplicationTypes.JoinAMat)
			{
				var school = conversionApplication.Schools.FirstOrDefault();

				if (school != null)
				{
					return CalculateDeclarationSectionStatus(school);
				}
			}
			else // FAM
			{
				// TODO: no idea what logic will be !!
				return Status.NotStarted;
			}
		}

		return Status.NotStarted;
	}

	public Status CalculateSchoolStatus(List<ApplicationComponentViewModel> schoolComponents)
	{
		if (schoolComponents.Any() && schoolComponents.All(c => c.Status == Status.Completed))
			return Status.Completed;
		if (schoolComponents.Any() && schoolComponents.All(c => c.Status == Status.NotStarted))
			return Status.NotStarted;
		return Status.InProgress;
	}
	
	///<inheritdoc/>
	public Status CalculateApplicationStatus(ConversionApplication? conversionApplication, IEnumerable<SchoolComponentsViewModel> schoolComponents)
	{
		var allSchoolStatuses = schoolComponents.Select(schoolComponent => CalculateSchoolStatus(schoolComponent.SchoolComponents)).ToList();

		var trustStatus = CalculateTrustStatus(conversionApplication);

		if (allSchoolStatuses.All(c => c == Status.Completed) && trustStatus == Status.Completed)
			return Status.Completed;
		if (allSchoolStatuses.All(c => c == Status.NotStarted) && trustStatus == Status.NotStarted)
			return Status.NotStarted;
		return Status.InProgress;
	}
}
