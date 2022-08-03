namespace Dfe.Academies.External.Web.Services
{
    public class BaseService
	{
		internal readonly IHttpClientFactory ClientFactory;
		internal const string AcademiesAPIHttpClientName = "AcademiesClient";
		internal const string AcademisationAPIHttpClientName = "AcademisationClient";

		protected BaseService(IHttpClientFactory clientFactory)
		{
			ClientFactory = clientFactory;
		}
	}
}