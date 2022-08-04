using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dfe.Academies.External.Web.Controllers
{
	// TODO MR:- baseController
	// [Authorize]
	public class TrustController : BaseController
	{
		private readonly ILogger<TrustController> _logger;

		public TrustController(ILogger<TrustController> logger, IReferenceDataRetrievalService referenceDataRetrievalService) : base(referenceDataRetrievalService)
		{
			_logger = logger;
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
				var trustSearchResponse = await ReferenceDataRetrievalService.GetTrusts(trustSearch);

				if (trustSearchResponse.Any())
				{
					return trustSearchResponse.Select(x => x.DisplayName).AsEnumerable();
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

		/// <summary>
		/// https://localhost:44350/trust/trust/ReturnTrustDetailsPartialViewPopulated?selectedTrust=THE%20STAFFORDSHIRE%20SCHOOLS%20MULTI%20ACADEMY%20TRUST
		/// </summary>
		/// <param name="selectedTrust"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("trust/ReturnTrustDetailsPartialViewPopulated")]
		[Route("trust/trust/ReturnTrustDetailsPartialViewPopulated")]
		public async Task<IActionResult> ReturnTrustDetailsPartialViewPopulated(string selectedTrust)
		{
			try
			{
				// Remove whitespace and trailing ) then split removing empty entries
				var trustSplit = selectedTrust
					.Trim()
					.Replace(")", string.Empty)
					.Split('(', StringSplitOptions.RemoveEmptyEntries);

				int ukprn = Convert.ToInt32(trustSplit[^1]);
				var result = await ReferenceDataRetrievalService.GetTrustByUkPrn(ukprn.ToString());

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
