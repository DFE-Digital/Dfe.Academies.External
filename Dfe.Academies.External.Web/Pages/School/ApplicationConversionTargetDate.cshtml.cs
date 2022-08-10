using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationConversionTargetDateModel : BasePageEditModel
    {
	    private readonly ILogger<ApplicationConversionTargetDateModel> _logger;
	    private readonly IConversionApplicationCreationService _academisationCreationService;
	    private const string NextStepPage = "/ConversionRationale";

		//// MR:- selected school props for UI rendering
		[BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; private set; }

	    public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture data

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption TargetDateDifferent { get; set; }

		/// <summary>
		/// Full 'Date' representation of date selected
		/// i.e. day + month + year entered !
		/// </summary>
		[BindProperty]
		public DateTime? TargetDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TargetDateYear { get; set; }

		[BindProperty]
		public string? TargetDateExplained { get; set; }

		public bool HasError {
			get
			{
				var bools = new[] { SchoolConversionTargetDateError, TargetDateExplainedError };

				return bools.Any(b => b);
			}
		}

		public bool SchoolConversionTargetDateError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("SchoolConversionTargetDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool TargetDateExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("TargetDateExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public ApplicationConversionTargetDateModel(ILogger<ApplicationConversionTargetDateModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			try
			{
				LoadAndStoreCachedConversionApplication();

				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab existing data from API endpoint to populate VM - applicationId && SchoolId combination !


					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				return Page();
			}

			if (TargetDateDifferent == SelectOption.Yes && !TargetDate.HasValue)
			{
				ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must select a conversion date");
				PopulateValidationMessages();
				return Page();
			}

			if (TargetDateDifferent == SelectOption.Yes && string.IsNullOrWhiteSpace(TargetDateExplained))
			{
				ModelState.AddModelError("TargetDateExplainedNotEntered", "You must explain why you want to convert on this date");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// binding var check
				var x = TargetDateDifferent;
				var y = TargetDate;
				var z = TargetDateExplained;

				// TODO MR:- call API endpoint to log data
				// await _academisationCreationService.UpdateSchoolConversionDate(TargetDate, TargetDateExplained);

				// update temp store for next step
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				// need to go onto next step in process 'reasons for conversion page'
				return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::ApplicationConversionTargetDateModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			ViewData["Errors"] = ConvertModelStateToDictionary();

			if (!ModelState.IsValid)
			{
				foreach (var modelStateError in ConvertModelStateToDictionary())
				{
					// MR:- add friendly message for validation summary
					if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
					{
						this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
					}
				}
			}
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			ApplicationId = selectedSchool.ApplicationId;
			Urn = selectedSchool.URN;
			SchoolName = selectedSchool.SchoolName;
			// TODO MR:- bind below from API data
			//TargetDateDifferent = ;
			//TargetDate = ;
			//TargetDateExplained = ;
		}
	}
}
