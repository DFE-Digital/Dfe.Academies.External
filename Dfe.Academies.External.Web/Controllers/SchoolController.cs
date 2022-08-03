using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.Academies.External.Web.Controllers
{
	// [Authorize]
	public class SchoolController : BaseController
	{
		private readonly ILogger<SchoolController> _logger;

		public SchoolController(ILogger<SchoolController> logger,
								IReferenceDataRetrievalService referenceDataRetrievalService,
								IConversionApplicationRetrievalService conversionApplicationRetrievalService) 
			: base(logger, referenceDataRetrievalService, conversionApplicationRetrievalService)
		{
			_logger = logger;
		}

		[HttpGet]
		[Route("school/SchoolOverview/{appId}/{applyingSchoolId}")]
		[Route("school/school/school-overview")]
		public async Task<IActionResult> SchoolOverview(int appId, int applyingSchoolId)
		{
			try
			{
				// TODO MR:- drop appId && applyingSchoolId into cache !!
				var applicationDetails = await LoadAndSetApplicationDetails(appId);
				var school = await _referenceDataRetrievalService.GetSchool(applyingSchoolId);

				return View();
			}
			catch (Exception ex)
			{
				_logger.LogError("SchoolController::SchoolOverview::Exception - {Message}", ex.Message);
				return CatchErrorAndRedirect(ex);
			}
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
					// TODO MR:- ?? concerns casework returns a JSON array, should we do this?
                    // return new JsonResult(Array.Empty<SchoolSearchResultViewModel>());
					return Enumerable.Empty<string>();
				}

				var schoolSearch = new SchoolSearch(searchQuery, string.Empty, string.Empty);
				var schoolSearchResponse = await _referenceDataRetrievalService.SearchSchools(schoolSearch);

				// TODO MR:- ?? concerns casework returns a JSON array, should we do this?
				// return new JsonResult(schoolSearchResponse);

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
				_logger.LogError("SchoolController::Search::Exception - {Message}", ex.Message);

				// TODO MR:- ?? concerns casework returns below which makes sense.
				// Would need to amend controller method to return Task<ActionResult>
				// return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
				_logger.LogError("SchoolController::ReturnSchoolDetailsPartialViewPopulated::Exception - {Message}", ex.Message);
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
