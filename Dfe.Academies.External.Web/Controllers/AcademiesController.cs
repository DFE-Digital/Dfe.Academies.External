using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers
{
	public class AcademiesController : Controller
	{
		public IActionResult Error()
		{
			// TODO MR:- return a generic error page / view
			return View();
		}
	}
}
