using System.Net.Mime;
using Dfe.Academies.External.Web.Logger;
using Dfe.Academies.External.Web.Services;

namespace Dfe.Academies.External.Web.Extensions;

public static class StartupExtension
{
    /// <summary>
    /// HttpFactory for Academies API
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="Exception"></exception>
    public static void AddAcademiesApi(this IServiceCollection services, IConfiguration configuration)
    {
        var academiesApiEndpoint = configuration["academies-api:endpoint"];
        var academiesApiKey = configuration["academies-api:key"];

        if (string.IsNullOrWhiteSpace(academiesApiEndpoint) || string.IsNullOrWhiteSpace(academiesApiKey))
            throw new Exception("AddAcademiesApi::missing configuration");

        services.AddHttpClient("AcademiesClient", client =>
        {
            client.BaseAddress = new Uri(academiesApiEndpoint);
            client.DefaultRequestHeaders.Add("ApiKey", academiesApiKey);
            client.DefaultRequestHeaders.Add("ContentType", MediaTypeNames.Application.Json);
        });
    }

    /// <summary>
    /// HttpFactory for Academisation API
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="Exception"></exception>
    public static void AddAcademisationApi(this IServiceCollection services, IConfiguration configuration)
    {
	    var academisationApiEndpoint = configuration["academisation-api:endpoint"];
	    var academisationApiKey = configuration["academisation-api:key"];

	    if (string.IsNullOrWhiteSpace(academisationApiEndpoint) || string.IsNullOrWhiteSpace(academisationApiKey))
		    throw new Exception("AddAcademisationApi::missing configuration");

	    services.AddHttpClient("AcademisationClient", client =>
	    {
		    client.BaseAddress = new Uri(academisationApiEndpoint);
		    client.DefaultRequestHeaders.Add("ApiKey", academisationApiKey);
		    client.DefaultRequestHeaders.Add("ContentType", MediaTypeNames.Application.Json);
	    });
    }

    /// <summary>
    /// Setup Service layer DI
    /// </summary>
    /// <param name="services"></param>
    public static void AddInternalServices(this IServiceCollection services)
    {
        // Web application services
        services.AddSingleton<IConversionApplicationCreationService, ConversionApplicationCreationService>();
        services.AddSingleton<IConversionApplicationRetrievalService, ConversionApplicationRetrievalService>();
        services.AddSingleton<IReferenceDataRetrievalService, ReferenceDataRetrievalService>();

        // others......
        services.AddSingleton<ILoggerClass, LoggerClass>();
    }
}