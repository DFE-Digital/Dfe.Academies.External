using System;
using System.Net;
using Dfe.Academies.Contracts.V4.Trusts;
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
		public async Task<IEnumerable<TrustDto>> Search(string searchQuery)
		{
			try
			{
				_logger.LogInformation("TrustController::Search");

				// Double check search query.
				if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < SearchQueryMinLength)
				{
					return Enumerable.Empty<TrustDto>();
				}

				var trustSearch = new TrustSearch(searchQuery, searchQuery, searchQuery);
				var trusts = await ReferenceDataRetrievalService.GetTrusts(trustSearch);

				if (trusts.Any())
				{
					return trusts;
				}
				else
				{
					return Enumerable.Empty<TrustDto>();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("TrustController::Search::Exception - {Message}", ex.Message);

				return Enumerable.Empty<TrustDto>();
			}
		}

		[HttpPost]
		[Route("trust/ReturnTrustDetailsPartialViewPopulated")]
		[Route("trust/trust/ReturnTrustDetailsPartialViewPopulated")]
		public async Task<IActionResult> ReturnTrustDetailsPartialViewPopulated(TrustDto selectedTrust)
		{
			try
			{
				var vm = new TrustDetailsViewModel(trustName: selectedTrust.Name,
					ukprn: selectedTrust.Ukprn ?? string.Empty,
					trustReference : selectedTrust.ReferenceNumber,
					street: selectedTrust.Address.Street,
					town: selectedTrust.Address.Town,
					fullUkPostcode: selectedTrust.Address.Postcode);

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
