using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantModel : BasePageEditModel
{
    private readonly ILogger<ApplicationPreOpeningSupportGrantModel> _logger;
    private readonly IConversionApplicationCreationService _academisationCreationService;

    //// MR:- selected school props for UI rendering
    [BindProperty]
    public int ApplicationId { get; set; }

    [BindProperty]
    public int Urn { get; set; }

    public string SchoolName { get; private set; } = string.Empty;

	public ApplicationTypes ApplicationType { get; private set; }

	//// MR:- VM props to capture data
	// enum - to school / to trust
	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must provide details")]
	public PayFundsTo SchoolSupportGrantFundsPaidTo { get; set; }

	public ApplicationPreOpeningSupportGrantModel(ILogger<ApplicationPreOpeningSupportGrantModel> logger,
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
			var conversionApplication = await LoadAndSetApplicationDetails(appId);

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			// Grab other values from API
			if (selectedSchool != null)
			{
				// TODO MR:- grab data from API endpoint - applicationId && SchoolId combination !
				// data stored against the school ?????????????? not implemented 22/08/2022


				PopulateUiModel(selectedSchool, conversionApplication);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError("School::ApplicationPreOpeningSupportGrantModel::OnGetAsync::Exception - {Message}", ex.Message);
		}
	}

	// TODO MR:- Post()

	public override void PopulateValidationMessages()
	{
		PopulateViewDataErrorsWithModelStateErrors();
	}

	private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? conversionApplication)
	{
		ApplicationType = conversionApplication.ApplicationType;
		ApplicationId = selectedSchool.ApplicationId;
		Urn = selectedSchool.URN;
		SchoolName = selectedSchool.SchoolName;
		// TODO MR:- populate other props from API - not implemented 22/08/2022
		//PayFundsTo = selectedSchool.PayFundsTo;
	}
}

