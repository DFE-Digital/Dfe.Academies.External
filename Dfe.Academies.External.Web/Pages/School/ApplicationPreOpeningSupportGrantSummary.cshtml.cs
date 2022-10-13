using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School;

public class ApplicationPreOpeningSupportGrantSummaryModel : BasePageEditModel
{
	private readonly ILogger<ApplicationPreOpeningSupportGrantSummaryModel> _logger;

	//// MR:- selected school props for UI rendering
	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public int Urn { get; set; }

	public string SchoolName { get; private set; } = string.Empty;

	//// MR:- VM props to show school conversion data
	public List<ApplicationPreOpeningSupportGrantHeadingViewModel> ViewModel { get; set; } = new();

	public ApplicationPreOpeningSupportGrantSummaryModel(ILogger<ApplicationPreOpeningSupportGrantSummaryModel> logger,
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
			_logger.LogError("School::ApplicationPreOpeningSupportGrantSummaryModel::OnGetAsync::Exception - {Message}", ex.Message);
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

		ApplicationPreOpeningSupportGrantHeadingViewModel heading1 = new(ApplicationPreOpeningSupportGrantHeadingViewModel.Heading,
			"/school/ApplicationPreOpeningSupportGrant"){
			Status = !string.IsNullOrEmpty(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
				SchoolConversionComponentStatus.Complete
				: SchoolConversionComponentStatus.NotStarted
		};
		
		heading1.Sections.Add(new(
			ApplicationPreOpeningSupportGrantSectionViewModel.FundsSchoolOrTrust,
			(string.IsNullOrWhiteSpace(selectedSchool.SchoolSupportGrantFundsPaidTo.ToString()) ?
				QuestionAndAnswerConstants.NoInfoAnswer : selectedSchool.SchoolSupportGrantFundsPaidTo?.GetDescription()) ?? string.Empty
		));

		var vm = new List<ApplicationPreOpeningSupportGrantHeadingViewModel> { heading1 };

		ViewModel = vm;
	}
}
