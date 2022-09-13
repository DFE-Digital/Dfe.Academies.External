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
			// TODO: Get data from Academisation API
			//// var applications = await _resilientRequestProvider.GetAsync();

			// **** Mock Demo Data - as per Figma - for now ****
			//baseaddress has a backslash at the end to be a valid URI !!!
			//https://academies-academisation-api-dev.azurewebsites.net/application/99
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
			// TODO: MR:- get application from API then re-shape it !
			// var application = await GetApplication(applicationId);

			// **** Mock Demo Data - as per Figma ****
			List<ConversionApplicationComponent> conversionApplicationComponents = new()
			{
			    //V1:-
			    new(name:"About the conversion") {Id = 1, SchoolId = schoolId, Status = Status.InProgress},
				new(name:"Further information") {Id = 1, SchoolId = schoolId, Status = Status.NotStarted},
				new(name:"Finances") {Id = 4, SchoolId = schoolId, Status = Status.NotStarted}, // existing
			    new(name:"Future pupil numbers") {Id = 3, SchoolId = schoolId, Status = Status.NotStarted},
				new(name:"Land and buildings") {Id = 7, SchoolId = schoolId, Status = Status.NotStarted},
				new(name:"Consultation") {Id = 7, SchoolId = schoolId, Status = Status.NotStarted},
				new(name:"Pre-opening support grant") {Id = 7, SchoolId = schoolId, Status = Status.NotStarted},
				new(name:"Declaration") {Id = 7, SchoolId = schoolId, Status = Status.NotStarted}
			    //// V2 below:-
			    ////new(name:"Contact details") {ApplicationId = 1, SchoolId = schoolId, Status = Status.Completed},
			    ////new(name:"Performance and safeguarding") {ApplicationId = 2, SchoolId = schoolId, Status = Status.InProgress},
			    ////new(name:"Partnerships and affiliations") {ApplicationId = 5, SchoolId = schoolId, Status = Status.NotStarted},
			    ////new(name:"Religious education") {ApplicationId = 6, SchoolId = schoolId, Status = Status.NotStarted},
			    ////new(name:"Local authority") {ApplicationId = 8, SchoolId = schoolId, Status = Status.NotStarted}
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
			// MR:- contributors are a list<> against the application within API json!

			// TODO: Get data from Academisation API
			// var application = await GetApplication(applicationId);

			// **** Mock Demo Data - as per Figma ****
			List<ConversionApplicationContributor> conversionApplicationContributors = new()
			{
				new(firstName: "Phillip", lastName:"Frond","Phillip@email.com", SchoolRoles.ChairOfGovernors, null)
					{ApplicationId = applicationId},
				new(firstName: "Robert", lastName: "Phillips", "Robert@email.com", role:SchoolRoles.Other ,  otherRoleName: "PA to the headteacher")
					{ApplicationId = applicationId}
			};

			return conversionApplicationContributors;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetConversionApplicationContributors::Exception - {Message}", ex.Message);
			return new List<ConversionApplicationContributor>();
		}
	}

	///<inheritdoc/>
	public async Task<ConversionApplication> GetApplication(int applicationId)
	{
		try
		{
			//baseaddress has a backslash at the end to be a valid URI !!!
			//https://academies-academisation-api-dev.azurewebsites.net/application/99
			// endpoint will return 404 if id NOT found !
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				Converters = {
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				}
			};

			// Get data from Academisation API
			var conversionApplication = await _resilientRequestProvider.GetAsync<ConversionApplication>(apiurl, options);

			return conversionApplication;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationRetrievalService::GetApplication::Exception - {Message}", ex.Message);
			return new ConversionApplication();
		}
	}
}
