using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers
{
	// TODO MR:- baseController
	// [Authorize]
	public class SchoolController : Controller
	{
		private readonly ILogger<SchoolController> _logger;

		public SchoolController(ILogger<SchoolController> logger)
		{
			_logger = logger;	
		}

		[HttpGet]
		[Route("School/SchoolOverview/{appId}/{applyingSchoolId}")]
		public async Task<IActionResult> Overview(int appId, int applyingSchoolId)
		{
			try
			{
				return View("SchoolOverview");
			}
			catch (Exception ex)
			{
				_logger.LogError("SchoolController::Overview::Overview::Exception - {Message}", ex.Message);
			}

			return null;
		}
	}
}
