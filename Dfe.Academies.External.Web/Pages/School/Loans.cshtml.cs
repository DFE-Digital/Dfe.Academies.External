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

		public List<LoanViewModel> LoanViewModels { get; set; } = new();

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
					PopulateUiModel(selectedSchool);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("School::CurrentFinancialYearModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}
		
		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;
			LoanViewModels = new List<LoanViewModel>();
			LoanViewModels.AddRange(selectedSchool.Loans.Select(x => new LoanViewModel { Provider = x.Provider, TotalAmount = x.Amount.Value }));
			AnyLoans = LoanViewModels.Any() ? SelectOption.Yes : SelectOption.No;
		}
		
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}
	}
}
