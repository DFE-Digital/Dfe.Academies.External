using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LoanDetails : BasePageEditModel
	{
		private readonly IConversionApplicationCreationService academisationCreationService;

		[BindProperty]
		public int Id { get; set; }
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


		public LoanDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			this.academisationCreationService = academisationCreationService;
		}

		public async Task<IActionResult> OnGet(int appId, int urn, int id, bool isEdit)
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
			IsEdit = isEdit;
			Id = id;

			//If clicked changed answers then load the loan from tempdata and populate the fields
			if (IsEdit)
			{
				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
				var selectedLoan = selectedSchool?.Loans?.FirstOrDefault(loan => Id == loan.LoanId);

				if (selectedLoan != null)
				{
					Id = selectedLoan.LoanId;
					TotalAmount = selectedLoan.Amount;
					Purpose = selectedLoan.Purpose;
					Provider = selectedLoan.Provider;
					InterestRate = selectedLoan.InterestRate;
					RepaymentSchedule = selectedLoan.Schedule;
				}
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!RunUiValidation())
			{
				return Page();
			}

			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);

			var loan = new SchoolLoan(
				Id,
				TotalAmount,
				Purpose,
				Provider,
				InterestRate,
				RepaymentSchedule);

			//If we're editing the loan then overwrite the correct loan in the list of loans with the current binded values
			if (IsEdit)
			{
				await this.academisationCreationService.UpdateLoan(ApplicationId, selectedSchool.id, loan);
			}
			else
			{
				await this.academisationCreationService.CreateLoan(ApplicationId, selectedSchool.id, loan);
			}

			return RedirectToPage("Loans", new { urn = Urn, appId = ApplicationId });
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
