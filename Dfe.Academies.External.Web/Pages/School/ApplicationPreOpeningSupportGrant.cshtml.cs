using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantModel : BaseSchoolPageEditModel
{
	[BindProperty]
	public ApplicationTypes ApplicationType { get; set; }

	//// MR:- VM props to capture data
	[BindProperty]
	public PayFundsTo? SchoolSupportGrantFundsPaidTo { get; set; }

	[BindProperty]
	public bool ConfirmSchoolPay { get; set; }

	public bool HasError
	{
		get
		{
			var bools = new[] { SchoolSupportGrantFundsPaidToError };

			return bools.Any(b => b);
		}
	}

	public bool SchoolSupportGrantFundsPaidToError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("SchoolSupportGrantFundsPaidToNotEntered");
		}
	}

	public ApplicationPreOpeningSupportGrantModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationCreationService academisationCreationService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
			academisationCreationService, "ApplicationPreOpeningSupportGrantSummary")
	{}

	/// <summary>
	/// Consuming different PopulateUiModel() NOT from base, so need an overload
	/// </summary>
	/// <param name="urn"></param>
	/// <param name="appId"></param>
	/// <returns></returns>
	public override async Task OnGetAsync(int urn, int appId)
	{
		LoadAndStoreCachedConversionApplication();
		var draftConversionApplication =
			TempDataHelper.GetSerialisedValue<ConversionApplication>(
				TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

		var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
		ApplicationId = appId;
		Urn = urn;

		if (selectedSchool != null)
		{
			PopulateUiModel(selectedSchool, draftConversionApplication);
		}
	}
	
	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return false;
		}

		if (ApplicationType == ApplicationTypes.JoinAMat && !SchoolSupportGrantFundsPaidTo.HasValue)
		{
			ModelState.AddModelError("SchoolSupportGrantFundsPaidToNotEntered", "You must provide details");
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
		PayFundsTo schoolSupportGrantFundsPaidTo = PayFundsTo.School;

		if (ApplicationType == ApplicationTypes.JoinAMat)
		{
			schoolSupportGrantFundsPaidTo = SchoolSupportGrantFundsPaidTo!.Value;
		}
		else
		{
			if (!ConfirmSchoolPay)
			{
				schoolSupportGrantFundsPaidTo = PayFundsTo.Trust;
			}
		}

		// TODO:- do we need to switch value of ConfirmSchoolPay around???????

		return new Dictionary<string, dynamic>
		{
			{ nameof(SchoolApplyingToConvert.SchoolSupportGrantFundsPaidTo), schoolSupportGrantFundsPaidTo },
			{ nameof(SchoolApplyingToConvert.ConfirmPaySupportGrantToSchool), ConfirmSchoolPay }
		};
	}

	///<inheritdoc/>
	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Consume conversionApplication, so need different overload
	/// </summary>
	/// <param name="selectedSchool"></param>
	/// <param name="conversionApplication"></param>
	private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? conversionApplication)
	{
		ApplicationType = conversionApplication.ApplicationType;
		if (conversionApplication.ApplicationType != ApplicationTypes.JoinAMat)
		{
			SchoolSupportGrantFundsPaidTo = PayFundsTo.Trust;
			ConfirmSchoolPay = false;
		}
		else
		{
			SchoolSupportGrantFundsPaidTo = selectedSchool.SchoolSupportGrantFundsPaidTo;
			ConfirmSchoolPay = selectedSchool.ConfirmPaySupportGrantToSchool ?? false;
		}
	}
}

