using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class HomeModel : BasePageModel
    {
        [BindProperty]
        public List<ConversionApplication> ExistingApplications { get; set; } = new();
        public List<ConversionApplication> CompletedApplications { get; set; } = new();

        private readonly IConversionApplicationRetrievalService _conversionApplications;
        private readonly ILogger _logger;

        public HomeModel(IConversionApplicationRetrievalService conversionApplications, ILogger<HomeModel> logger)
        {
            _conversionApplications = conversionApplications;
            _logger = logger;
        }

        public void OnGet()
        {           
            try
            {
                //TODO: Get login username 
                var username = User.Identity?.Name;

                ExistingApplications = _conversionApplications.GetPendingApplications(username);

                CompletedApplications = _conversionApplications.GetCompletedApplications(username);
            }
            catch (Exception ex)
            {
	            _logger.LogError("Application::HomeModel::OnGet::Exception - {Message}", ex.Message);
            }
        }

        public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}
    }
}
