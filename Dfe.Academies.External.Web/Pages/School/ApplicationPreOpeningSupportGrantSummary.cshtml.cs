using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantSummaryModel : BaseSchoolSummaryPageModel
{
	//// MR:- VM props to show school conversion data
	public List<ApplicationPreOpeningSupportGrantHeadingViewModel> ViewModel { get; set; } = new();

	public ApplicationPreOpeningSupportGrantSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService)
		: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	{
	}

	///<inheritdoc/>
	public override bool RunUiValidation()
	{
		// does not apply on this page
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
		// does not apply on this page
		return new();
	}

	///<inheritdoc/>
	public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	{
		ApplicationPreOpeningSupportGrantHeadingViewModel heading1 = new(ApplicationPreOpeningSupportGrantHeadingViewModel.Heading,
			"/school/ApplicationPreOpeningSupportGrant"){
			Status = !string.IsNullOrEmpty(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
				SchoolConversionComponentStatus.Complete
				: SchoolConversionComponentStatus.NotStarted
		};
		
		heading1.Sections.Add(new(
			ApplicationPreOpeningSupportGrantSectionViewModel.FundsSchoolOrTrust,
			(string.IsNullOrWhiteSpace(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
				QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantFundsPaidTo?.GetDescription()) ?? string.Empty
		));

		var vm = new List<ApplicationPreOpeningSupportGrantHeadingViewModel> { heading1 };

		ViewModel = vm;
	}
}
