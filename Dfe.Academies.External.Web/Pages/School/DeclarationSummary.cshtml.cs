using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class DeclarationSummaryModel : BasePageEditModel
	{
	    private readonly ILogger<DeclarationSummaryModel> _logger;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

	    public List<DeclarationSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public DeclarationSummaryModel(ILogger<DeclarationSummaryModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
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
				_logger.LogError("School::DeclarationSummaryModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// TODO:- move code to here !!
			throw new NotImplementedException();
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

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			SchoolName = selectedSchool.SchoolName;

			DeclarationSummaryHeadingViewModel heading1 = new(DeclarationSummaryHeadingViewModel.Heading,
				"/school/Declaration")
			{
				Status = selectedSchool.DeclarationBodyAgree.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			// MR:- NO sub questions shown here, just yes / no for answer!
			heading1.Sections.Add(new(
				DeclarationSummaryHeadingViewModel.HeadingDetails,
				selectedSchool.DeclarationIAmTheChairOrHeadteacher.GetStringDescription()
			));

			var vm = new List<DeclarationSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
