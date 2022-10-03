using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class Loans : BasePageEditModel
	{
		private readonly ILogger<Loans> _logger;
		private readonly IConversionApplicationCreationService _academisationCreationService;
		
		public Loans(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			ILogger<Loans> logger,
			IConversionApplicationCreationService academisationCreationService) :
			base(conversionApplicationRetrievalService, 
				referenceDataRetrievalService)
		{
			_logger = logger;
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
			TempData[TempDataHelper.LoanViewModelsKey] = null;
			
			return RedirectToPage("FinancesReview", new { urn = Urn, appId = ApplicationId });
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
					MergeCachedAndDatabaseLoans(selectedSchool);
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::CurrentFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
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
			var tempDataLoanViewModels = TempDataLoadLoanViewModels() ?? new List<LoanViewModel>();
			
			tempDataLoanViewModels.ForEach(x =>
			{
				var loan = LoanViewModels.Find(y => y.Id == x.Id && !x.IsDraft);
				if (loan != null)
				{
					loan.IsDraft = false;
					loan.TempId = x.TempId;
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
			TempDataSetLoanViewModels(LoanViewModels);
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			AnyLoans = LoanViewModels.Any() ? SelectOption.Yes : SelectOption.No;
		}
		
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}
	}
}
