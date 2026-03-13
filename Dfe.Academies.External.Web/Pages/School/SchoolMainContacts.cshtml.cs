using Dfe.Academies.External.Web.Constants;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.Validators;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class SchoolMainContactsModel : BaseSchoolPageEditModel
	{
		public string SigninApproverQuestionText { get; private set; } = "When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.";

		[BindProperty]
		public ApplicationSchoolContactsViewModel ViewModel { get; set; }

		

		public bool OtherContactError
		{
			get
			{
				var bools = new[] { OtherNameError, OtherEmailError, OtherEmailFormatError, OtherEmailInvalidError };

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
		public bool OtherEmailFormatError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("ViewModel.MainContactOtherEmail");
			}
		}

		public bool OtherEmailInvalidError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherEmailInvalid");
			}
		}
		
		public SchoolMainContactsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService)
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

				if (draftConversionApplication != null)
				{
					PopulateUiModel(selectedSchool, draftConversionApplication.ApplicationType);
				}
				else
				{
					ViewModel = new ApplicationSchoolContactsViewModel(ApplicationId, urn);
				}
			}
			else
			{
				ViewModel = new ApplicationSchoolContactsViewModel(ApplicationId, urn);
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

			if (ViewModel != null && ViewModel.ContactRole == MainConversionContact.Other)
			{
				if (string.IsNullOrWhiteSpace(ViewModel.MainContactOtherName))
				{
					ModelState.AddModelError("MainContactOtherNameNotEntered", "You must provide a contact name");
					PopulateValidationMessages();
					return false;
				}
				if (string.IsNullOrWhiteSpace(ViewModel.MainContactOtherEmail))
				{
					ModelState.AddModelError("MainContactOtherEmailNotEntered", ValidationMessageConstants.MustHaveOtherContactEmail);
					PopulateValidationMessages();
					return false;
				}
				else if (!string.IsNullOrWhiteSpace(ViewModel.MainContactOtherEmail))
				{
					var emailAddress = new EmailAddress(ViewModel.MainContactOtherEmail);
					var emailValidator = new EmailValidator();
					if (!emailValidator.Validate(emailAddress).IsValid)
					{
						ModelState.AddModelError("MainContactOtherEmailInvalid", "Main contact email is not a valid e-mail address");
						PopulateValidationMessages();
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Field order on the Main contacts page (matches display order for error summary).
		/// Keys must match those added by PopulateViewDataErrorsWithModelStateErrors (with # prefix).
		/// </summary>
		private static readonly string[] MainContactsValidationKeyOrder =
		[
			"#ViewModel.ContactHeadName",
			"#ViewModel.ContactHeadEmail",
			"#ViewModel.ContactChairName",
			"#ViewModel.ContactChairEmail",
			"#ViewModel.ContactRole",
			"#MainContactOtherNameNotEntered",
			"#MainContactOtherEmailNotEntered",
			"#ViewModel.MainContactOtherEmail",
			"#MainContactOtherEmailInvalid"
		];

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
			ReorderValidationMessagesToMatchPageOrder();
		}

		private void ReorderValidationMessagesToMatchPageOrder()
		{
			var current = ValidationErrorMessagesViewModel.ValidationErrorMessages;
			if (current.Count == 0) return;

			var ordered = new Dictionary<string, IEnumerable<string>?>();
			foreach (string key in MainContactsValidationKeyOrder)
			{
				if (current.TryGetValue(key, out var messages))
					ordered[key] = messages;
			}

			foreach (var kvp in current.Where(kvp => !ordered.ContainsKey(kvp.Key)))
			{
				ordered[kvp.Key] = kvp.Value;
			}

			ValidationErrorMessagesViewModel.ValidationErrorMessages = ordered;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// if ContactRole != other - blank 'MainContactOtherName' && 'MainContactOtherEmail' && 'MainContactOtherTelephone'
			if (ViewModel.ContactRole != MainConversionContact.Other)
			{
				ViewModel.MainContactOtherName = null;
				ViewModel.MainContactOtherEmail = null;
			}

			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactHeadName), ViewModel.ContactHeadName },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactHeadEmail), ViewModel.ContactHeadEmail },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactChairName), ViewModel.ContactChairName },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactChairEmail), ViewModel.ContactChairEmail },
				{ nameof(SchoolApplyingToConvert.SchoolConversionContactRole), ViewModel.ContactRole.ToString() },
				{ nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherName), ViewModel?.MainContactOtherName! },
				{ nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherEmail), ViewModel?.MainContactOtherEmail! },
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
				ContactChairName = selectedSchool.SchoolConversionContactChairName,
				ContactChairEmail = selectedSchool.SchoolConversionContactChairEmail,
				ContactRole = !string.IsNullOrEmpty(selectedSchool.SchoolConversionContactRole) ? selectedSchool.SchoolConversionContactRole.ToEnum<MainConversionContact>() : 0,
				MainContactOtherName = selectedSchool.SchoolConversionMainContactOtherName,
				MainContactOtherEmail = selectedSchool.SchoolConversionMainContactOtherEmail,
				ApproverContactName = selectedSchool.SchoolConversionApproverContactName,
				ApproverContactEmail = selectedSchool.SchoolConversionApproverContactEmail
			}; 
		}
	}
}
