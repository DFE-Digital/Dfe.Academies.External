using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class SchoolOverviewModel : BaseSchoolSummaryPageModel
	{
		public ApplicationTypes ApplicationType { get; private set; }

		public SchoolComponentsViewModel SchoolComponents { get; private set; } = new();

		public SchoolOverviewModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
									IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		/// <summary>
		/// Consuming different PopulateUiModel() NOT from base, so need an overload
		/// </summary>
		/// <param name="urn"></param>
		/// <param name="appId"></param>
		/// <returns></returns>
		public override async Task<ActionResult> OnGetAsync(int urn, int appId)
		{
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			var conversionApplication = await LoadAndSetApplicationDetails(appId);
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			
			if (selectedSchool != null)
			{
				selectedSchool.SchoolApplicationComponents = await ConversionApplicationRetrievalService
					.GetSchoolApplicationComponents(appId, urn);

				PopulateUiModel(selectedSchool, conversionApplication);
			}

			return Page();
		}
		
		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
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

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Consume conversionApplication, so need different overload
		/// </summary>
		/// <param name="selectedSchool"></param>
		/// <param name="application"></param>
		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? application)
		{
			ApplicationType = application.ApplicationType;
			SchoolComponentsViewModel componentsVm = new()
			{
				URN = selectedSchool.URN,
				ApplicationId = application.ApplicationId,
				// Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
				SchoolComponents = selectedSchool.SchoolApplicationComponents.Select(c =>
					new ApplicationComponentViewModel(name: c.Name,
						uri: SetSchoolApplicationComponentUriFromName(c.Name))
					{
						Status = c.Status
					}).ToList()
			};

			SchoolComponents = componentsVm;
		}
	}
}
