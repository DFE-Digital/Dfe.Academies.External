using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.RazorPages;



namespace Dfe.Academies.External.Web.Pages.Help
{
	public class HelpModel : PageModel
	{
        public string Message { get;  set; }

		public void OnGet()
		{
			 var RouteValue = RouteData.Values["HelpType"];
             Message = RouteValue.ToString();
		}
	}

}