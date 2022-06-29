using Dfe.Academies.External.Web.Logger;
using Dfe.Academies.External.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public List<TrustApplication> existingApplications { get; set; }
        public List<TrustApplication> completedApplications { get; set; }    

        private readonly ITrustApplication _trustApplication;
        private readonly ILoggerClass _Logger;

        public HomeModel(ITrustApplication trustApplication, ILoggerClass logger)
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
                // TODO: Add error log to file
                _Logger.Logger(ex.Message);
            }

        }
    }
}
