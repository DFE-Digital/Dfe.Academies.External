using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationRetrievalService : BaseService, IConversionApplicationRetrievalService
{
    private readonly ILogger<ConversionApplicationRetrievalService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public ConversionApplicationRetrievalService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationRetrievalService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public List<ConversionApplication> GetCompletedApplications(string? username)
    {
        // TODO: Get data from Academisation API
        // TODO: filter by useremail

        List<ConversionApplication> existingApplications = // Mock Demo Data
            new List<ConversionApplication>
            {
            new() { Id = 1, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "Harpenden Academy trust",
                SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolName = "St George’s school" } } }
            };

        return existingApplications;
    }

    public List<ConversionApplication> GetPendingApplications(string? username)
    {
        // TODO: Get data from Academisation API
        // TODO: filter by useremail

        // Mock Demo Data
        List<ConversionApplication> existingApplications = new List<ConversionApplication>
            {
            new() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolName = "Cambridge Regional college" } } },
            new() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() { Id = 3, SchoolName = "Fen Ditton primary school" }, 
                                                                                                    new() {Id  = 3, SchoolName = "Chesterton primary school" }, 
                                                                                                    new() {Id  = 3, SchoolName = "North Cambridge academy"} } },
            new() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolName = "King’s College London Maths school" } } }
            };

        return existingApplications;
    }

    public async Task<List<ConversionApplicationAuditEntry>> GetConversionApplicationAuditEntries(long id)
    {
        // TODO: Get data from Academisation API

        List<ConversionApplicationAuditEntry> auditEntries = new List<ConversionApplicationAuditEntry>
        {
            new(createdBy:"Phillip Frond", typeOfChange: "change", entityChanged: "Application", propertyChanged: "school") 
                {Id = 99, DateCreated = Convert.ToDateTime("25/05/2022")},
            new(createdBy: "Peter Parker", typeOfChange: "change", entityChanged: "Application", propertyChanged: "trust") 
                {Id = 98, DateCreated = Convert.ToDateTime("20/05/2022")},
            new(createdBy: "Richard Dickenson", typeOfChange: "add", entityChanged: "Application", propertyChanged: "started")
                {Id = 97, DateCreated = Convert.ToDateTime("15/05/2022")},
        };

        return auditEntries;
    }

    public async Task<List<ConversionApplicationComponent>> GetConversionApplicationComponentStatuses(long id)
    {
        // TODO: Get data from Academisation API

        throw new NotImplementedException();
    }
}

