using System.Reflection;
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

	public ApplicationStatus ApplicationStatus { get; private set; }

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
		var applicationDetails = ConversionApplicationRetrievalService.GetApplication(ApplicationId).Result;
		ApplicationStatus = applicationDetails.ApplicationStatus;
		ApplicationPreOpeningSupportGrantHeadingViewModel heading1;

		if (ApplicationType == ApplicationTypes.JoinAMat)
		{
			heading1 = new(ApplicationPreOpeningSupportGrantHeadingViewModel.Heading, "/school/ApplicationPreOpeningSupportGrantInAGroup")
			{
				Status = !string.IsNullOrEmpty(selectedSchool.SchoolSupportGrantJoiningInAGroup.ToString()) ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(new(
				ApplicationPreOpeningSupportGrantSectionViewModel.WillYouJoinTheTrustInAGroup,
				(!selectedSchool.SchoolSupportGrantJoiningInAGroup.HasValue ?
					QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantJoiningInAGroup.GetStringDescription()) ?? string.Empty));

			if (selectedSchool.SchoolSupportGrantJoiningInAGroup.HasValue && selectedSchool.SchoolSupportGrantJoiningInAGroup.Value)
			{
				heading1.Sections.Add(new(
					ApplicationPreOpeningSupportGrantSectionViewModel.FundsSchoolOrTrust,
					(string.IsNullOrWhiteSpace(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
						QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantFundsPaidTo?.GetDescription()) ?? string.Empty));
				heading1.Sections.Add(new(
					ApplicationPreOpeningSupportGrantSectionViewModel.BankAccountDetails,
					(!selectedSchool.SchoolSupportGrantBankDetailsProvided.HasValue ?
						QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantBankDetailsProvided.GetStringDescription()) ?? string.Empty));
			}
		}
		else
		{
			// if application type is form a mat maintain current funtionality
			heading1 = new(ApplicationPreOpeningSupportGrantHeadingViewModel.Heading, "/school/ApplicationPreOpeningSupportGrant")
			{
				Status = !string.IsNullOrEmpty(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(new(
				ApplicationPreOpeningSupportGrantSectionViewModel.FundsSchoolOrTrustFormAMat,
				(string.IsNullOrWhiteSpace(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
					QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantFundsPaidTo?.GetDescription()) ?? string.Empty));
		}

		var vm = new List<ApplicationPreOpeningSupportGrantHeadingViewModel> { heading1 };

		ViewModel = vm;
	}
}
