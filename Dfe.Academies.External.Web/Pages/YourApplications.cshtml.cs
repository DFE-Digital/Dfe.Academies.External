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

        public HomeModel(ITrustApplication trustApplication)
        {
            _trustApplication = trustApplication;
        }

        public void OnGet()
        {           
            try
            {
                //TODO: Get login username 
                var username = User.Identity.Name;

                existingApplications = _trustApplication.GetPendingApplications(username);

                completedApplications = _trustApplication.GetCompletedApplications(username);

                // TODO: Write unit test for GetCompletedtingApplications
            }
            catch (Exception ex)
            {
                // TODO: Add error log to file
                ;
            }

        }
    }
}
