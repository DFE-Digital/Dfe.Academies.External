﻿using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class FinancesReviewModel : BasePageEditModel
	{
	    private readonly ILogger<FinancesReviewModel> _logger;

	    //// MR:- selected school props for UI rendering
	    [BindProperty]
	    public int ApplicationId { get; set; }

	    [BindProperty]
	    public int Urn { get; set; }

	    public string SchoolName { get; private set; } = string.Empty;

	    //// MR:- VM props to show school conversion data
	    public List<FinancesReviewHeadingViewModel> ViewModel { get; set; } = new();

	    public FinancesReviewModel(ILogger<FinancesReviewModel> logger,
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
			    _logger.LogError("School::LandAndBuildingsSummaryModel::OnGetAsync::Exception - {Message}", ex.Message);
		    }
	    }

	    public override void PopulateValidationMessages()
	    {
		    PopulateViewDataErrorsWithModelStateErrors();
	    }

	    private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	    {
		    SchoolName = selectedSchool.SchoolName;
		    var previousFinancialYear = selectedSchool.PreviousFinancialYear;

			// previous financial year
			FinancesReviewHeadingViewModel heading1 = new(FinancesReviewHeadingViewModel.HeadingPreviousFinancialYear,
				"/school/PreviousFinancialYear")
		    {
			    Status = previousFinancialYear.FinancialYearEndDate.HasValue ?
				    SchoolConversionComponentStatus.Complete
				    : SchoolConversionComponentStatus.NotStarted
		    };
			// PFYEndDate
			heading1.Sections.Add(new(FinancesReviewSectionViewModel.PFYEndDate,
				previousFinancialYear.FinancialYearEndDate.HasValue ?
					previousFinancialYear.FinancialYearEndDate.Value.ToShortDateString() : QuestionAndAnswerConstants.NoAnswer)
			);
			//PFYRevenue
			heading1.Sections.Add(new(FinancesReviewSectionViewModel.PFYRevenue,
				previousFinancialYear.Revenue.HasValue ?
					previousFinancialYear.Revenue.Value.ToString() : QuestionAndAnswerConstants.NoAnswer)
			);
			//PFYRevenueStatus
			//PFYRevenueStatusExplained - SubQ
			heading1.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(previousFinancialYear.RevenueStatus.HasValue ?
					previousFinancialYear.RevenueStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoAnswer)
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.PFYRevenueStatusExplained,
						previousFinancialYear.RevenueStatusExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});
			//PFYCapitalCarryForward
			heading1.Sections.Add(new(FinancesReviewSectionViewModel.PFYCapitalCarryForward,
				previousFinancialYear.CapitalCarryForward.HasValue ?
					previousFinancialYear.CapitalCarryForward.Value.ToString() : QuestionAndAnswerConstants.NoAnswer)
			);
			//PFYCapitalCarryForwardStatus
			//PFYCapitalCarryForwardExplained - SubQ
			heading1.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(previousFinancialYear.CapitalCarryForwardStatus.HasValue ?
					previousFinancialYear.CapitalCarryForwardStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoAnswer)
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.PFYCapitalCarryForwardExplained,
						previousFinancialYear.CapitalCarryForwardExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});


			// previous financial year - questions

			// current financial year - heading2

			// next financial year - heading3

			// loans - heading4

			// leases - heading5

			// financial investigations - heading6

			var vm = new List<FinancesReviewHeadingViewModel> { heading1 };

			ViewModel = vm;
	    }

	}
}