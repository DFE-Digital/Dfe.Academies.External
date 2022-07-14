using System.Globalization;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationRetrievalService : BaseService, IConversionApplicationRetrievalService
{
    private readonly ILogger<ConversionApplicationRetrievalService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ResilientRequestProvider _resilientRequestProvider;

    public ConversionApplicationRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationRetrievalService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _resilientRequestProvider = new ResilientRequestProvider(httpClientFactory.CreateClient(HttpClientName));
    }

    public List<ConversionApplication> GetCompletedApplications(string? username)
    {
	    // TODO: Get data from Academisation API
	    //// _resilientRequestProvider.Get();

	    // TODO: filter by useremail

	    // **** Mock Demo Data - as per Figma ****
	    List<ConversionApplication> existingApplications = new()
	    {
		    new() { Id = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549",
			    SchoolOrSchoolsApplyingToConvert = new()
				    { new(schoolName: "St George’s school" ) {SchoolId = 2 } } }
	    };

	    return existingApplications;
    }

    public List<ConversionApplication> GetPendingApplications(string? username)
    {
        // TODO: Get data from Academisation API
        //// _resilientRequestProvider.Get();

        // TODO: filter by useremail

        // **** Mock Demo Data - as per Figma ****
        List<ConversionApplication> existingApplications = new()
            {
                new() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549",
                        SchoolOrSchoolsApplyingToConvert = new List<SchoolApplyingToConvert>
                        {
                            new(schoolName: "Cambridge Regional college") {SchoolId = 96 }
                        }
                },
                new() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956",
                        SchoolOrSchoolsApplyingToConvert = new List<SchoolApplyingToConvert>{
                            new(schoolName: "Fen Ditton primary school") { SchoolId = 99 },
                            new(schoolName: "Chesterton primary school") { SchoolId  = 98},
                            new(schoolName: "North Cambridge academy") { SchoolId  = 97 }
                        }
                },
                new() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974",
                        SchoolOrSchoolsApplyingToConvert = new List<SchoolApplyingToConvert>
                        {
                            new(schoolName: "King’s College London Maths school") { SchoolId = 95 }
                        }
                }
            };

        return existingApplications;
    }

    public async Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(int applicationId)
    {
        // TODO: Get data from Academisation API
        //// _resilientRequestProvider.Get();

        // **** Mock Demo Data - as per Figma ****
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

    public async Task<List<ConversionApplicationComponent>> GetSchoolApplicationComponents(int schoolId)
    {
        // TODO: Get data from Academisation API
        //// _resilientRequestProvider.Get();

        // **** Mock Demo Data - as per Figma ****
        List<ConversionApplicationComponent> conversionApplicationComponents = new()
        {
            new(name:"Contact details") {Id = 1, SchoolId = schoolId, Status = Status.Completed},
            new(name:"Performance and safeguarding") {Id = 2, SchoolId = schoolId, Status = Status.InProgress},
            new(name:"Pupil numbers") {Id = 3, SchoolId = schoolId, Status = Status.NotStarted},
            new(name:"Finances") {Id = 4, SchoolId = schoolId, Status = Status.NotStarted},
            new(name:"Partnerships and affiliations") {Id = 5, SchoolId = schoolId, Status = Status.NotStarted},
            new(name:"Religious education") {Id = 6, SchoolId = schoolId, Status = Status.NotStarted},
            new(name:"Land and buildings") {Id = 7, SchoolId = schoolId, Status = Status.NotStarted},
            new(name:"Local authority") {Id = 8, SchoolId = schoolId, Status = Status.NotStarted}
        };

        return conversionApplicationComponents;
    }

    public async Task<List<ConversionApplicationContributor>> GetConversionApplicationContributors(int applicationId)
    {
        // TODO: Get data from Academisation API
        // _resilientRequestProvider.Get

        // **** Mock Demo Data - as per Figma ****
        List<ConversionApplicationContributor> conversionApplicationContributors = new()
        {
            new(firstName: "Phillip", surname:"Frond", SchoolRoles.Chair, null) 
											{ApplicationId = applicationId},
            new(firstName: "Robert", surname: "Phillips", role:SchoolRoles.Other ,  otherRoleNotListed: "PA to the headteacher") 
											{ApplicationId = applicationId}
        };

        return conversionApplicationContributors;
    }

    public async Task<SchoolApplyingToConvert> GetSchool(int schoolId)
    {
        // TODO: Get data from Academisation API
        // _resilientRequestProvider.Get

        // **** Mock Demo Data - as per Figma ****
        SchoolApplyingToConvert schoolApplyingToConvert = new(schoolName: "Chesterton primary school") { SchoolId = schoolId };

        return schoolApplyingToConvert;
    }

    public async Task<ConversionApplication> GetApplication(int applicationId)
    {
        // TODO: Get data from Academisation API
        // _resilientRequestProvider.Get

        // **** Mock Demo Data - as per Figma ****
        ConversionApplication conversionApplication = new()
        {
            Id = applicationId,
            UserEmail = "",
            Application = "Join a multi-academy trust A2B_2549",
            SchoolOrSchoolsApplyingToConvert = new List<SchoolApplyingToConvert>
            {
                new(schoolName: "Cambridge Regional college") { SchoolId = 96 }
            }
        };

        return conversionApplication;
    }
}
