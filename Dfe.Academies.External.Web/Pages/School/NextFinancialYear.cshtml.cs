using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class NextFinancialYearModel : BasePageEditModel
{
    private readonly ILogger<NextFinancialYearModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;
    public string NFYEndDateFormInputName = "sip_nfyenddate";

    //// MR:- selected school props for UI rendering
    [BindProperty]
    public int ApplicationId { get; set; }

    [BindProperty]
    public int Urn { get; set; }

    public string SchoolName { get; private set; } = string.Empty;

	//// MR:- VM props to capture Pfy data
	[BindProperty]
	public string? NFYEndDate { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateDay { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateMonth { get; set; }

	[BindProperty] // MR:- don't know whether I need this
	public string? NFYEndDateDateYear { get; set; }

	// NFY props

	// TODO MR:- below, once file upload whoopsy sorted!
	//string? CapitalCarryForwardFileLink = null

	// optional validation

	public bool NFYFinancialEndDateError
	{
		get
		{
			if (!ModelState.IsValid && ModelState.Keys.Contains("NFYFinancialEndDateNotEntered"))
			{
				return true;
			}

			return false;
		}
	}
	
	public NextFinancialYearModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
	    IReferenceDataRetrievalService referenceDataRetrievalService,
	    ILogger<NextFinancialYearModel> logger,
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
		    _logger.LogError("School::NextFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
	    }
    }

    public async Task<IActionResult> OnPostAsync(IFormCollection form)
    {
	    // MR:- try and build a date from component parts !!!
	    var NFYEndDateComponents = RetrieveDateTimeComponentsFromDatePicker(form, NFYEndDateFormInputName);
	    var NFYEndDateComponentDay = NFYEndDateComponents.FirstOrDefault(x => x.Key == "day").Value;
	    var NFYEndDateComponentMonth = NFYEndDateComponents.FirstOrDefault(x => x.Key == "month").Value;
	    var NFYEndDateComponentYear = NFYEndDateComponents.FirstOrDefault(x => x.Key == "year").Value;

	    var NFYEndDate = BuildDateTime(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);

	    if (!ModelState.IsValid)
	    {
		    // error messages component consumes ViewData["Errors"]
		    PopulateValidationMessages();
		    // MR:- date input disappears without below !!
		    RePopDatePickerModel(NFYEndDateComponentDay, NFYEndDateComponentMonth, NFYEndDateComponentYear);
		    return Page();
	    }

		// TODO MR:- optional validaTION

		try
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var previousFinancialYear = new SchoolFinancialYear(NFYEndDate,
				//Revenue,
				//PFYRevenueStatus,
				//PFYRevenueStatusExplained,
				//null,
				//CapitalCarryForward,
				//PFYCapitalCarryForwardStatus,
				//PFYCapitalCarryForwardExplained,
				null);

			await _academisationCreationService.ApplicationSchoolNextFinancialYear(previousFinancialYear, ApplicationId, Urn);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("FinancesReview", new { appId = ApplicationId, urn = Urn });
		}
		catch (Exception ex)
		{
			_logger.LogError("School::NextFinancialYearModel::OnPostAsync::Exception - {Message}", ex.Message);
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

		// TODO MR:- NFY one's!!
	}

	private void RePopDatePickerModel(string nfyEndDateComponentDay, string nfyEndDateComponentMonth, string nfyEndDateComponentYear)
	{
		NFYEndDateDay = nfyEndDateComponentDay;
		NFYEndDateMonth = nfyEndDateComponentMonth;
		NFYEndDateDateYear = nfyEndDateComponentYear;
	}
}

