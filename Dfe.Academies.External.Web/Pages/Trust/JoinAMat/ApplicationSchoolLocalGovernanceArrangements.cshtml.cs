using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Dfe.Academies.External.Web.Pages.Trust.JoinAMat
{
	public class ApplicationSchoolLocalGovernanceArrangements : BaseSchoolPageEditModel
	{
		public ApplicationSchoolLocalGovernanceArrangements(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService conversionApplicationCreationService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService, "ApplicationSchoolJoinAMatTrustSummary")
		{
		}

		public string? SelectedTrustName { get; set; }
		
		[BindProperty]
		public SelectOption? ChangesToLaGovernanceOption { get; set; }

		[BindProperty]
		
		public string? ChangesToLaGovernanceExplained { get; set; }
		
		public bool ChangesToLaGovernanceDetailsError => !ModelState.IsValid && ModelState.Keys.Contains("ChangesToLaGovernanceExplainedNotEntered");
		
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public override bool RunUiValidation()
		{
			
			if(ChangesToLaGovernanceOption == null)
			{				
				ModelState.AddModelError("ChangesToLaGovernanceOptionNotSelected", "You must chose an option");
				PopulateValidationMessages();
				return false;
			}

			
			if (ChangesToLaGovernanceOption == SelectOption.Yes && string.IsNullOrWhiteSpace(ChangesToLaGovernanceExplained))
			{
				ModelState.AddModelError("ChangesToLaGovernanceExplainedNotEntered", "You must provide change details");
				PopulateValidationMessages();
				return false;
			}

			

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new();
		}
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{

		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(ApplicationId);

			if (!RunUiValidation())
			{
				return Page();
			}

			if (applicationDetails?.JoinTrustDetails == null)
			{
				throw new InvalidOperationException("Application has no existing trust details");
			}

			ChangesToLaGovernanceExplained =
				ChangesToLaGovernanceOption == SelectOption.Yes ? ChangesToLaGovernanceExplained : null;
			var updatedTrustDetails = applicationDetails.JoinTrustDetails with
			{
				ChangesToTrust = applicationDetails.JoinTrustDetails.ChangesToTrust,
				ChangesToTrustExplained = applicationDetails.JoinTrustDetails.ChangesToTrustExplained,
				ChangesToLaGovernance = ChangesToLaGovernanceOption == SelectOption.Yes,
				ChangesToLaGovernanceExplained = ChangesToLaGovernanceExplained
			};
			
			await ConversionApplicationCreationService.SetExistingTrustDetails(ApplicationId, updatedTrustDetails);

			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			var applicationDetails = await ConversionApplicationRetrievalService.GetApplication(appId);
			SelectedTrustName = applicationDetails.JoinTrustDetails?.TrustName ?? string.Empty;
			ChangesToLaGovernanceOption =
				applicationDetails.JoinTrustDetails.ChangesToLaGovernance.GetEnumValue();
			ChangesToLaGovernanceExplained = applicationDetails.JoinTrustDetails.ChangesToLaGovernanceExplained;
			var selectedSchool = applicationDetails?.Schools.FirstOrDefault(x => x.URN == urn);

			if (selectedSchool != null)
			{
				PopulateUiModel(selectedSchool);
			}

			return Page();
		}
	}
}
