using Dfe.Academies.External.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public List<TrustApplication> existingApplications { get; set; }
        public void OnGet()
        {
            //TODO: Get login username 
           var username = User.Identity.Name;

            TrustApplication trustApplication = new TrustApplication(); // TODO: use Dependency Injection

            existingApplications = trustApplication.GetPendingApplications(username);

            // TODO: Write unit test for GetPendingApplications
        }
    }
}
