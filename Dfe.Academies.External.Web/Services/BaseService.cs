using GovUK.Dfe.CoreLibs.Http.Interfaces;

namespace Dfe.Academies.External.Web.Services
{
	public class BaseService
	{
		internal const string AcademiesAPIHttpClientName = "AcademiesClient";
		internal const string AcademisationAPIHttpClientName = "AcademisationClient";
		private const string CorrelationIdHeaderKey = "x-correlationId";
		public HttpClient HttpClient { get; set; }

		protected BaseService(IHttpClientFactory clientFactory, ICorrelationContext correlationContext, string httpClientName)
		{
			this.HttpClient = clientFactory.CreateClient(httpClientName);
			this.HttpClient.DefaultRequestHeaders.Add(CorrelationIdHeaderKey, correlationContext.CorrelationId.ToString());
		}
	}
}
