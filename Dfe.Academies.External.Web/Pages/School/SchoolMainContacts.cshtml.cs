using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.Validators;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class SchoolMainContactsModel : BasePageEditModel
	{
		private readonly IConversionApplicationCreationService _academisationCreationService;

		//// MR:- selected school props for UI rendering
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		public string SigninApproverQuestionText { get; private set; } = string.Empty;

		[BindProperty]
		public ApplicationSchoolContactsViewModel ViewModel { get; set; }

		public bool OtherContactError
		{
			get
			{
				var bools = new[] { OtherNameError, OtherEmailError, OtherTelephoneError };

				return bools.Any(b => b);
			}
		}

		public bool OtherNameError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherNameNotEntered");
			}
		}

		public bool OtherEmailError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherEmailNotEntered");
			}
		}

		public bool OtherTelephoneError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherTelephoneNotEntered");
			}
		}

		public SchoolMainContactsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			ApplicationId = appId;
			Urn = urn;
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			// Grab other values from API
			if (selectedSchool != null)
			{
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData);

				PopulateUiModel(selectedSchool, draftConversionApplication.ApplicationType);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}
			
			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var dictionaryMapper = PopulateUpdateDictionary();
			await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

			// update temp store for next step - application overview as last step in process
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			return RedirectToPage("SchoolConversionKeyDetails", new { appId = ApplicationId, urn = Urn });
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherName))
			{
				ModelState.AddModelError("MainContactOtherNameNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherEmail))
			{
				ModelState.AddModelError("MainContactOtherEmailNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherTelephone))
			{
				ModelState.AddModelError("MainContactOtherTelephoneNotEntered", "You must provide details");
				PopulateValidationMessages();
				return false;
			}

			// Check ViewModel.MainContactOtherEmail - is-valid email address
			if (ViewModel.ContactRole == MainConversionContact.Other &&
			    !string.IsNullOrWhiteSpace(ViewModel.MainContactOtherEmail))
			{
				var emailAddress = new EmailAddress(ViewModel.MainContactOtherEmail);
				var emailValidator = new EmailValidator();
				var validationResult = emailValidator.Validate(emailAddress);

				if (!validationResult.IsValid)
				{
					// display:- (ErrorMessage = "Main contact email is not a valid e-mail address")
					ModelState.AddModelError("MainContactOtherEmailInvalid", "Main contact email is not a valid e-mail address");
					PopulateValidationMessages();
					return false;
				}
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
			// if ContactRole != other - blank 'MainContactOtherName' && 'MainContactOtherEmail' && 'MainContactOtherTelephone'
			if (ViewModel.ContactRole != MainConversionContact.Other)
			{
				ViewModel.MainContactOtherName = null;
				ViewModel.MainContactOtherEmail = null;
				ViewModel.MainContactOtherTelephone = null;
			}

			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactHeadName), ViewModel.ContactHeadName },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactHeadEmail), ViewModel.ContactHeadEmail },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactHeadTel), ViewModel.ContactHeadTel },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactChairName), ViewModel.ContactChairName },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactChairEmail), ViewModel.ContactChairEmail },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactChairTel), ViewModel.ContactChairTel },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactRole), ViewModel.ContactRole.ToString() },
				{ nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherName), ViewModel?.MainContactOtherName! },
				{ nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherEmail), ViewModel?.MainContactOtherEmail! },
				{ nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherTelephone), ViewModel?.MainContactOtherTelephone! },
				{ nameof(SchoolApplyingToConvert.SchoolConversionApproverContactName), ViewModel?.ApproverContactName },
				{ nameof(SchoolApplyingToConvert.SchoolConversionApproverContactEmail), ViewModel?.ApproverContactEmail }
			};
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ApplicationTypes applicationType)
		{
			SchoolName = selectedSchool.SchoolName;

			ViewModel = new ApplicationSchoolContactsViewModel(ApplicationId, selectedSchool.URN)
			{
				ContactHeadName = selectedSchool.SchoolConversionContactHeadName,
				ContactHeadEmail = selectedSchool.SchoolConversionContactHeadEmail,
				ContactHeadTel = selectedSchool.SchoolConversionContactHeadTel,
				ContactChairName = selectedSchool.SchoolConversionContactChairName,
				ContactChairEmail = selectedSchool.SchoolConversionContactChairEmail,
				ContactChairTel = selectedSchool.SchoolConversionContactChairTel,
				ContactRole = !string.IsNullOrEmpty(selectedSchool.SchoolConversionContactRole) ? selectedSchool.SchoolConversionContactRole.ToEnum<MainConversionContact>() : 0,
				MainContactOtherName = selectedSchool.SchoolConversionMainContactOtherName,
				MainContactOtherEmail = selectedSchool.SchoolConversionMainContactOtherEmail,
				MainContactOtherTelephone = selectedSchool.SchoolConversionMainContactOtherTelephone,
				ApproverContactName = selectedSchool.SchoolConversionApproverContactName,
				ApproverContactEmail = selectedSchool.SchoolConversionApproverContactEmail
			};

			SigninApproverQuestionText = applicationType == ApplicationTypes.FormASat
						? "When your schools converts, we need to create a new DfE sign-in account for the academy. Please supply the most appropriate contact to be set up as the DfE Sign-in approver to manage the new academies account."
						: "When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.";
		}
	}
}
