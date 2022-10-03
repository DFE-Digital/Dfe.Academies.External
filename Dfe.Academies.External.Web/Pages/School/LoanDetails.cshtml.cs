using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LoanDetails : BasePageEditModel
	{
		[BindProperty]
		public int  Id { get; set; }
		[BindProperty]
		public int TempId { get; set; }
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }
		[BindProperty]
		public decimal TotalAmount { get; set; }
		[BindProperty]
		public string Purpose { get; set; }
		[BindProperty]
		public string Provider { get; set; }
		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Interest rate must be greater than 0")]
		public decimal InterestRate { get; set; }
		[BindProperty]
		public string RepaymentSchedule { get; set; }
		
		public LoanViewModel SelectedLoan { get; set; }
		
		[BindProperty]
		public bool IsEdit { get; set; }
		
		[BindProperty]
		public bool IsDraft { get; set; }

		public void OnGet(int appId, int urn, int tempId, bool isEdit, bool isDraft, int id)
		{
			LoadAndStoreCachedConversionApplication();
			
			ApplicationId = appId;
			Urn = urn;
			IsEdit = isEdit;
			TempId = tempId;
			Id = id;
			
			if (IsEdit)
			{
				var loanModels = TempDataLoadLoanViewModels();
				SelectedLoan = loanModels?.FirstOrDefault(loan =>
					(loan.IsDraft && TempId == loan.TempId) 
					|| (!loan.IsDraft && Id == loan.Id));
				if (SelectedLoan != null)
				{
					Id = SelectedLoan.Id;
					TempId = SelectedLoan.TempId;
					TotalAmount = SelectedLoan.TotalAmount;
					Purpose = SelectedLoan.Purpose;
					Provider = SelectedLoan.Provider;
					InterestRate = SelectedLoan.InterestRate;
					RepaymentSchedule = SelectedLoan.RepaymentSchedule;
				}
			}
		}
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return Page();
			}

			SelectedLoan = new LoanViewModel
			{
				Id = Id,
				IsDraft = IsDraft,
				TempId = TempId,
				TotalAmount = TotalAmount,
				InterestRate = InterestRate,
				Provider = Provider,
				Purpose = Purpose,
				RepaymentSchedule = RepaymentSchedule
			};
			var loanViewModels = TempDataLoadLoanViewModels() ?? new List<LoanViewModel>();
			if (IsEdit)
			{
				var loanViewModel = loanViewModels.FirstOrDefault(loan =>
					(SelectedLoan.IsDraft && SelectedLoan.TempId == loan.TempId) 
					|| (!SelectedLoan.IsDraft && SelectedLoan.Id == loan.Id));
				
				if (loanViewModel != null)
				{
					loanViewModel.Id = SelectedLoan.Id;
					loanViewModel.IsDraft = false;
					loanViewModel.TempId = SelectedLoan.TempId;
					loanViewModel.TotalAmount = SelectedLoan.TotalAmount;
					loanViewModel.InterestRate = SelectedLoan.InterestRate;
					loanViewModel.Provider = SelectedLoan.Provider;
					loanViewModel.Purpose = SelectedLoan.Purpose;
					loanViewModel.RepaymentSchedule = SelectedLoan.RepaymentSchedule;
				}
			}
			else
			{
				SelectedLoan.TempId = TempId;
				SelectedLoan.IsDraft = true;
				loanViewModels.Add(SelectedLoan);
			}
			TempDataSetLoanViewModels(loanViewModels);
			TempDataSetSelectedLoan(SelectedLoan);
			return RedirectToPage( "Loans" ,new {urn = Urn, appId = ApplicationId});
		}

		public LoanDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService) : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}
	}
}
