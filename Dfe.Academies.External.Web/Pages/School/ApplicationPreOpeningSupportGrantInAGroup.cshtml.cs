using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantInAGroupModel : BaseSchoolPageEditModel
{
	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must provide details")]
	public SelectOption? JoinInAGroup { get; set; }

	public ApplicationPreOpeningSupportGrantInAGroupModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationService academisationCreationService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
			academisationCreationService, "ApplicationPreOpeningSupportGrant")
	{}

	/// <summary>
	/// Consuming different PopulateUiModel() NOT from base, so need an overload
	/// </summary>
	/// <param name="urn"></param>
	/// <param name="appId"></param>
	/// <returns></returns>
	public override async Task<ActionResult> OnGetAsync(int urn, int appId)
	{
		LoadAndStoreCachedConversionApplication();
		var draftConversionApplication =
			TempDataHelper.GetSerialisedValue<ConversionApplication>(
				TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		// check user access
		var checkStatus = await CheckApplicationPermission(appId);

		if (checkStatus is ForbidResult)
		{
			return RedirectToPage("../ApplicationAccessException");
		}

		var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
		ApplicationId = appId;
		Urn = urn;

		if (selectedSchool != null)
		{
			PopulateUiModel(selectedSchool);
		}

		return Page();
	}
	
	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return false;
		}

		return true;
	}

	///<inheritdoc/>
	public override void PopulateValidationMessages()
	{
		PopulateViewDataErrorsWithModelStateErrors();
	}

	///<inheritdoc/>
	public override Dictionary<string, dynamic> PopulateUpdateDictionary()
	{
		return new Dictionary<string, dynamic>
		{
			{ nameof(SchoolApplyingToConvert.SchoolSupportGrantJoiningInAGroup), JoinInAGroup == SelectOption.Yes},
		};
	}

	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		JoinInAGroup = selectedSchool.SchoolSupportGrantJoiningInAGroup.GetEnumValue();
	}
}

