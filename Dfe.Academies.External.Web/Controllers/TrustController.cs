using System.Net;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers
{
	[Authorize]
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
				var trusts = await ReferenceDataRetrievalService.GetTrusts(trustSearch);

				if (trusts.Any())
				{
					return trusts.Select(x =>
					{
						return $"{x.Name} ({x.Ukprn})";
					});
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
				var trust = await ReferenceDataRetrievalService.GetTrustByUkPrn(ukprn.ToString());

				var vm = new TrustDetailsViewModel(trustName: trust.Name,
					ukprn: ukprn,
					trustReference : trust.ReferenceNumber,
					street: trust.Address.Street,
					town: trust.Address.Town,
					fullUkPostcode: trust.Address.Postcode);

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
