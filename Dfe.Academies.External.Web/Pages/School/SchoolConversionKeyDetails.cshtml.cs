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
																		"/school/ApplicationSelectSchool");

			// TODO MR:- for answer, consume QuestionAndAnswerConstants.NoInfoAnswer if string.IsNullOrWhiteSpace()
			// OR data from API
			heading1.Sections.Add(new (SchoolConversionComponentSectionViewModel.NameOfSchoolSectionName, "TBC"));

			SchoolConversionComponentHeadingViewModel heading2 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationContactDetails,
				"/school/SchoolMainContacts");
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherEmailSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherTelNoSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairEmailSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairTelNoSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsMainContactWhomSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsApproversFullNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsApproversEmailSectionName, "TBC"));

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

			// TODO MR:- QuestionAndAnswerConstants.NoInfoAnswer
			SchoolConversionComponentHeadingViewModel heading4 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationJoinTrustReason,
				"/school/ApplicationJoinTrustReasons");
			heading4.Sections.Add(new(SchoolConversionComponentSectionViewModel.ReasonsForJoiningTrustSectionName, "TBC"));

			SchoolConversionComponentHeadingViewModel heading5 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchoolNameChange,
				"/school/ApplicationChangeSchoolName");
			heading5.Sections.Add(new(SchoolConversionComponentSectionViewModel.NameOfSchoolChangingSectionName, "TBC"));

			var vm = new List<SchoolConversionComponentHeadingViewModel> { heading1, heading2, heading3, heading4, heading5 };

			ViewModel = vm;
		}
	}
}
