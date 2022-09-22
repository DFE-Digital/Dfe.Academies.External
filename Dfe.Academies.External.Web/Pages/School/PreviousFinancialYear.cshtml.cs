using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class PreviousFinancialYearModel : BasePageEditModel
	{
		private readonly ILogger<PreviousFinancialYearModel> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;
		public string PFYEndDateFormInputName = "sip_pfyenddate";

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture Pfy data
		
		[BindProperty]
		public string? PFYEndDate { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? PFYEndDateDateYear { get; set; }

		// TODO MR:- optional????
		[BindProperty]
		public decimal? Revenue { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public RevenueType PFYRevenueStatus { get; set; }

		[BindProperty]
		public string? PFYRevenueStatusExplained { get; set; }
		
		// TODO MR:- below, once file upload whoopsy sorted!
		//string? RevenueStatusFileLink = null,

		// TODO MR:- optional????
		[BindProperty]
		public decimal? CapitalCarryForward { get; set; }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public RevenueType PFYCapitalCarryForwardStatus { get; set; }

		[BindProperty]
		public string? PFYCapitalCarryForwardExplained { get; set; }

		// TODO MR:- below, once file upload whoopsy sorted!
		//string? CapitalCarryForwardFileLink = null

		public bool PFYFinancialEndDateError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("PFYFinancialEndDateNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool PFYRevenueStatusExplainedError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("PFYRevenueStatusExplainedNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool HasError
		{
			get
			{
				var bools = new[] { PFYRevenueStatusExplainedError,
				};

				return bools.Any(b => b);
			}
		}

		public PreviousFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
										IReferenceDataRetrievalService referenceDataRetrievalService,
										ILogger<PreviousFinancialYearModel> logger,
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
				ApplicationId = appId;
				Urn = urn;

				// Grab other values from API
				if (selectedSchool != null)
				{
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::PreviousFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync(IFormCollection form)
		{
			// MR:- try and build a date from component parts !!!
			var PFYEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, PFYEndDateFormInputName);
			var PFYEndDateComponentDay = PFYEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
			var PFYEndDateComponentMonth = PFYEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
			var PFYEndDateComponentYear = PFYEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

			var PFYEndDate = BuildDateTime(PFYEndDateComponentDay, PFYEndDateComponentMonth, PFYEndDateComponentYear);

			if (!ModelState.IsValid)
			{
				// error messages component consumes ViewData["Errors"]
				PopulateValidationMessages();
				return Page();
			}

			if (PFYRevenueStatus == RevenueType.Deficit && string.IsNullOrWhiteSpace(PFYRevenueStatusExplained))
			{
				ModelState.AddModelError("PFYRevenueStatusExplainedNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}
			
			if (PFYRevenueStatus == RevenueType.Deficit && PFYEndDate == DateTime.MinValue)
			{
				ModelState.AddModelError("PFYFinancialEndDateNotEntered", "You must give a valid date");
				PopulateValidationMessages();
				PFYEndDateDay = PFYEndDateComponentDay;
				PFYEndDateMonth = PFYEndDateComponentMonth;
				PFYEndDateDateYear = PFYEndDateComponentYear;
				return Page();
			}

			// TODO MR:- other optional validation - PFYCapitalCarryForwardExplained !!

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

				// TODO MR:- call API
				var previousFinancialYear = new SchoolFinancialYear(PFYEndDate, 
																		Revenue, 
																		PFYRevenueStatus, 
																		PFYRevenueStatusExplained, 
																		null,
																		CapitalCarryForward);

				//await _academisationCreationService.ApplicationSchoolLandAndBuildings(previousFinancialYear, ApplicationId, Urn);

				// update temp store for next step - application overview
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("LandAndBuildingsSummary", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::PreviousFinancialYearModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}

		}

		public override void PopulateValidationMessages()
        {
	        PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
		}
	}
}
