using Dfe.Academies.External.Web.Attributes;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
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
			IConversionApplicationCreationService academisationCreationService) :
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
			MergeCachedAndDatabaseEntities(selectedSchool);

			if (!RunUiValidation()) return Page();

			var draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>(TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();
			
			foreach (var leaseViewModel in LeaseViewModels)
			{
				if (AnyLeases == SelectOption.No && !leaseViewModel.IsDraft)
				{
					await ConversionApplicationCreationService.DeleteLease(ApplicationId, selectedSchool.id, leaseViewModel.Id);
					continue;
				}

				var lease = new SchoolLease(leaseViewModel.Id,
					leaseViewModel.LeaseTerm,
					leaseViewModel.RepaymentAmount,
					leaseViewModel.InterestRate,
					leaseViewModel.PaymentsToDate,
					leaseViewModel.Purpose,
					leaseViewModel.ValueOfAssets,
					leaseViewModel.ResponsibleForAssets);


				if (leaseViewModel.IsDraft)
					await ConversionApplicationCreationService.CreateLease(ApplicationId, selectedSchool.id, lease);
				else
				{
					await ConversionApplicationCreationService.UpdateLease(ApplicationId, selectedSchool.id, lease);
				}
			}

			// if user has selected no then update the school and set hasLoans
			if (!LeaseViewModels.Any() && AnyLeases == SelectOption.No)
			{
				var dictionaryMapper = PopulateUpdateDictionary();
				await ConversionApplicationCreationService.PutSchoolApplicationDetails(ApplicationId, Urn, dictionaryMapper);
			}

			// update temp store for next step - application overview
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);
			TempData[$"{Urn.ToString()}-{typeof(List<LeaseViewModel>)}"] = null;
			
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
				MergeCachedAndDatabaseEntities(selectedSchool);
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
					IsDraft = false,
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

		private void MergeCachedAndDatabaseEntities(SchoolApplyingToConvert selectedSchool)
		{
			LoadLeasesFromDatabase(selectedSchool);
			
			//Try to merge with what is saved in the cache
			//Use the ID on the lease view model
			var tempDataViewModels = TempDataLoadBySchool<List<LeaseViewModel>>(Urn) ?? new List<LeaseViewModel>();

			if (tempDataViewModels.Any())
			{
				//override hasLeases value if we have some temp leases 
				HasLeases = true;
			}

			tempDataViewModels.ForEach(x =>
			{
				var lease = LeaseViewModels.Find(y => y.Id == x.Id && !x.IsDraft);
				
				//Overwrite the lease from the database with the one stored in the cache if they have matching IDs
				//and the one in the cache isn't a draft because it's an integer so there's a chance of collisions
				if (lease != null)
				{
					lease.IsDraft = false;
					lease.Id = x.Id;
					lease.LeaseTerm = x.LeaseTerm;
					lease.RepaymentAmount = x.RepaymentAmount;
					lease.InterestRate = x.InterestRate;
					lease.PaymentsToDate = x.PaymentsToDate;
					lease.Purpose = x.Purpose;
					lease.ValueOfAssets = x.ValueOfAssets;
					lease.ResponsibleForAssets = x.ResponsibleForAssets;
				}
				else
				{
					LeaseViewModels.Add(x);
				}
			});
			TempDataSetBySchool<List<LeaseViewModel>>(Urn, LeaseViewModels);

		}

		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			AnyLeases = HasLeases.HasValue && HasLeases.Value ? SelectOption.Yes : SelectOption.No;
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

			if (!AnyLeases.HasValue)
			{
				ModelState.AddModelError("InvalidSelectOptionError", "You must select an option");
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
