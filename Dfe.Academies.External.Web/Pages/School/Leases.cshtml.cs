using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class Leases : BaseSchoolPageEditModel
	{
		public Leases(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService) :
			base(conversionApplicationRetrievalService,
				referenceDataRetrievalService, academisationCreationService, "FinancialInvestigations")
		{
		}

		[BindProperty]
		[RequiredEnum(ErrorMessage = "You must provide details")]
		public SelectOption? AnyLeases { get; set; }

		public List<LeaseViewModel> LeaseViewModels { get; set; }

		public bool? HasLeases { get; set; }

		//Validation errors
		public bool AddedLeasesButEmptyCollectionError => !ModelState.IsValid && ModelState.Keys.Contains("AddedLeasesButEmptyCollectionError");
		public bool InvalidSelectOptionError => !ModelState.IsValid && ModelState.Keys.Contains("InvalidSelectOptionError");

		public bool HasError
		{
			get
			{
				var bools = new[]
				{
					AddedLeasesButEmptyCollectionError,
					InvalidSelectOptionError
				};

				return bools.Any(b => b);
			}
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var selectedSchool = await LoadAndSetSchoolDetails(ApplicationId, Urn);
			LoadLeasesFromDatabase(selectedSchool);

			if (!RunUiValidation()) return Page();

			//clear leases if no and set hasLeases
			if (AnyLeases == SelectOption.No)
			{
				foreach (var leaseViewModel in LeaseViewModels)
				{
					await ConversionApplicationCreationService.DeleteLease(ApplicationId, selectedSchool.id, leaseViewModel.Id);
				}
				
				var dictionaryMapper = PopulateUpdateDictionary();
				await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);
			}

			return RedirectToPage(NextStepPage, new { urn = Urn, appId = ApplicationId });
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
				LoadLeasesFromDatabase(selectedSchool);
				PopulateUiModel(selectedSchool);
			}

			return Page();
		}

		private void LoadLeasesFromDatabase(SchoolApplyingToConvert selectedSchool)
		{
			//Populate viewmodel from currently saved data
			HasLeases = selectedSchool.HasLeases;
			LeaseViewModels = new List<LeaseViewModel>();
			selectedSchool.Leases.ForEach(lease =>
			{
				LeaseViewModels.Add(new LeaseViewModel
				{
					Id = lease.LeaseId,
					LeaseTerm = lease.LeaseTerm,
					RepaymentAmount = lease.RepaymentAmount,
					InterestRate = lease.InterestRate,
					PaymentsToDate = lease.PaymentsToDate,
					Purpose = lease.Purpose,
					ValueOfAssets = lease.ValueOfAssets,
					ResponsibleForAssets = lease.ResponsibleForAssets

				});
			});
		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			AnyLeases = HasLeases.GetEnumValue();
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (AnyLeases == SelectOption.Yes && !LeaseViewModels.Any())
			{
				ModelState.AddModelError("AddedLeasesButEmptyCollectionError", "You must provide the details on the lease");
				PopulateValidationMessages();
				return false;
			}

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
			return new Dictionary<string, dynamic> { { nameof(SchoolApplyingToConvert.HasLeases), AnyLeases == SelectOption.Yes } };
		}
	}
}
