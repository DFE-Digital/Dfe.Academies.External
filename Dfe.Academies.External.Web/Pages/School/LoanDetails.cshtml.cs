using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LoanDetails : BasePageEditModel
	{
		[BindProperty]
		public int  Id { get; set; }
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public decimal TotalAmount { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string Purpose { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string Provider { get; set; }
		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Interest rate must be greater than 0")]
		[Required(ErrorMessage = "You must provide details")]
		public decimal InterestRate { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string RepaymentSchedule { get; set; }
		
		[BindProperty]
		public bool IsEdit { get; set; }
		
		[BindProperty]
		public bool IsDraft { get; set; }

		public LoanDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		public void OnGet(int appId, int urn, int id, bool isEdit)
		{
			LoadAndStoreCachedConversionApplication();
			
			ApplicationId = appId;
			Urn = urn;
			IsEdit = isEdit;
			Id = id;
			
			//If clicked changed answers then load the loan from tempdata and populate the fields
			if (IsEdit)
			{
				var loanModels = TempDataLoadLoanViewModels(Urn);
				
				var selectedLoan = loanModels?.FirstOrDefault(loan => loan.IsDraft == IsDraft && Id == loan.Id);
				
				if (selectedLoan != null)
				{
					Id = selectedLoan.Id;
					TotalAmount = selectedLoan.TotalAmount;
					Purpose = selectedLoan.Purpose;
					Provider = selectedLoan.Provider;
					InterestRate = selectedLoan.InterestRate;
					RepaymentSchedule = selectedLoan.RepaymentSchedule;
				}
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			var selectedLoan = new LoanViewModel
			{
				Id = Id,
				IsDraft = IsDraft,
				TotalAmount = TotalAmount,
				InterestRate = InterestRate,
				Provider = Provider,
				Purpose = Purpose,
				RepaymentSchedule = RepaymentSchedule
			};
			
			var loanViewModels = TempDataLoadLoanViewModels(Urn) ?? new List<LoanViewModel>();
			
			//If we're editing the loan then overwrite the correct loan in the list of loans with the current binded values
			if (IsEdit)
			{
				var loanViewModel = loanViewModels.FirstOrDefault(loan => IsDraft == loan.IsDraft && Id == loan.Id);
				
				if (loanViewModel != null)
				{
					loanViewModel.Id = Id;
					loanViewModel.IsDraft = false;
					loanViewModel.TotalAmount = TotalAmount;
					loanViewModel.InterestRate = InterestRate;
					loanViewModel.Provider = Provider;
					loanViewModel.Purpose = Purpose;
					loanViewModel.RepaymentSchedule = RepaymentSchedule;
				}
			}
			else
			{
				//Otherwise it's a new loan, so add it to the list of loans
				selectedLoan.IsDraft = true;
				selectedLoan.Id = loanViewModels.Count;
				loanViewModels.Add(selectedLoan);
			}
			TempDataSetLoanViewModels(Urn, loanViewModels);
			return RedirectToPage( "Loans" ,new {urn = Urn, appId = ApplicationId});
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
	}
}
