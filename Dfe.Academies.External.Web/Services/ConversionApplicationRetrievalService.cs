using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationRetrievalService : BaseService, IConversionApplicationRetrievalService
{
	private readonly ILogger<ConversionApplicationRetrievalService> _logger;
	private readonly HttpClient _httpClient;
	private readonly ResilientRequestProvider _resilientRequestProvider;
	private readonly IFileUploadService _fileUploadService;
	public ConversionApplicationRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationRetrievalService> logger, IFileUploadService fileUploadService) : base(httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		_logger = logger;
		_fileUploadService = fileUploadService;
		_resilientRequestProvider = new ResilientRequestProvider(_httpClient);
	}

	///<inheritdoc/>
	public async Task<List<ConversionApplication>> GetCompletedApplications(string? email)
	{
		try
		{
			string apiurl = $"{_httpClient.BaseAddress}application/contributor/{email}?api-version=V1";

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
			_logger.LogError("ConversionApplicationRetrievalService::GetCompletedApplications::Exception - {Message}", ex.Message);
			return new List<ConversionApplication>();
		}
	}

	///<inheritdoc/>
	public async Task<List<ConversionApplication>> GetPendingApplications(string? email)
	{
		try
		{
			// baseaddress has a backslash at the end to be a valid URI !!!
			// https://academies-academisation-api-dev.azurewebsites.net/application/99
			// endpoint will return 404 if id NOT found !
			string apiurl = $"{_httpClient.BaseAddress}application/contributor/{email}?api-version=V1";

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
			_logger.LogError("ConversionApplicationRetrievalService::GetPendingApplications::Exception - {Message}", ex.Message);
			return new List<ConversionApplication>();
		}
	}

	///<inheritdoc/>
	public async Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(int applicationId)
	{
		try
		{
			// TODO: Get data from Academisation API - concept doesn't exist yet (12/08/2022)
			//// var applicationAudits = await _resilientRequestProvider.GetAsync();

			// **** Mock Demo Data - as per Figma - for now ****
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
			_logger.LogError("ConversionApplicationRetrievalService::GetConversionApplicationAuditEntries::Exception - {Message}", ex.Message);
			return new List<ConversionApplicationAuditEntry>();
		}
	}

	///<inheritdoc/>
	public async Task<List<ConversionApplicationComponent>> GetSchoolApplicationComponents(int applicationId, int schoolId)
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

			List<ConversionApplicationComponent> conversionApplicationComponents = new()
			{
			    new(name:"About the conversion") {Id = 1, SchoolId = schoolId, Status = CalculateAboutTheConversionSectionStatus(school)},
				new(name:"Further information") {Id = 1, SchoolId = schoolId, Status = CalculateFurtherInformationSectionStatus(school)},
				new(name:"Finances") {Id = 4, SchoolId = schoolId, Status = CalculateFinanceSectionStatus(school)},
			    new(name:"Future pupil numbers") {Id = 3, SchoolId = schoolId, Status = CalculateFuturePupilNumbersSectionStatus(school)},
				new(name:"Land and buildings") {Id = 7, SchoolId = schoolId, Status = CalculateLandAndBuildingsSectionStatus(school)},
				new(name:"Consultation") {Id = 7, SchoolId = schoolId, Status = CalculateConsultationSectionStatus(school)},
				new(name:"Pre-opening support grant") {Id = 7, SchoolId = schoolId, Status = CalculatePreOpeningSupportGrantSectionStatus(school)},
				new(name:"Declaration") {Id = 7, SchoolId = schoolId, Status = CalculateDeclarationSectionStatus(school)}
		    };

			return conversionApplicationComponents;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetSchoolApplicationComponents::Exception - {Message}", ex.Message);
			return new List<ConversionApplicationComponent>();
		}
	}
	
	public async Task<List<ConversionApplicationComponent>> GetFormAMatTrustComponents(int applicationId)
	{
		try
		{
			var application = await GetApplication(applicationId);

			if (application?.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}
			

			List<ConversionApplicationComponent> conversionApplicationComponents = new()
			{
				new(name:"Name of the trust") {Id = 1, Status = CalculateNameOfTheTrustSectionStatus(application.FormTrustDetails)},
				new(name:"Opening date") {Id = 2, Status = CalculateOpeningDateSectionStatus(application.FormTrustDetails)},
				new(name:"Reasons for forming the trust") {Id = 3,Status = CalculateReasonsForFormingTrustSectionStatus(application.FormTrustDetails)},
				new(name:"Plans for growth") {Id = 4, Status = CalculatePlansForGrowthSectionStatus(application.FormTrustDetails)},
				new(name:"School improvement strategy") {Id = 5, Status = CalculateSchoolImprovementStrategyStatus(application.FormTrustDetails)},
				new(name:"Governance structure") {Id = 6, Status = CalculateGovernanceStructureSectionStatus(application)},
				new(name:"Key people") {Id = 7, Status = CalculateKeyPeopleSectionStatus(application.FormTrustDetails)}
			};

			return conversionApplicationComponents;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetSchoolApplicationComponents::Exception - {Message}", ex.Message);
			return new List<ConversionApplicationComponent>();
		}
	}

	private Status CalculateKeyPeopleSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return Status.NotStarted;
	}

	private Status CalculateGovernanceStructureSectionStatus(ConversionApplication application)
	{
		 var result = _fileUploadService.GetFiles(FileUploadConstants.TopLevelFolderName, application.ApplicationId.ToString(), application.ApplicationReference, FileUploadConstants.JoinAMatTrustGovernanceFilePrefixFieldName).Result;
		 return result.Any() ? Status.Completed : Status.NotStarted;
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

	private Status CalculateOpeningDateSectionStatus(NewTrust applicationFormTrustDetails)
	{
		return applicationFormTrustDetails.FormTrustOpeningDate.HasValue ? Status.Completed : Status.NotStarted;
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
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

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
	private Status CalculateFurtherInformationSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		//This might only return InProgress if any of the file uploads are mandatory for the application but don't have to be uploaded immediately
		var sectionStarted = !string.IsNullOrEmpty(selectedSchool.TrustBenefitDetails);
		
		return sectionStarted ? Status.Completed : Status.NotStarted;
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
	/// Same logic in here as Pre-opening support grant summary page. Should we re-factor?
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

	///<inheritdoc/>
	public Status CalculateApplicationStatus(ConversionApplication? conversionApplication)
	{
		Status overallStatus = Status.NotStarted;
		Status schoolConversionStatus = Status.NotStarted;
		BitArray statuses = new BitArray(2);

		if (conversionApplication != null)
		{
			if (conversionApplication.ApplicationType == ApplicationTypes.JoinAMat)
			{
				var school = conversionApplication.Schools.FirstOrDefault();

				if (school != null && school.SchoolApplicationComponents.Any())
				{
					if (school.SchoolApplicationComponents.All(comp => comp.Status == Status.NotStarted))
					{
						schoolConversionStatus = Status.NotStarted;
					}
					else
					{
						schoolConversionStatus = school.SchoolApplicationComponents.All(comp => comp.Status == Status.Completed) ?
							Status.Completed : Status.InProgress;
					}
				}

				// below could return InProgress or Completed or NotStarted
				var trustStatus = CalculateTrustStatus(conversionApplication);
				statuses.Set(0, schoolConversionStatus == Status.Completed); // ONLY set to false IF NOT completed
				statuses.Set(1, trustStatus == Status.Completed);

				// bitwise, trustStatus == completed == true
				// bitwise, schoolConversionStatus == completed == true
				// need 2 true's for overall = Completed

				int trueCount = (from bool m in statuses
					where m
					select m).Count();

				if (trueCount == 2)
				{
					overallStatus = Status.Completed;
				}
				else
				{
					// one of schoolConversionStatus OR trustStatus is NOT completed !!!
					if (schoolConversionStatus == Status.Completed && trustStatus == Status.NotStarted)
					{
						overallStatus = Status.InProgress;
					}
					else if (trustStatus == Status.Completed && schoolConversionStatus == Status.NotStarted)
					{
						overallStatus = Status.InProgress;
					}
					else if (schoolConversionStatus == Status.Completed)
					{
						overallStatus = trustStatus;
					}
					else if (trustStatus == Status.Completed)
					{
						overallStatus = schoolConversionStatus;
					}
					else
					{
						// neither status are completed, one InProgress / one NotStarted for instance
						overallStatus = schoolConversionStatus > trustStatus ? schoolConversionStatus : trustStatus;
					}
				}
			}
			else // FAM
			{
				// TODO: no idea what logic will be !!
				overallStatus = Status.NotStarted;
			}
		}
		
		return overallStatus;
	}
}
