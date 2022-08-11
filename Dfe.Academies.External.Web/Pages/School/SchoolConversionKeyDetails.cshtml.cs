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
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
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

				// Grab other values from API
				if (selectedSchool != null)
				{
					// TODO MR:- grab data from API endpoint - applicationId && SchoolId combination !


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
			ViewData["Errors"] = ConvertModelStateToDictionary();

			if (!ModelState.IsValid)
			{
				foreach (var modelStateError in ConvertModelStateToDictionary())
				{
					// MR:- add friendly message for validation summary
					if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
					{
						this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
					}
				}
			}
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			ApplicationId = selectedSchool.ApplicationId;
			Urn = selectedSchool.URN;
			SchoolName = selectedSchool.SchoolName;
			// TODO MR:- sort out sections - setup VM from what we get back from API

			SchoolConversionComponentHeadingViewModel heading1 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationSchool,
																		"/school/ApplicationSelectSchool");

			// TODO MR:- fo answer, consume QuestionAndAnswerConstants.NoInfoAnswer if string.isnullorempty()
			heading1.Sections.Add(new (SchoolConversionComponentSectionViewModel.NameOfSchoolSectionName, "TBC"));

			SchoolConversionComponentHeadingViewModel heading2 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationContactDetails,
				"/school/ContactDetails");
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherEmailSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsHeadteacherTelNoSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairEmailSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsChairTelNoSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsMainContactWhomSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsApproversFullNameSectionName, "TBC"));
			heading2.Sections.Add(new(SchoolConversionComponentSectionViewModel.ContactDetailsApproversEmailSectionName, "TBC"));

			SchoolConversionComponentHeadingViewModel heading3 = new(SchoolConversionComponentHeadingViewModel.HeadingApplicationPreferredDateForConversion,
				"/school/ApplicationConversionTargetDate");
			heading3.Sections.Add(new(SchoolConversionComponentSectionViewModel.ApplicationConversionTargetDateSectionName, "TBC"));

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
