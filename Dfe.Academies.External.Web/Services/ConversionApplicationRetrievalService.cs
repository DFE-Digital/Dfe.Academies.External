using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationRetrievalService : BaseService, IConversionApplicationRetrievalService
{
	private readonly ILogger<ConversionApplicationRetrievalService> _logger;
	private readonly HttpClient _httpClient;
	private readonly ResilientRequestProvider _resilientRequestProvider;

	public ConversionApplicationRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationRetrievalService> logger) : base(httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		_logger = logger;
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
		// 1) SchoolMainContacts = !string.IsNullOrEmpty(selectedSchool.SchoolConversionContactHeadName)
		// 2) ApplicationConversionTargetDate = selectedSchool.SchoolConversionTargetDateSpecified.HasValue
		// 3) ApplicationJoinTrustReasons = !string.IsNullOrWhiteSpace(selectedSchool.ApplicationJoinTrustReason)
		// 4) ApplicationChangeSchoolName = selectedSchool.ConversionChangeNamePlanned.HasValue

		// TODO :- agree logic
		return Status.InProgress;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateFurtherInformationSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		// one LARGE form in v1.5. So can just use below logic:-
		// var sectionStarted = !string.IsNullOrEmpty(selectedSchool.TrustBenefitDetails)

		// TODO
		return Status.InProgress;
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
		// 1) PreviousFinancialYear = selectedSchool.previousFinancialYear.FinancialYearEndDate.HasValue
		// 2) CurrentFinancialYear = selectedSchool.currentFinancialYear.FinancialYearEndDate.HasValue
		// 3) NextFinancialYear = selectedSchool.nextFinancialYear.FinancialYearEndDate.HasValue
		// 4) loans = selectedSchool.HasLoans
		// 5) leases = selectedSchool.HasLeases
		// 6) FinancialInvestigations = selectedSchool.FinanceOngoingInvestigations.HasValue

		// TODO :- agree logic
		return Status.InProgress;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as future pupil numbers summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateFuturePupilNumbersSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.ProjectedPupilNumbersYear1 != null
			? Status.Completed
			: Status.NotStarted;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as Land and buildings summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateLandAndBuildingsSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.LandAndBuildings.WorksPlanned.HasValue != null
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
		return selectedSchool?.SchoolHasConsultedStakeholders.HasValue != null
			? Status.Completed
			: Status.NotStarted;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as Pre-opening support grant summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculatePreOpeningSupportGrantSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.SchoolSupportGrantFundsPaidTo.HasValue != null
			? Status.Completed
			: Status.NotStarted;
	}

	/// <summary>
	/// Will only return Completed or NotStarted as only one logic check !
	/// Same logic in here as declaration summary page. Should we re-factor?
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <returns></returns>
	private Status CalculateDeclarationSectionStatus(SchoolApplyingToConvert? selectedSchool)
	{
		return selectedSchool?.DeclarationBodyAgree.HasValue != null 
			? Status.Completed
			: Status.NotStarted;
	}

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

	/// <summary>
	/// calc JAM trust status - JAM specific components = 6 sections - so could return 'In Progress' or Completed or NotStarted
	/// Same logic in here as ApplicationSchoolJoinAMatTrustSummary page. Should we re-factor?
	/// </summary>
	/// <param name="conversionApplication"></param>
	/// <returns></returns>
	public Status CalculateJoinAMatTrustStatus(ConversionApplication? conversionApplication)
	{
		// need 3 bools to represent each sub-section. completed = yes/no
		// 1) applicationselecttrust :- !string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails?.TrustName) = complete
		// 2) applicationschooltrustconsent - 3 steps :-
		// a) step 1 (ApplicationSchoolTrustConsent) = conversionApplication.JoinTrustDetails != null ??? as this step is docs
		// b) step 2 (ApplicationSchoolChangesToATrust) = conversionApplication.JoinTrustDetails.ChangesToTrust.HasValue = complete
		// c) step 3 (ApplicationSchoolLocalGovernanceArrangements) = conversionApplication.JoinTrustDetails.ChangesToLaGovernanceExplained.HasValue

		if (conversionApplication != null && conversionApplication.JoinTrustDetails != null)
		{
			BitArray statuses = new BitArray(3);
			statuses.Set(0, !string.IsNullOrWhiteSpace(conversionApplication.JoinTrustDetails.TrustName)); // ONLY set to false IF EMPTY!
			statuses.Set(1, conversionApplication.JoinTrustDetails!.ChangesToTrust.HasValue);
			statuses.Set(2, conversionApplication.JoinTrustDetails!.ChangesToLaGovernance.HasValue);

			////bool hasAnyFalse = statuses.Cast<bool>().Contains(false);
			////bool hasAnyTrue = statuses.Cast<bool>().Contains(true);

			int falseCount = (from bool m in statuses
				where !m
				select m).Count();

			//// need to do a count of false. if falseCount = 3 - status = NotStarted
			//// need to do a count of false. if falseCount = >=1 || falseCount <=2 - status = InProgress
			//// need to do a count of false. if falseCount = 0 - status = Completed
			if (falseCount == 3)
			{
				return Status.NotStarted;
			}
			else if (falseCount > 0)
			{
				return Status.InProgress;
			}
			else
			{
				return Status.Completed;
			}
		}
		else
		{
			return Status.NotStarted;
		}
	}

	// calc FAM trust status - FAM specific components !!
	public Status CalculateFormAMatTrustStatus(ConversionApplication? conversionApplication)
	{
		// TODO:- agree logic !!

		// consume below:-
		// conversionApplication.FormATrust

		return Status.NotStarted;
	}
}
