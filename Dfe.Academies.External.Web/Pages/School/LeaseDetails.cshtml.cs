using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class LeaseDetails : BasePageEditModel
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
		public string LeaseTerm { get; set; }

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

		public async Task<IActionResult> OnGet(int appId, int urn, int id, bool isEdit, bool isDraft)
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

			//If clicked changed answers then load the lease from tempdata and populate the fields
			if (IsEdit)
			{
				var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);

				var selectedlease = selectedSchool?.Leases?.FirstOrDefault(lease => Id == lease.LeaseId);

				if (selectedlease != null)
				{
					Id = selectedlease.LeaseId;
					LeaseTerm = selectedlease.LeaseTerm;
					RepaymentAmount = selectedlease.RepaymentAmount;
					InterestRate = selectedlease.InterestRate;
					PaymentsToDate = selectedlease.PaymentsToDate;
					Purpose = selectedlease.Purpose;
					ValueOfAssets = selectedlease.ValueOfAssets;
					ResponsibleForAssets = selectedlease.ResponsibleForAssets;
				}
			}

			return Page();
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

			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);

			var lease = new SchoolLease(Id,
								LeaseTerm,
								RepaymentAmount,
								InterestRate,
								PaymentsToDate,
								Purpose,
								ValueOfAssets,
								ResponsibleForAssets);

			if (IsEdit)
			{
				await this.academisationCreationService.UpdateLease(ApplicationId, selectedSchool.id, lease);
			}
			else
			{
				await this.academisationCreationService.CreateLease(ApplicationId, selectedSchool.id, lease);
			}

			return RedirectToPage("Leases", new { urn = Urn, appId = ApplicationId });
		}

		///<inheritdoc/>
		public LeaseDetails(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
																	IReferenceDataRetrievalService referenceDataRetrievalService, IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			this.academisationCreationService = academisationCreationService;
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
