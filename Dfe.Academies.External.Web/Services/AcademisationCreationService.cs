using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Services;

internal sealed class AcademisationCreationService : AbstractService, IAcademisationCreationService
{
    private readonly ILogger<AcademisationCreationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public AcademisationCreationService(IHttpClientFactory httpClientFactory, ILogger<AcademisationCreationService> logger) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

	public TrustApplication CreateNewApplication(ApplicationTypes applicationType)
    {
        ResilientRequestProvider apiRequestProvider = new ResilientRequestProvider(_httpClientFactory.CreateClient(HttpClientName));

        throw new NotImplementedException();
    }
}