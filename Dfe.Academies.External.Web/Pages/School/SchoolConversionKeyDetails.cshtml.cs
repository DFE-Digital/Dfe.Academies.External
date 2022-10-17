using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	/// <summary>
	/// MR:- clone of ConversionKeyDetailsReview.cshtml - A2C-SIP
	/// </summary>
	public class SchoolConversionKeyDetailsModel : BaseSchoolSummaryPageModel
	{
		//// MR:- VM props to show school conversion data
		public List<SchoolConversionComponentHeadingViewModel> ViewModel { get; set; } = new();

		public SchoolConversionKeyDetailsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
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
			SchoolConversionComponentHeadingViewModel heading1 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchool,
																		"/school/ApplicationSelectSchool")
			{
				Status = selectedSchool.URN != 0 ?
					SchoolConversionComponentStatus.Incomplete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(
				new(SchoolConversionComponentSectionViewModel.NameOfSchoolSectionName,
					SchoolName));

			SchoolConversionComponentHeadingViewModel heading2 =
				new(SchoolConversionComponentHeadingViewModel.HeadingApplicationContactDetails,
				"/school/SchoolMainContacts")
				{
					Status = !string.IsNullOrEmpty(selectedSchool.SchoolConversionContactHeadName) ?
					SchoolConversionComponentStatus.Complete : SchoolConversionComponentStatus.NotStarted
				};
			
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherNameSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactHeadName) ?
					selectedSchool.SchoolConversionContactHeadName : QuestionAndAnswerConstants.NoInfoAnswer
					));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherEmailSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactHeadEmail) ?
						selectedSchool.SchoolConversionContactHeadEmail : QuestionAndAnswerConstants.NoInfoAnswer
						));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherTelNoSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactHeadTel) ?
						selectedSchool.SchoolConversionContactHeadTel : QuestionAndAnswerConstants.NoInfoAnswer
						));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairNameSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactChairName) ?
						selectedSchool.SchoolConversionContactChairName : QuestionAndAnswerConstants.NoInfoAnswer
						));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairEmailSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactChairEmail) ?
						selectedSchool.SchoolConversionContactChairEmail : QuestionAndAnswerConstants.NoInfoAnswer
						));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairTelNoSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionContactChairTel) ?
						selectedSchool.SchoolConversionContactChairTel : QuestionAndAnswerConstants.NoInfoAnswer
						));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsMainContactRoleSectionName,
					!string.IsNullOrEmpty(selectedSchool.SchoolConversionContactRole) 
						? selectedSchool.SchoolConversionContactRole.ToEnum<MainConversionContact>().GetDescription() 
						: QuestionAndAnswerConstants.NoInfoAnswer
				));

			// check we have an 'Other' contact role before outputting sub q's
			if (!string.IsNullOrEmpty(selectedSchool.SchoolConversionContactRole) 
			    && selectedSchool.SchoolConversionContactRole.Equals(MainConversionContact.Other.ToString()))
			{
				heading2.Sections.Add(
					new(SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherNameSectionName,
						!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionMainContactOtherName) ?
							selectedSchool.SchoolConversionMainContactOtherName : QuestionAndAnswerConstants.NoInfoAnswer
				));

				heading2.Sections.Add(
					new(
						SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherEmailSectionName,
						!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionMainContactOtherEmail) ?
							selectedSchool.SchoolConversionMainContactOtherEmail : QuestionAndAnswerConstants.NoInfoAnswer
						));

				heading2.Sections.Add(
					new(
						SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherTelephoneSectionName,
						!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionMainContactOtherTelephone) ?
							selectedSchool.SchoolConversionMainContactOtherTelephone : QuestionAndAnswerConstants.NoInfoAnswer
						));
			}
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsApproversFullNameSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionApproverContactName) ?
						selectedSchool.SchoolConversionApproverContactName : QuestionAndAnswerConstants.NoInfoAnswer
					));
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsApproversEmailSectionName,
					!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionApproverContactEmail) ?
						selectedSchool.SchoolConversionApproverContactEmail : QuestionAndAnswerConstants.NoInfoAnswer
					));

			SchoolConversionComponentHeadingViewModel heading3 =
				new(SchoolConversionComponentHeadingViewModel.HeadingApplicationPreferredDateForConversion,
				"/school/ApplicationConversionTargetDate")
				{
					Status = selectedSchool.SchoolConversionTargetDateSpecified.HasValue ?
					SchoolConversionComponentStatus.Complete : SchoolConversionComponentStatus.NotStarted
				};

			heading3.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ApplicationConversionTargetDateSectionName,
					selectedSchool.SchoolConversionTargetDateSpecified.GetStringDescription())
				{
					SubQuestionAndAnswers = new()
					{
						new SchoolLandAndBuildingsSummarySectionViewModel(
							"Preferred date",
							selectedSchool.SchoolConversionTargetDate.HasValue ?
								selectedSchool.SchoolConversionTargetDate.Value.ToString("dd/MM/yyyy")
								: QuestionAndAnswerConstants.NoAnswer
						),
						new SchoolLandAndBuildingsSummarySectionViewModel(
							"Explain why you want to convert on this date",
							!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionTargetDateExplained) ?
								selectedSchool.SchoolConversionTargetDateExplained
								: QuestionAndAnswerConstants.NoAnswer
						)
					}
				});

			SchoolConversionComponentHeadingViewModel heading4 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationJoinTrustReason,
				"/school/ApplicationJoinTrustReasons")
			{
				Status = !string.IsNullOrWhiteSpace(selectedSchool.ApplicationJoinTrustReason) ?
					SchoolConversionComponentStatus.Complete : SchoolConversionComponentStatus.NotStarted
			};
			heading4.Sections.Add(new(SchoolConversionComponentSectionViewModel.ReasonsForJoiningTrustSectionName,
				!string.IsNullOrWhiteSpace(selectedSchool.ApplicationJoinTrustReason) ?
					selectedSchool.ApplicationJoinTrustReason
					: QuestionAndAnswerConstants.NoInfoAnswer
				));

			SchoolConversionComponentHeadingViewModel heading5 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchoolNameChange,
				"/school/ApplicationChangeSchoolName")
			{
				Status = selectedSchool.ConversionChangeNamePlanned.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading5.Sections.Add(new(
								SchoolConversionComponentSectionViewModel.NameOfSchoolChangingSectionName,
								selectedSchool.ConversionChangeNamePlanned.GetStringDescription()
			));

			var vm = new List<SchoolConversionComponentHeadingViewModel> { heading1, heading2, heading3, heading4, heading5 };

			ViewModel = vm;
		}
	}
}
