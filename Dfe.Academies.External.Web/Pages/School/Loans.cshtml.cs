﻿using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class Loans : BasePageEditModel
	{
		private readonly IConversionApplicationCreationService _academisationCreationService;
		
		public Loans(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			ILogger<Loans> logger,
			IConversionApplicationCreationService academisationCreationService) :
			base(conversionApplicationRetrievalService, 
				referenceDataRetrievalService)
		{
			_academisationCreationService = academisationCreationService;
		}
		
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }
		
		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? AnyLoans { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

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

		public async Task<IActionResult> OnPostAsync()
		{
			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);
			MergeCachedAndDatabaseLoans(selectedSchool);
			
			if (AnyLoans == SelectOption.Yes && !LoanViewModels.Any())
			{
				ModelState.AddModelError("AddedLoansButEmptyCollectionError", "You must provide the details on the loan");
				PopulateValidationMessages();
				return Page();
			}

			if (!AnyLoans.HasValue)
			{
				ModelState.AddModelError("InvalidSelectOptionError", "You must select an option");
				PopulateValidationMessages();
				return Page();
			}

			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var loans = new List<SchoolLoan>();
			
			if(AnyLoans == SelectOption.Yes)
				LoanViewModels.ForEach(x =>
				{
					loans.Add(new SchoolLoan(x.TotalAmount, x.Purpose, x.Provider,x.InterestRate, x.RepaymentSchedule));
				});
			
			var dictionaryMapper = new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.Loans), loans }
			};

			await _academisationCreationService.PutSchoolApplicationDetails(
				ApplicationId,
				Urn,
				dictionaryMapper
			);

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			TempData[Urn.ToString()] = null;
			
			return RedirectToPage("FinancesReview", new { urn = Urn, appId = ApplicationId });
		}
		
		public async Task OnGetAsync(int urn, int appId)
		{
			LoadAndStoreCachedConversionApplication();
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				MergeCachedAndDatabaseLoans(selectedSchool);
				PopulateUiModel(selectedSchool);
			}
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
			var tempDataLoanViewModels = TempDataLoadLoanViewModels(Urn) ?? new List<LoanViewModel>();
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
			TempDataSetLoanViewModels(Urn, LoanViewModels);
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// TODO:- move code to here !!
			throw new NotImplementedException();
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

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			AnyLoans = LoanViewModels.Any() ? SelectOption.Yes : SelectOption.No;
		}
	}
}