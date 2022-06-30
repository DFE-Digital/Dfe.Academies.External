using Dfe.Academies.External.Web.Logger;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public List<ConversionApplication> ExistingApplications { get; set; }
        public List<ConversionApplication> CompletedApplications { get; set; }    

        private readonly IConversionApplicationsService _conversionApplications;
        private readonly ILoggerClass _logger;

        public HomeModel(IConversionApplicationsService conversionApplications, ILoggerClass logger)
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
                _logger.Logger(ex.Message);
                //_logger.LogError("Application::HomeModel::OnGet::Exception - {Message}", ex.Message);
            }
        }
    }
}
