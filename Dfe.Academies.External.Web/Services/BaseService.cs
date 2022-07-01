namespace Dfe.Academies.External.Web.Services
{
    public class BaseService
	{
		internal readonly IHttpClientFactory ClientFactory;
		internal const string HttpClientName = "AcademiesClient";
		
		protected BaseService(IHttpClientFactory clientFactory)
		{
			ClientFactory = clientFactory;
		}
	}
}