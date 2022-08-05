using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;

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

				var schoolSearch = new TrustSearch(searchQuery, searchQuery, searchQuery);
				var schoolSearchResponse = await _referenceDataRetrievalService.GetTrusts(schoolSearch);

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
				var trusts = await _referenceDataRetrievalService.GetTrustByUkPrn(ukprn.ToString());

				// MR:- search returns list<> so need to do below:-
				var trust = trusts.FirstOrDefault();

				var vm = new TrustDetailsViewModel(trustName: trust.GroupName,
					ukprn: ukprn,
					street: trust.TrustAddress.Street,
					town: trust.TrustAddress.Town,
					fullUkPostcode: trust.TrustAddress.Postcode);

				return PartialView("_TrustDetails", vm);
			}
			catch (Exception ex)
			{
				_logger.LogError("SchoolController::ReturnTrustDetailsPartialViewPopulated::Exception - {Message}", ex.Message);
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
