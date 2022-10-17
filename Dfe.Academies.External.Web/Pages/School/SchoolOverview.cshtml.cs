using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class SchoolOverviewModel : BasePageEditModel
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		public int Urn { get; set; }

		public string SchoolName { get; private set; } = string.Empty;

		public ApplicationTypes ApplicationType { get; private set; }

		public SchoolComponentsViewModel SchoolComponents { get; private set; } = new();

		public SchoolOverviewModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
									IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		public async Task OnGetAsync(int urn, int appId)
		{
			var conversionApplication = await LoadAndSetApplicationDetails(appId);
			var selectedSchool = await LoadAndSetSchoolDetails(appId, urn);
			ApplicationId = appId;
			Urn = urn;

			// Grab other values from API
			if (selectedSchool != null)
			{
				selectedSchool.SchoolApplicationComponents = await ConversionApplicationRetrievalService
					.GetSchoolApplicationComponents(appId, urn);

				PopulateUiModel(selectedSchool, conversionApplication);
			}
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
		
		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool, ConversionApplication? application)
		{
			SchoolName = selectedSchool.SchoolName;

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
