using System.Linq;
using System.Net;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dfe.Academies.External.Web.Controllers
{
	[Authorize]
	public class SchoolController : BaseController
	{
		private readonly ILogger<SchoolController> _logger;

		public SchoolController(ILogger<SchoolController> logger, IReferenceDataRetrievalService referenceDataRetrievalService) : base(referenceDataRetrievalService)
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
				return Enumerable.Empty<string>();
			}

			// If searchQuery is numeric (6 digits), treat it as a URN search
			string name = string.Empty;
			string urn = string.Empty;
			
			if (int.TryParse(searchQuery, out _) && searchQuery.Length == 6)
			{
				// It's a 6-digit number, treat as URN
				urn = searchQuery;
			}
			else
			{
				// It's a text search, treat as name
				name = searchQuery;
			}

			var schoolSearch = new SchoolSearch(name, urn, string.Empty);
			var schoolSearchResponse = await ReferenceDataRetrievalService.SearchSchools(schoolSearch);

			if (!schoolSearchResponse.Any())
			{
				return Enumerable.Empty<string>();
			}

			// Format results and optimize ordering for better search relevance
			var results = schoolSearchResponse
				.Select(x => new { 
					DisplayText = $"{x.Name} ({x.Urn})", 
					Name = x.Name ?? string.Empty,
					Urn = x.Urn ?? string.Empty,
					SearchQuery = searchQuery.ToLowerInvariant()
				})
				.OrderBy(x => GetSearchRelevanceOrder(x.Name, x.Urn, searchQuery))
				.ThenBy(x => x.Name) // Secondary sort by name for consistent ordering
				.Select(x => x.DisplayText)
				.ToList();

			return results;
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

		private static int GetSearchRelevanceOrder(string name, string urn, string searchQuery)
		{
			// Prioritize exact matches
			if (name.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
				return 0;
			// Then URN exact matches
			if (urn.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
				return 1;
			// Then name starts with query
			if (name.StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase))
				return 2;
			// Then name contains query
			if (name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
				return 3;
			// Then URN contains query
			if (urn.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
				return 4;
			// Everything else
			return 5;
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

				_logger.LogInformation(JsonConvert.SerializeObject(result));

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
