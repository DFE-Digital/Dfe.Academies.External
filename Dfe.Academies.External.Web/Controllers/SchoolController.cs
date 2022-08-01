using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;

namespace Dfe.Academies.External.Web.Controllers
{
	// TODO MR:- baseController
	// [Authorize]
	public class SchoolController : Controller
	{
		private const int SearchQueryMinLength = 3;
		private readonly ILogger<SchoolController> _logger;
		private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;

		public SchoolController(ILogger<SchoolController> logger,
								IReferenceDataRetrievalService referenceDataRetrievalService)
		{
			_logger = logger;
			_referenceDataRetrievalService = referenceDataRetrievalService;
		}

		[HttpGet]
		[Route("school/SchoolOverview/{appId}/{applyingSchoolId}")]
		[Route("school/school/SchoolOverview")]
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

		[HttpGet]
		[Route("school/search")]
		[Route("school/school/search")]
		public async Task<IEnumerable<string>> Search(string searchQuery)
		{
			try
			{
				_logger.LogInformation("SchoolController::Search::OnGetSchoolsSearchResult");

				// Double check search query.
				if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < SearchQueryMinLength)
				{
					// TODO MR:- ?? concerns casework returns a JSON array, should we do this? Depends what API returns
                    //return new JsonResult(Array.Empty<SchoolSearchResultViewModel>());
					return Enumerable.Empty<string>();
				}

				var schoolSearch = new SchoolSearch(searchQuery, string.Empty, string.Empty);
				var schoolSearchResponse = await _referenceDataRetrievalService.SearchSchools(schoolSearch);

				// TODO MR:- ?? concerns casework returns a JSON array, should we do this? Depends what API returns
				//return new JsonResult(schoolSearchResponse);

				if (schoolSearchResponse.Any())
				{
					return schoolSearchResponse.Select(x => x.DisplayName).AsEnumerable();
				}
				else
				{
					return Enumerable.Empty<string>();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("SchoolController::Search::OnGetSchoolsSearchResult::Exception - {Message}", ex.Message);

				// TODO MR:- ?? concerns casework returns below which makes sense.
				// Would need to amend controller method to return Task<ActionResult>
				//return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
				return Enumerable.Empty<string>();
			}
		}

		[HttpGet]
		[Route("school/ReturnSchoolDetailsPartialViewPopulated")]
		[Route("school/school/ReturnSchoolDetailsPartialViewPopulated")]
		public async Task<IActionResult> ReturnSchoolDetailsPartialViewPopulated(string selectedSchool)
		{
			try
			{
				// Remove whitespace and trailing ) then split removing empty entries
				var schoolSplit = selectedSchool
                    .Trim()
					.Replace(")", string.Empty)
					.Split('(', StringSplitOptions.RemoveEmptyEntries);

				int urn = Convert.ToInt32(schoolSplit[^1]);
				var result = await _referenceDataRetrievalService.GetSchool(urn);

				var vm = new SchoolDetailsViewModel(schoolName: result.Name,
					urn: Convert.ToInt32(result.Urn),
					street: result.Address.Street,
					town: result.Address.Town,
					fullUkPostcode: result.Address.Postcode);

				return PartialView("_SchoolDetails", vm);
			}
			catch (Exception ex)
			{
				_logger.LogError("SchoolController::DisplaySearchResult::OnGetSchoolsSearchResult::Exception - {Message}", ex.Message);
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
