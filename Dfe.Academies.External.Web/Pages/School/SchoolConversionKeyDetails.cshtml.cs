using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.Pages.School
{
	/// <summary>
	/// MR:- clone of ConversionKeyDetailsReview.cshtml - A2C-SIP
	/// </summary>
	public class SchoolConversionKeyDetailsModel : BasePageEditModel
	{
		private readonly ILogger<SchoolConversionKeyDetailsModel> _logger;

		//// MR:- selected school props for UI rendering
		[BindProperty] 
		public int ApplicationId { get; set; }

		[BindProperty] 
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to show school conversion data
		public List<SchoolConversionComponentHeadingViewModel> ViewModel { get; set; } = new();
		
		public SchoolConversionKeyDetailsModel(ILogger<SchoolConversionKeyDetailsModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
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
				_logger.LogError("School::SchoolConversionKeyDetailsModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

			SchoolConversionComponentHeadingViewModel heading1 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchool,
																		"/school/ApplicationSelectSchool")
			{
				Status = selectedSchool.URN !=0 ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading1.Sections.Add(
				new (SchoolConversionComponentSectionViewModel.NameOfSchoolSectionName,
					SchoolName));
			
			SchoolConversionComponentHeadingViewModel heading2 = 
				new(SchoolConversionComponentHeadingViewModel.HeadingApplicationContactDetails,
				"/school/SchoolMainContacts")
				{ Status = !String.IsNullOrEmpty(selectedSchool.SchoolConversionContactHeadName) ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
				};

			// TODO MR:- set from API data
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherNameSectionName, 
					selectedSchool.SchoolConversionContactHeadName ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherEmailSectionName, 
					selectedSchool.SchoolConversionContactHeadEmail ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherTelNoSectionName, 
					selectedSchool.SchoolConversionContactHeadTel ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairNameSectionName, 
					selectedSchool.SchoolConversionContactChairName ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairEmailSectionName, 
					selectedSchool.SchoolConversionContactChairEmail ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsChairTelNoSectionName, 
					selectedSchool.SchoolConversionContactChairTel ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsMainContactRoleSectionName, selectedSchool.SchoolConversionContactRole ?? QuestionAndAnswerConstants.NoAnswer )
				);
			if (!string.IsNullOrEmpty(selectedSchool.SchoolConversionContactRole) && selectedSchool.SchoolConversionContactRole.Equals(MainConversionContact.Other.ToString()))
			{
				heading2.Sections.Add(
					new(
						SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherNameSectionName, 
						selectedSchool.SchoolConversionMainContactOtherName ?? QuestionAndAnswerConstants.NoAnswer )
				);
				
				heading2.Sections.Add(
					new(
						SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherEmailSectionName, 
						selectedSchool.SchoolConversionMainContactOtherEmail?? QuestionAndAnswerConstants.NoAnswer )
				);
				
				heading2.Sections.Add(
					new(
						SchoolConversionComponentSectionViewModel.ContactDetailsMainContactOtherTelephoneSectionName, 
						selectedSchool.SchoolConversionMainContactOtherTelephone ?? QuestionAndAnswerConstants.NoAnswer )
				);
			}
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsApproversFullNameSectionName, 
					selectedSchool.SchoolConversionApproverContactName ?? QuestionAndAnswerConstants.NoAnswer)
				);
			heading2.Sections.Add(
				new(
					SchoolConversionComponentSectionViewModel.ContactDetailsApproversEmailSectionName, 
					selectedSchool.SchoolConversionApproverContactEmail ?? QuestionAndAnswerConstants.NoAnswer)
				);

			SchoolConversionComponentHeadingViewModel heading3 = 
				new(SchoolConversionComponentHeadingViewModel.HeadingApplicationPreferredDateForConversion,
				"/school/ApplicationConversionTargetDate") 
				{ Status = selectedSchool.SchoolConversionTargetDateSpecified.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
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
							(selectedSchool.SchoolConversionTargetDate.HasValue ? 
								selectedSchool.SchoolConversionTargetDate.Value.ToString("dd/MM/yyyy") 
								: QuestionAndAnswerConstants.NoAnswer)
						),
						new SchoolLandAndBuildingsSummarySectionViewModel(
							"Explain why you want to convert on this date",
							(!string.IsNullOrWhiteSpace(selectedSchool.SchoolConversionTargetDateExplained) ?
								selectedSchool.SchoolConversionTargetDateExplained
								: QuestionAndAnswerConstants.NoAnswer)
						)
					}
				});

			// TODO MR:- ApplicationJoinTrustReasons not in API yet - 08/09/2022
			SchoolConversionComponentHeadingViewModel heading4 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationJoinTrustReason,
				"/school/ApplicationJoinTrustReasons");
			heading4.Sections.Add(new(SchoolConversionComponentSectionViewModel.ReasonsForJoiningTrustSectionName, "TBC"));

			SchoolConversionComponentHeadingViewModel heading5 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchoolNameChange,
				"/school/ApplicationChangeSchoolName")
			{
				Status = selectedSchool.ConversionChangeNamePlanned.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			heading5.Sections.Add(new(
								SchoolConversionComponentSectionViewModel.NameOfSchoolChangingSectionName,
								(!string.IsNullOrWhiteSpace(selectedSchool.ProposedNewSchoolName) ?
									selectedSchool.ProposedNewSchoolName
									: QuestionAndAnswerConstants.NoAnswer)
								)
			);

			var vm = new List<SchoolConversionComponentHeadingViewModel> { heading1, heading2, heading3, heading4, heading5 };

			ViewModel = vm;
		}
	}
}
