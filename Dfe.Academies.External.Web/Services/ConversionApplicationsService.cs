using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationsService : AbstractService, IConversionApplicationsService
{
    private readonly ILogger<ConversionApplicationsService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public ConversionApplicationsService(IHttpClientFactory httpClientFactory, ILogger<ConversionApplicationsService> logger) : base(httpClientFactory)
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
                SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "St George’s school" } } }
            };

        return existingApplications;
    }

    public List<ConversionApplication> GetPendingApplications(string? username)
    {
        // TODO: Get data from Academisation API
        // TODO: filter by useremail

        List<ConversionApplication> existingApplications = // Mock Demo Data
            new List<ConversionApplication>
            {
            new() { Id = 2, UserEmail = "", Application = "Join a multi-academy trust A2B_2549", TrustName = "The Diocese of Ely multi - academy trust",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "Cambridge Regional college" } } },
            new() { Id = 3, UserEmail = "", Application = "Form a new multi- academy trust A2B_8956", TrustName = "Cambs multi-academy example trust",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() { Id = 3, SchoolOrSchoolsApplyingToConvertProperty = "Fen Ditton primary school" }, 
                                                                                                    new() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "Chesterton primary school" }, 
                                                                                                    new() {Id  = 3, SchoolOrSchoolsApplyingToConvertProperty = "North Cambridge academy"} } },
            new() { Id = 4, UserEmail = "", Application = "Form a new single academy trust A2B_8974", TrustName = "Single academy trust example",
                    SchoolOrSchoolsApplyingToConvert = new List<SchoolOrSchoolsApplyingToConvert>{ new() {Id = 2, SchoolOrSchoolsApplyingToConvertProperty = "King’s College London Maths school" } } }
            };

        return existingApplications;
    }
}

