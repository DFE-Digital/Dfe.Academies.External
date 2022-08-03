using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers
{
    public class BaseController : Controller
    {
        public const int SearchQueryMinLength = 3;
        private readonly ILogger<BaseController> _logger;
        public readonly IReferenceDataRetrievalService ReferenceDataRetrievalService;
        public readonly IConversionApplicationRetrievalService ConversionApplicationRetrievalService;

        public BaseController(ILogger<BaseController> logger,
                                IReferenceDataRetrievalService referenceDataRetrievalService,
                                IConversionApplicationRetrievalService conversionApplicationRetrievalService)
        {
            _logger = logger;
            ReferenceDataRetrievalService = referenceDataRetrievalService;
            ConversionApplicationRetrievalService = conversionApplicationRetrievalService; 
        }

        protected IActionResult CatchErrorAndRedirect(Exception ex)
        {
            _logger.LogError("BaseController::CatchErrorAndRedirect::Exception - {Message}", ex.Message);
            return RedirectToAction("Error", "academies");
        }

        protected async Task<ApplicationCacheValuesViewModel?> LoadAndSetApplicationDetails(int applicationId)
        {
            ApplicationCacheValuesViewModel? cachedValuesViewModel = null;
            var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(applicationId, Enums.ApplicationTypes.FormNewMat);

            if (Object.ReferenceEquals(applicationDetails, null))
            {
	            if (applicationDetails == null) return cachedValuesViewModel;
	            cachedValuesViewModel = new(applicationDetails.Id, applicationDetails.ApplicationType,
		            applicationDetails.ApplicationReference);

	            ViewDataHelper.StoreSerialisedValue(nameof(ApplicationCacheValuesViewModel), ViewData, cachedValuesViewModel);
            }

            return cachedValuesViewModel;
        }
    }
}
