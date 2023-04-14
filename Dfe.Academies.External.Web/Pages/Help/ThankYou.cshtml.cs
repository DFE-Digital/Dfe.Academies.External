using Dfe.Academies.External.Web.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.Help
{
    public class ThankYouModel : PageModel
    {
		[BindProperty]
		public HelpTypes? page { get; set; } = null;

		public void OnGet(HelpTypes helpTypeId)
        {
			this.page = helpTypeId;
        }
    }
}
