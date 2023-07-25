using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationSchoolConsultationModel : BaseSchoolPageEditModel
{
	// MR:- VM props to capture data
	[BindProperty]
	[RequiredEnum(ErrorMessage = "You must choose an option")]
	public SelectOption? SchoolConsultationStakeholders { get; set; }

	[BindProperty]
	public string? SchoolConsultationStakeholdersConsult { get; set; }

	public bool HasError
	{
		get
		{
			var bools = new[] { SchoolConsultationStakeholdersConsultError };

			return bools.Any(b => b);
		}
	}

	public bool SchoolConsultationStakeholdersConsultError
	{
		get
		{
			return !ModelState.IsValid && ModelState.Keys.Contains("SchoolConsultationStakeholdersConsultNotEntered");
		}
	}

	public ApplicationSchoolConsultationModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationService academisationCreationService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
			academisationCreationService, "ApplicationSchoolConsultationSummary")
	{}

	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		if (!ModelState.IsValid)
		{
			PopulateValidationMessages();
			return false;
		}

		if (SchoolConsultationStakeholders == SelectOption.No && string.IsNullOrWhiteSpace(SchoolConsultationStakeholdersConsult))
		{
			ModelState.AddModelError("SchoolConsultationStakeholdersConsultNotEntered", "You must provide details");
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
		// if school HAS consulted blank out 'SchoolConsultationStakeholdersConsult'
		if (SchoolConsultationStakeholders == SelectOption.No)
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders), false },
				{ nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders), SchoolConsultationStakeholdersConsult! }
			};
		}
		else
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders), true },
				{ nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders), null }
			};
		}
	}

	///<inheritdoc/>
	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		SchoolConsultationStakeholders = selectedSchool.SchoolHasConsultedStakeholders.GetEnumValue();
		SchoolConsultationStakeholdersConsult = selectedSchool.SchoolPlanToConsultStakeholders;
	}
}
