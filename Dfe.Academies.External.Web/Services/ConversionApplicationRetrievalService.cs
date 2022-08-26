using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using System.Globalization;
using Newtonsoft.Json;

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
		    // **** Mock Demo Data - as per Figma - for now ****
		    List<ConversionApplication> existingApplications = new()
		    {
			    new() { ApplicationId = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549",
				    Schools = new()
				    { new(schoolName: "St George’s school", applicationId: 1, urn: 101934, ukprn: null)
				    }
			    }
		    };
		    
		    return existingApplications;
		}
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationRetrievalService::GetCompletedApplications::Exception - {Message}", ex.Message);
		    return new List<ConversionApplication>();
	    }
    }

    ///<inheritdoc/>
	public List<ConversionApplication> GetPendingApplications(string? username)
    {
	    try
	    {
		    // TODO: Get data from Academisation API
		    //// var applications = await _resilientRequestProvider.GetAsync();

		    // **** Mock Demo Data - as per Figma - for now ****
		    List<ConversionApplication> existingApplications = new()
		    {
			    new() { ApplicationId = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549",
				    Schools = new List<SchoolApplyingToConvert>
				    {
					    new(schoolName: "Cambridge Regional college", urn: 101934, ukprn: null,  applicationId: 2)
				    }
			    },
			    new() { ApplicationId = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956",
				    Schools = new List<SchoolApplyingToConvert>{
					    new(schoolName: "Fen Ditton primary school", urn: 101934, applicationId: 3, ukprn: null),
					    new(schoolName: "Chesterton primary school", urn: 101934, applicationId: 3, ukprn: null),
					    new(schoolName: "North Cambridge academy", urn: 101934, applicationId: 3, ukprn: null)
				    }
			    },
			    new() { ApplicationId = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974",
				    Schools = new List<SchoolApplyingToConvert>
				    {
					    new(schoolName: "King’s College London Maths school", urn: 101934, applicationId: 4, ukprn: null)
				    }
			    }
		    };

		    return existingApplications;
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
			    new(firstName: "Phillip", surname:"Frond","Phillip@email.com", SchoolRoles.Chair, null)
				    {ApplicationId = applicationId},
			    new(firstName: "Robert", surname: "Phillips", "Robert@email.com", role:SchoolRoles.Other ,  otherRoleName: "PA to the headteacher")
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
			//https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}/application/{applicationId}?api-version=V1";

			ConversionApplication conversionApplication = new();

			// TODO: Get data from Academisation API
			//var application = await _resilientRequestProvider.GetAsync<ConversionApplication>(apiurl);

			// **** Mock Demo Data - as per Figma ****
			switch (applicationId)
			{
				case 1:
					conversionApplication = new()
					{
						ApplicationId = applicationId,
						ApplicationType = ApplicationTypes.JoinMat,
						UserEmail = "",
						Application = "Join a multi-academy trust A2B_2549",
						// MR:- comment out below if want to test that application overview page shows a 'add school' button!!
						Schools = new List<SchoolApplyingToConvert>
					{
						new(schoolName: "Chesterton primary school", urn: 101934, applicationId: applicationId, ukprn: null)
					},
						ConversionStatus = 3,
						// MR:- comment out below if want to test that application overview page shows a 'add trust' button!!
						ExistingTrust = new(trustName: "Existing Trust Test")
					};
					break;
				case 2:
					conversionApplication = new()
					{
						ApplicationId = applicationId,
						ApplicationType = ApplicationTypes.FormNewMat,
						UserEmail = "",
						Application = "Form a new multi-academy trust A2B_2549",
						Schools = new List<SchoolApplyingToConvert>
					{
						new(schoolName: "Chesterton primary school", urn: 101934, applicationId: applicationId, ukprn: null),
						new(schoolName: "Newcastle primary school", urn: 101934, applicationId:applicationId, ukprn: null),
						new(schoolName: "Another primary school", urn: 101934, applicationId:applicationId, ukprn: null)
					},
						ConversionStatus = 3,
						FormATrust = new(proposedTrustName: "New multi academy trust"),
					};
					break;
				case 3:
					// MR:- this application is rare
					conversionApplication = new()
					{
						ApplicationId = applicationId,
						ApplicationType = ApplicationTypes.FormNewSingleAcademyTrust,
						UserEmail = "",
						Application = "Form a new single academy trust A2B_2549",
						Schools = new List<SchoolApplyingToConvert>
					{
						new(schoolName: "Chesterton primary school", urn: 101934, applicationId: applicationId, ukprn: null)
					},
						ConversionStatus = 3,
						FormATrust = new(proposedTrustName: "New single academy Trust")
					};
					break;
			}

			return conversionApplication;
		}
	    catch (Exception ex)
	    {
		    _logger.LogError("ConversionApplicationRetrievalService::GetApplication::Exception - {Message}", ex.Message);
		    return new ConversionApplication();
		}
    }
}
