using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class Loans : BaseSchoolPageEditModel
	{
		public Loans(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService) :
			base(conversionApplicationRetrievalService, referenceDataRetrievalService,
				academisationCreationService, "Leases")
		{ }

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? AnyLoans { get; set; }

		public List<LoanViewModel> LoanViewModels { get; set; }
		public bool? HasLoans { get; set; }

		//Validation errors
		public bool AddedLoansButEmptyCollectionError => !ModelState.IsValid && ModelState.Keys.Contains("AddedLoansButEmptyCollectionError");
		public bool InvalidSelectOptionError => !ModelState.IsValid && ModelState.Keys.Contains("InvalidSelectOptionError");

		public bool HasError
		{
			get
			{
				var bools = new[]
				{
					AddedLoansButEmptyCollectionError,
					InvalidSelectOptionError
				};

				return bools.Any(b => b);
			}
		}

		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();

			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("../ApplicationAccessException");
			}

			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				LoadLoansFromDatabase(selectedSchool);
				PopulateUiModel(selectedSchool);
			}

			return Page();
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);
			LoadLoansFromDatabase(selectedSchool);

			if (!RunUiValidation())
			{
				return Page();
			}

			// clear loans if no and set hasLoans
			if (AnyLoans == SelectOption.No)
			{
				foreach (var loanViewModel in LoanViewModels)
				{
					await ConversionApplicationCreationService.DeleteLoan(ApplicationId, selectedSchool.id, loanViewModel.Id);

				}
				
				var dictionaryMapper = PopulateUpdateDictionary();
				await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);
			}

			return RedirectToPage(NextStepPage, new { urn = Urn, appId = ApplicationId });
		}

		private void LoadLoansFromDatabase(SchoolApplyingToConvert selectedSchool)
		{
			//Populate viewmodel from currently saved data
			HasLoans = selectedSchool.HasLoans;
			LoanViewModels = new List<LoanViewModel>();
			selectedSchool.Loans.ForEach(loan =>
			{
				LoanViewModels.Add(new LoanViewModel
				{
					Id = loan.LoanId,
					InterestRate = loan.InterestRate,
					Provider = loan.Provider,
					Purpose = loan.Purpose,
					RepaymentSchedule = loan.Schedule,
					TotalAmount = loan.Amount
				});
			});
		}
		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (AnyLoans == SelectOption.Yes && !LoanViewModels.Any())
			{
				ModelState.AddModelError("AddedLoansButEmptyCollectionError", "You must provide the details on the loan");
				PopulateValidationMessages();
				return false;
			}

			if (!AnyLoans.HasValue)
			{
				ModelState.AddModelError("InvalidSelectOptionError", "You must select an option");
				PopulateValidationMessages();
				return false;
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
			return new Dictionary<string, dynamic> { { nameof(SchoolApplyingToConvert.HasLoans), AnyLoans == SelectOption.Yes } };
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			AnyLoans = HasLoans.HasValue && HasLoans.Value ? SelectOption.Yes : SelectOption.No;
		}
	}
}
