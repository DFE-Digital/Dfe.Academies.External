namespace Dfe.Academies.External.Web.Services
{
	internal class AbstractService
	{
		internal readonly IHttpClientFactory ClientFactory;
		internal const string HttpClientName = "AcademiesClient";
		
		protected AbstractService(IHttpClientFactory clientFactory)
		{
			ClientFactory = clientFactory;
		}
	}
}