using Dfe.Academisation.CorrelationIdMiddleware;

namespace Dfe.Academies.External.Web.Services
{
	public class BaseService
	{
		internal const string AcademiesAPIHttpClientName = "AcademiesClient";
		internal const string AcademisationAPIHttpClientName = "AcademisationClient";
		public HttpClient HttpClient { get; set; }

		protected BaseService(IHttpClientFactory clientFactory, ICorrelationContext correlationContext, string httpClientName)
		{
			this.HttpClient = clientFactory.CreateClient(httpClientName);
			this.HttpClient.DefaultRequestHeaders.Add(Keys.HeaderKey, correlationContext.CorrelationId.ToString());
		}
	}
}
