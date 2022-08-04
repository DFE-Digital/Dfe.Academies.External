using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.Academies.External.Web.Controllers
{
	// TODO MR:- baseController
	// [Authorize]
	public class TrustController : Controller
	{
		private const int SearchQueryMinLength = 3;
		private readonly ILogger<TrustController> _logger;
		private readonly IReferenceDataRetrievalService _referenceDataRetrievalService;

		public TrustController(ILogger<TrustController> logger,
			IReferenceDataRetrievalService referenceDataRetrievalService)
		{
			_logger = logger;
			_referenceDataRetrievalService = referenceDataRetrievalService;
		}

		[HttpGet]
		[Route("trust/search")]
		[Route("trust/trust/search")]
		public async Task<IEnumerable<string>> Search(string searchQuery)
		{
			try
			{
				_logger.LogInformation("TrustController::Search");

				// Double check search query.
				if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < SearchQueryMinLength)
				{
					return Enumerable.Empty<string>();
				}

				var trustSearch = new TrustSearch(searchQuery, searchQuery, searchQuery);
				var trustSearchResponse = await _referenceDataRetrievalService.GetTrusts(trustSearch);

				if (trustSearchResponse.Any())
				{
					return trustSearchResponse.Select(x => x.GroupName).AsEnumerable();
				}
				else
				{
					return Enumerable.Empty<string>();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("TrustController::Search::Exception - {Message}", ex.Message);

				return Enumerable.Empty<string>();
			}
		}

		[HttpGet]
		[Route("trust/ReturnTrustDetailsPartialViewPopulated")]
		[Route("trust/trust/ReturnSchoolDetailsPartialViewPopulated")]
		public async Task<IActionResult> ReturnTrustDetailsPartialViewPopulated(string selectedTrust)
		{
			try
			{
				// Remove whitespace and trailing ) then split removing empty entries
				var schoolSplit = selectedTrust
					.Trim()
					.Replace(")", string.Empty)
					.Split('(', StringSplitOptions.RemoveEmptyEntries);

				int ukprn = Convert.ToInt32(schoolSplit[^1]);
				var result = await _referenceDataRetrievalService.GetTrustByUkPrn(ukprn.ToString());

				// API returns a List<> for a singular Get, for some unknown reason!
				var trust = result.FirstOrDefault();

				var vm = new TrustDetailsViewModel(trustName: trust.GroupName,
					ukprn: ukprn,
					street: trust.TrustAddress.Street,
					town: trust.TrustAddress.Town,
					fullUkPostcode: trust.TrustAddress.Postcode);

				return PartialView("_TrustDetails", vm);
			}
			catch (Exception ex)
			{
				_logger.LogError("TrustController::ReturnTrustDetailsPartialViewPopulated::Exception - {Message}", ex.Message);
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
