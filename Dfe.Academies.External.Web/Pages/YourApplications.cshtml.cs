using System.Security.Claims;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
	public class HomeModel : BasePageModel
	{
		[BindProperty]
		public List<ConversionApplication> ExistingApplications { get; set; } = new();
		public List<ConversionApplication> CompletedApplications { get; set; } = new();

		private readonly IConversionApplicationRetrievalService _conversionApplications;

		public HomeModel(IConversionApplicationRetrievalService conversionApplications, ILogger<HomeModel> logger)
		{
			_conversionApplications = conversionApplications;
		}

		public async Task OnGet()
		{
			string userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "";

			ExistingApplications = await _conversionApplications.GetPendingApplications(userEmail);

			CompletedApplications = await _conversionApplications.GetCompletedApplications(userEmail);
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}
	}
}
