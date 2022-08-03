using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.Academies.External.Web.Controllers
{
	// TODO MR:- [Authorize]
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

				// TODO MR:- if searchQuery = numeric it's a urn ?

				var schoolSearch = new SchoolSearch(searchQuery, string.Empty, string.Empty);
				var schoolSearchResponse = await ReferenceDataRetrievalService.SearchSchools(schoolSearch);

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
				var result = await ReferenceDataRetrievalService.GetSchool(urn);

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

		//[HttpGet]
		//[Route("school/SchoolOverview/{appId}/{schoolUrn}")]
		//[Route("school/school/school-overview")]
		//public async Task<IActionResult> SchoolOverview(int appId, int schoolUrn)
		//{
		//	try
		//	{
		//		await LoadAndSetApplicationDetails(appId);
		//		var school = await ReferenceDataRetrievalService.GetSchool(schoolUrn);
		//		// https://stackoverflow.com/questions/46772632/how-pass-objects-from-one-page-to-another-on-asp-net-core-with-razor-pages
		//		return RedirectToPage("SchoolOverview", "SchoolApplication", 
		//			new { urn = schoolUrn, applicationId = appId });
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError("SchoolController::SchoolOverview::Exception - {Message}", ex.Message);
		//		return CatchErrorAndRedirect(ex);
		//	}
		//}
	}
}
