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
		private readonly ILogger<PupilNumbersModel> _logger;

		//// MR:- selected school props for UI rendering
		[BindProperty] 
		public int ApplicationId { get; set; }

		[BindProperty] 
		public int Urn { get; private set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to show school conversion data
		public List<SchoolConversionComponentHeadingViewModel> ViewModel { get; set; } = new();


		public SchoolConversionKeyDetailsModel(ILogger<PupilNumbersModel> logger,
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

			// TODO MR:- fo answer, consume SchoolConversionComponentSectionViewModel.NoInfoAnswer if string.isnullorempty()
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

		// MR:- stuff from A2C-sip

		//var reviewSectionSchoolName = new QandAReviewModel
		//{
		//		Title = "The school joining the trust",
		//		ChangeReference = "AddSchool",
		//		OrgType = Enums.OrganisationType.School,
		// Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionSchoolName.ChangeReference}"];
		//};

		//reviewSectionSchoolName.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "The name of the school",
		//	Answer = ((string) ViewData[FieldConstants.SchoolName]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SchoolName,
		//Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionMainContact.ChangeReference}"];
		//});

		//var reviewSectionMainContact = new QandAReviewModel
		//{
		//	Title = "Contact details",
		//	ChangeReference = "ConversionMainContact",
		//	OrgType = Enums.OrganisationType.School
		// Status = MR:- none = section heading
		//};

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Name of headteacher",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string) ViewData[FieldConstants.SipSchoolConversionContactHeadName]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactHeadName
		// Status = MR:- none - data filled in or no i.e. do we have a name of headteacher?
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Headteacher's email address",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionContactHeadEmail]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactHeadEmail
		// Status = MR:- none - data filled in or no i.e. do we have an headteacher email?
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Headteacher's telephone number",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionContactHeadTel]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactHeadTel
		// Status = MR:- none - data filled in or no i.e. do we have an headteacher tel number?
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Name of the chair of the governing body",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionContactChairName]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactChairName
		// Status = MR:- none - data filled in or no i.e. do we have an chair of the governing body?
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Chair's email address",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionContactChairEmail]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactChairEmail
		// Status = MR:- none - data filled in or no i.e. do we have an chair of the governing body email?
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Chair's telephone number",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionContactChairTel]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionContactChairTel
		// Status = MR:- none - data filled in or no i.e. do we have an chair of the governing tel number?
		//});

		//var mainContactForConversionAnswer = Constants.NoInfo;
		//var showAdditionalInfo = false;

		//if ((string)ViewData[FieldConstants.SipSchoolConversionMainContact] == ((int)Enums.A2CMainConversionContact.HeadTeacher).ToString())
		//{
		//	mainContactForConversionAnswer = "The headteacher";
		//}
		//else if ((string)ViewData[FieldConstants.SipSchoolConversionMainContact] == ((int)Enums.A2CMainConversionContact.ChairOfGoverningBody).ToString())
		//{
		//	mainContactForConversionAnswer = "The chair of the governing body";
		//}
		//else if ((string)ViewData[FieldConstants.SipSchoolConversionMainContact] == ((int)Enums.A2CMainConversionContact.Other).ToString())
		//{
		//	mainContactForConversionAnswer = "Someone else";
		//	showAdditionalInfo = true;
		//}

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Who is the main contact for the conversion?",
		//	Answer = mainContactForConversionAnswer,
		//	FieldName = FieldConstants.SipSchoolConversionMainContact,
		//	ShowAdditionalInfo = showAdditionalInfo,
		//	AdditionalInfo = new List<QandAModel>
		//		{
		//			new QandAModel
		//			{
		//				Question = "Name",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherName]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionMainContactOtherName
		//			},
		//			new QandAModel
		//			{
		//				Question = "Email",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherEmail]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionMainContactOtherEmail
		//			},
		//			new QandAModel
		//			{
		//				Question = "Telephone number",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherTelephone]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionMainContactOtherTelephone
		//			},
		//			new QandAModel
		//			{
		//				Question = "Role",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherRole]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionMainContactOtherRole
		//			}
		//		}
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Approver's full name",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionApproverContactName]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionApproverContactName
		//});

		//reviewSectionMainContact.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Approver's email address",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionApproverContactEmail]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionApproverContactEmail
		//});

		//var reviewSectionTargetDate = new QandAReviewModel
		//{
		//	Title = "Date for conversion",
		//	ChangeReference = "ConversionTargetDate",
		//	OrgType = Enums.OrganisationType.School,
		// Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionTargetDate.ChangeReference}"];
		//};

		//var targetDate = Constants.NoInfo;
		//if (!string.IsNullOrEmpty((string)ViewData[FieldConstants.SipSchoolConversionTargetDateDifferent]))
		//{
		//	switch (long.Parse(ViewData[FieldConstants.SipSchoolConversionTargetDateDifferent].ToString()))
		//	{
		//		case (int)Enums.A2CSelectOption.Yes: // MR:- this enum in school change name branch -> SelectOption
		//			targetDate = "Yes";
		//			break;
		//		case (int)Enums.A2CSelectOption.No: // MR:- this enum in school change name branch -> SelectOption
		//			targetDate = "No";
		//			break;
		//	}
		//}

		//reviewSectionTargetDate.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Do you want the conversion to happen on a particular date?",
		//	Answer = targetDate,
		//	FieldName = FieldConstants.SipSchoolConversionTargetDateDifferent,
		//	ShowAdditionalInfo = targetDate == "Yes",
		//	AdditionalInfo = new List<QandAModel>
		//		{
		//			new QandAModel
		//			{
		//				Question = "PreferredDate",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionTargetDateDate]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionTargetDateDate
		//			},
		//			new QandAModel
		//			{
		//				Question = "Explain why you want to convert on this date?",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionTargetDateExplained]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionTargetDateExplained
		//			}
		//		}
		//});

		//var reviewSectionRationale = new QandAReviewModel
		//{
		//	Title = "Reasons for joining",
		//	ChangeReference = "ConversionRationale",
		//	OrgType = Enums.OrganisationType.School,
		// Status = (Enums.SchoolConversionComponentStatus)ViewData[$"status-{reviewSectionRationale.ChangeReference}"];
		//};

		//reviewSectionRationale.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Why does the school want to join this trust in particular?",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//	Answer = ((string)ViewData[FieldConstants.SipSchoolConversionReasonsForJoining]).DisplayNoInfoIfNullOrEmpty(),
		//	FieldName = FieldConstants.SipSchoolConversionReasonsForJoining,
		//	IsVisible = ((string)ViewData[$"{FieldConstants.SipSchoolConversionReasonsForJoining}-visible"]).SafeBoolConvertDefaultTrue()
		//});

		//var reviewSectionName = new QandAReviewModel
		//{
		//	Title = "Changing the name of the school",
		//	ChangeReference = "ConversionNameChange",
		//	OrgType = Enums.OrganisationType.School,
		// Status = (Enums.SchoolConversionComponentStatus)ViewData[$"status-{reviewSectionName.ChangeReference}"];
		//};

		//var nameChange = Constants.NoInfo;
		//if (!string.IsNullOrEmpty((string)ViewData[FieldConstants.SipSchoolConversionChangeName]))
		//{
		//	switch (long.Parse(ViewData[FieldConstants.SipSchoolConversionChangeName].ToString()))
		//	{
		//		case (int)Enums.A2CSelectOption.Yes: // MR:- this enum in school change name branch -> SelectOption
		//			nameChange = "Yes";
		//			break;
		//		case (int)Enums.A2CSelectOption.No: // MR:- this enum in school change name branch -> SelectOption
		//			nameChange = "No";
		//			break;
		//	}
		//}

		//reviewSectionName.QuestionsAndAnswers.Add(new QandAModel
		//{
		//	Question = "Is the school planning to change its name when it becomes an academy?",
		//	Answer = nameChange,
		//	FieldName = FieldConstants.SipSchoolConversionChangeName,
		//	ShowAdditionalInfo = nameChange == "Yes",
		//	AdditionalInfo = new List<QandAModel>
		//		{
		//			new QandAModel
		//			{
		//				Question = "What's the proposed new name?",
		// MR:- below sets text to public const string NoInfo = "You have not added any information";
		//				Answer = ((string) ViewData[FieldConstants.SipSchoolConversionChangeNameValue]).DisplayNoInfoIfNullOrEmpty(),
		//				FieldName = FieldConstants.SipSchoolConversionChangeNameValue
		//			}
		//		}
		//});
	}
}
