using Dfe.Academies.External.Web.Attributes;
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
		{}
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? AnyLoans { get; set; }

		public List<LoanViewModel> LoanViewModels { get; set; }
		
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
				MergeCachedAndDatabaseLoans(selectedSchool);
				PopulateUiModel(selectedSchool);
			}

			return Page();
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);
			MergeCachedAndDatabaseLoans(selectedSchool);

			if (!RunUiValidation())
			{
				return Page();
			}
			
			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			foreach (var loanViewModel in LoanViewModels)
			{
				if (AnyLoans == SelectOption.No && !loanViewModel.IsDraft)
				{
					await ConversionApplicationCreationService.DeleteLoan(ApplicationId, selectedSchool.id, loanViewModel.Id);
					continue;
				}

				var loan = new SchoolLoan(
					loanViewModel.Id,
					loanViewModel.TotalAmount, 
					loanViewModel.Purpose,
					loanViewModel.Provider, 
					loanViewModel.InterestRate,
					loanViewModel.RepaymentSchedule);

				if (loanViewModel.IsDraft)
					await ConversionApplicationCreationService.CreateLoan(ApplicationId, selectedSchool.id, loan);
				else
				{
					await ConversionApplicationCreationService.UpdateLoan(ApplicationId, selectedSchool.id, loan);
				}
			}
			
			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			TempData[$"{Urn.ToString()}-{typeof(List<LoanViewModel>)}"] = null;
			
			return RedirectToPage(NextStepPage, new { urn = Urn, appId = ApplicationId });
		}

		private void LoadLoansFromDatabase(SchoolApplyingToConvert selectedSchool)
		{
			//Populate viewmodel from currently saved data
			LoanViewModels = new List<LoanViewModel>();
			selectedSchool.Loans.ForEach(loan =>
			{
				LoanViewModels.Add(new LoanViewModel
				{
					IsDraft = false,
					Id = loan.LoanId,
					InterestRate = loan.InterestRate,
					Provider = loan.Provider,
					Purpose = loan.Purpose,
					RepaymentSchedule = loan.Schedule,
					TotalAmount = loan.Amount
				});
			});
		}

		private void MergeCachedAndDatabaseLoans(SchoolApplyingToConvert selectedSchool)
		{
			LoadLoansFromDatabase(selectedSchool);
			
			//Try to merge with what is saved in the cache
			//Use the ID on the loan view model
			var tempDataLoanViewModels = TempDataLoadBySchool<List<LoanViewModel>>(Urn) ?? new List<LoanViewModel>();
			tempDataLoanViewModels.ForEach(x =>
			{
				var loan = LoanViewModels.Find(y => y.Id == x.Id && !x.IsDraft);
				
				//Overwrite the loan from the database with the one stored in the cache if they have matching IDs
				//and the one in the cache isn't a draft because it's an integer so there's a chance of collisions
				if (loan != null)
				{
					loan.IsDraft = false;
					loan.Id = x.Id;
					loan.Provider = x.Provider;
					loan.Purpose = x.Purpose;
					loan.InterestRate = x.InterestRate;
					loan.RepaymentSchedule = x.RepaymentSchedule;
					loan.TotalAmount = x.TotalAmount;
				}
				else
				{
					LoanViewModels.Add(x);
				}
			});
			TempDataSetBySchool<List<LoanViewModel>>(Urn, LoanViewModels);
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
			// does not apply on this page
			return new();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			AnyLoans = LoanViewModels.Any() ? SelectOption.Yes : SelectOption.No;
		}
	}
}
