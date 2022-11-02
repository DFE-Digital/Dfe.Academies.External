using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
    public class ApplicationAccessExceptionModel : PageModel
    {
		[BindProperty]
	    public string Message { get; set; } = null!;

	    public void OnGet(string errorMessage)
        {
	        Message = errorMessage;
        }
    }
}
