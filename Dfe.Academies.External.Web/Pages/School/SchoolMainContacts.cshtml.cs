using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class SchoolMainContactsModel : BasePageEditModel
	{
		private readonly ILogger<SchoolMainContactsModel> _logger;
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
				if (!ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherNameNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool OtherEmailError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherEmailNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public bool OtherTelephoneError
		{
			get
			{
				if (!ModelState.IsValid && ModelState.Keys.Contains("MainContactOtherTelephoneNotEntered"))
				{
					return true;
				}

				return false;
			}
		}

		public SchoolMainContactsModel(ILogger<SchoolMainContactsModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
			_academisationCreationService = academisationCreationService;
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			try
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
			catch (Exception ex)
			{
				_logger.LogError("School::SchoolMainContactsModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherName))
			{
				ModelState.AddModelError("MainContactOtherNameNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherEmail))
			{
				ModelState.AddModelError("MainContactOtherEmailNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			if (ViewModel.ContactRole == MainConversionContact.Other && string.IsNullOrWhiteSpace(ViewModel.MainContactOtherTelephone))
			{
				ModelState.AddModelError("MainContactOtherTelephoneNotEntered", "You must provide details");
				PopulateValidationMessages();
				return Page();
			}

			try
			{
				//// grab draft application from temp= null
				var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
				
				var dictionaryMapper = new Dictionary<string, dynamic>
				{
					{ "SchoolConversionContactHeadName", ViewModel.ContactHeadName },
					{ "SchoolConversionContactHeadEmail", ViewModel.ContactHeadEmail },
					{ "SchoolConversionContactHeadTel", ViewModel.ContactHeadTel },
					{ "SchoolConversionContactChairName", ViewModel.ContactChairName },
					{ "SchoolConversionContactChairEmail", ViewModel.ContactChairEmail },
					{ "SchoolConversionContactChairTel", ViewModel.ContactChairTel },
					{ "SchoolConversionContactRole", ViewModel.ContactRole.ToString() },
					{ "SchoolConversionMainContactOtherName", ViewModel?.MainContactOtherName },
					{ "SchoolConversionMainContactOtherEmail", ViewModel?.MainContactOtherEmail },
					{ "SchoolConversionMainContactOtherTelephone", ViewModel?.MainContactOtherTelephone },
					{ "SchoolConversionApproverContactName", ViewModel?.ApproverContactName },
					{ "SchoolConversionApproverContactEmail", ViewModel?.ApproverContactEmail }
				};
				
				await _academisationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);

				// update temp store for next step - application overview as last step in process
				TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

				return RedirectToPage("SchoolConversionKeyDetails", new { appId = ApplicationId, urn = Urn });
			}
			catch (Exception ex)
			{
				_logger.LogError("School::SchoolMainContactsModel::OnPostAsync::Exception - {Message}", ex.Message);
				return Page();
			}
		}

		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
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
