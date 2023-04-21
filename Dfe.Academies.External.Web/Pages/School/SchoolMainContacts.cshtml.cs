using Dfe.Academies.External.Web.Dtos;
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
	public class SchoolMainContactsModel : BaseSchoolPageEditModel
	{
		//
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
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "ApplicationConversionTargetDate")
		{}

		/// <summary>
		/// Consuming different PopulateUiModel() NOT from base, so need an overload
		/// </summary>
		/// <param name="urn"></param>
		/// <param name="appId"></param>
		/// <returns></returns>
		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("../ApplicationAccessException");
			}

			ApplicationId = appId;
			Urn = urn;
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

			// Grab other values from API
			if (selectedSchool != null)
			{
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData);

				PopulateUiModel(selectedSchool, draftConversionApplication.ApplicationType);
			}

			return Page();
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

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Consume conversionApplication.ApplicationType, so need different overload
		/// </summary>
		/// <param name="selectedSchool"></param>
		/// <param name="applicationType"></param>
		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ApplicationTypes applicationType)
		{
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

			SigninApproverQuestionText = "When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.";
		}
	}
}
