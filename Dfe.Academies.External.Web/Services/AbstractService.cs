namespace Dfe.Academies.External.Web.Services
{
    public class AbstractService
	{
		internal readonly IHttpClientFactory ClientFactory;
		internal const string HttpClientName = "AcademiesClient";
		
		protected AbstractService(IHttpClientFactory clientFactory)
		{
			ClientFactory = clientFactory;
		}
	}
}