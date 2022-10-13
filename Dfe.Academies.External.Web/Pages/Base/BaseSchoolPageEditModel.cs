using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.Base;

public abstract class BaseSchoolPageEditModel : BasePageEditModel
{
	public readonly IConversionApplicationCreationService ConversionApplicationCreationService;

	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public int Urn { get; set; }

	public string SchoolName { get; private set; } = string.Empty;

	public string NextStepPage { get; private set; }

	protected BaseSchoolPageEditModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
									IReferenceDataRetrievalService referenceDataRetrievalService,
									IConversionApplicationCreationService conversionApplicationCreationService,
									string nextStepPage) 
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	{
		ConversionApplicationCreationService = conversionApplicationCreationService;
		NextStepPage = nextStepPage;
	}

	//1) Create OnGetAsync() func in new base class - call PopulateUiModel() method, that will be overridden in each page
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
				SchoolName = selectedSchool.SchoolName;
			}
		}
		catch (Exception ex)
		{
			// TODO MR:- fix !!
			//_logger.LogError("School::CurrentFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
		}
	}


	//2) Create OnPostAsync() func in new base class - call PopulateUiModel() method, that will be overridden in each page
	public async Task<IActionResult> OnPostAsync()
	{
		try
		{
			//// grab draft application from temp= null
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();

			await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage(NextStepPage, new { appId = ApplicationId, urn = Urn });
		}
		catch (Exception ex)
		{
			// TODO MR:- fix !!
			//_logger.LogError("School::CurrentFinancialYearModel::OnPostAsync::Exception - {Message}", ex.Message);
			return Page();
		}
	}

	/// <summary>
	/// take school data from API and populate UI controls
	/// </summary>
	/// <param name="selectedSchool"></param>
	public abstract void PopulateUiModel(SchoolApplyingToConvert selectedSchool);
}
