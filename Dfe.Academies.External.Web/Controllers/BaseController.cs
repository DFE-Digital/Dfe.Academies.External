using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers
{
    public class BaseController : Controller
    {
        public const int SearchQueryMinLength = 3;
        public readonly IReferenceDataRetrievalService ReferenceDataRetrievalService;

        public BaseController(IReferenceDataRetrievalService referenceDataRetrievalService)
        {
            ReferenceDataRetrievalService = referenceDataRetrievalService;
        }
    }
}
