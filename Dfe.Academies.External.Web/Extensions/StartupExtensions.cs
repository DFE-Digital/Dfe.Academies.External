using System.Net.Mime;
using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Services;
using Dfe.Academisation.CorrelationIdMiddleware;

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
		var academiesApiEndpoint = configuration["academies_api:endpoint"];
		var academiesApiKey = configuration["academies_api:key"];

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
		var academisationApiEndpoint = configuration["academisation_api:endpoint"];
		var academisationApiKey = configuration["academisation_api:key"];

		if (string.IsNullOrWhiteSpace(academisationApiEndpoint) || string.IsNullOrWhiteSpace(academisationApiKey))
			throw new Exception("AddAcademisationApi::missing configuration");

		services.AddHttpClient("AcademisationClient", client =>
		{
			client.BaseAddress = new Uri(academisationApiEndpoint);
			client.DefaultRequestHeaders.Add("x-api-key", academisationApiKey);
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
		services.AddScoped<IConversionApplicationService, ConversionApplicationService>();
		services.AddScoped<IConversionApplicationRetrievalService, ConversionApplicationRetrievalService>();
		services.AddScoped<IReferenceDataRetrievalService, ReferenceDataRetrievalService>();
		services.AddSingleton<IContributorEmailSenderService, ContributorEmailSenderService>();
	}

	public static IApplicationBuilder UseBespokeExceptionHandling(this IApplicationBuilder @this, IHostEnvironment environment)
	{
		var logger = @this.ApplicationServices.GetRequiredService<ILogger<BespokeExceptionHandlingMiddleware>>();
		@this.UseMiddleware<BespokeExceptionHandlingMiddleware>(environment, logger);

		return @this;
	}
}
