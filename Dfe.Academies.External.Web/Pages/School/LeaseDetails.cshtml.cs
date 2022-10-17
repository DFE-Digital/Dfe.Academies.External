using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LeaseDetails : BasePageEditModel
	{
		[BindProperty]
		public int  Id { get; set; }
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public int Urn { get; set; }
		
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public int LeaseTerm { get; set; }
		
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public decimal RepaymentAmount { get; set; }
		
		[BindProperty]
		[Range(0, 200000000000000, ErrorMessage = "Interest rate must be greater than 0")]
		[Required(ErrorMessage = "You must provide details")]
		public decimal InterestRate { get; set; }
		
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public decimal PaymentsToDate { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string Purpose { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string ValueOfAssets { get; set; }
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string ResponsibleForAssets { get; set; }

		[BindProperty]
		public bool IsEdit { get; set; }
		
		[BindProperty]
		public bool IsDraft { get; set; }

		public void OnGet(int appId, int urn, int id, bool isEdit)
		{
			LoadAndStoreCachedConversionApplication();
			
			ApplicationId = appId;
			Urn = urn;
			IsEdit = isEdit;
			Id = id;
			
			//If clicked changed answers then load the lease from tempdata and populate the fields
			if (IsEdit)
			{
				var leaseModels = TempDataLoadBySchool<List<LeaseViewModel>>(Urn);
				
				var selectedlease = leaseModels?.FirstOrDefault(lease => lease.IsDraft == IsDraft && Id == lease.Id);
				
				if (selectedlease != null)
				{
					Id = selectedlease.Id;
					LeaseTerm = selectedlease.LeaseTerm;
					RepaymentAmount = selectedlease.RepaymentAmount;
					InterestRate = selectedlease.InterestRate;
					PaymentsToDate = selectedlease.PaymentsToDate;
					Purpose = selectedlease.Purpose;
					ValueOfAssets = selectedlease.ValueOfAssets;
					ResponsibleForAssets = selectedlease.ResponsibleForAssets;
				}
			}
		}

		///<inheritdoc/>
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

			var selectedLoan = new LeaseViewModel
			{
				Id = Id,
				LeaseTerm = LeaseTerm,
				RepaymentAmount = RepaymentAmount,
				InterestRate = InterestRate,
				PaymentsToDate = PaymentsToDate,
				Purpose = Purpose,
				ValueOfAssets = ValueOfAssets,
				ResponsibleForAssets = ResponsibleForAssets
			};
			
			var leaseViewModels = TempDataLoadBySchool<List<LeaseViewModel>>(Urn) ?? new List<LeaseViewModel>();
			
			//If we're editing the loan then overwrite the correct loan in the list of loans with the current binded values
			if (IsEdit)
			{
				var leaseViewModel = leaseViewModels.FirstOrDefault(loan => IsDraft == loan.IsDraft && Id == loan.Id);
				
				if (leaseViewModel != null)
				{
					leaseViewModel.Id = Id;
					leaseViewModel.IsDraft = false;
					leaseViewModel.LeaseTerm = LeaseTerm;
					leaseViewModel.RepaymentAmount = RepaymentAmount;
					leaseViewModel.InterestRate = InterestRate;
					leaseViewModel.PaymentsToDate = PaymentsToDate;
					leaseViewModel.Purpose = Purpose;
					leaseViewModel.ValueOfAssets = ValueOfAssets;
					leaseViewModel.ResponsibleForAssets = ResponsibleForAssets;
				}
			}
			else
			{
				//Otherwise it's a new loan, so add it to the list of loans
				selectedLoan.IsDraft = true;
				selectedLoan.Id = leaseViewModels.Count;
				leaseViewModels.Add(selectedLoan);
			}
			TempDataSetBySchool<List<LeaseViewModel>>(Urn, leaseViewModels);
			return RedirectToPage("Leases",new {urn = Urn, appId = ApplicationId});
		}

		///<inheritdoc/>
		public LeaseDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService, 
																	IReferenceDataRetrievalService referenceDataRetrievalService) 
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new();
		}
	}
}
