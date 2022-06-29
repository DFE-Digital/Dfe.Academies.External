using Dfe.Academies.External.Web.Logger;
using Dfe.Academies.External.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public List<ConversionApplication> existingApplications { get; set; }
        public List<ConversionApplication> completedApplications { get; set; }    

        private readonly IConversionApplication _trustApplication;
        private readonly ILoggerClass _Logger;

        public HomeModel(IConversionApplication trustApplication, ILoggerClass logger)
        {
            _trustApplication = trustApplication;
            _Logger = logger;
        }

        public void OnGet()
        {           
            try
            {
                //TODO: Get login username 
                var username = User.Identity.Name;

                existingApplications = _trustApplication.GetPendingApplications(username);

                completedApplications = _trustApplication.GetCompletedApplications(username);

                _Logger.Logger("Message");

            }
            catch (Exception ex)
            {
                _Logger.Logger(ex.Message);
            }

        }
    }
}
