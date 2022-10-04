using Dfe.Academies.External.Web.Enums;
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
			    _logger.LogError("School::FinancesReviewModel::OnGetAsync::Exception - {Message}", ex.Message);
		    }
	    }

	    public override void PopulateValidationMessages()
	    {
		    PopulateViewDataErrorsWithModelStateErrors();
	    }

	    private FinancesReviewHeadingViewModel PopulatePreviousFinancialYear(SchoolApplyingToConvert selectedSchool)
	    {
		     var previousFinancialYear = selectedSchool.PreviousFinancialYear;

			// previous financial year - heading
			FinancesReviewHeadingViewModel PFYheading = new(FinancesReviewHeadingViewModel.HeadingPreviousFinancialYear,
				"/school/PreviousFinancialYear")
		    {
			    Status = previousFinancialYear.FinancialYearEndDate.HasValue ?
				    SchoolConversionComponentStatus.Complete
				    : SchoolConversionComponentStatus.NotStarted
		    };
			// PFYEndDate
			PFYheading.Sections.Add(new(FinancesReviewSectionViewModel.PFYEndDate,
				previousFinancialYear.FinancialYearEndDate.HasValue ?
					previousFinancialYear.FinancialYearEndDate.Value.ToShortDateString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//PFYRevenue
			PFYheading.Sections.Add(new(FinancesReviewSectionViewModel.PFYRevenue,
				previousFinancialYear.Revenue.HasValue ?
					previousFinancialYear.Revenue.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//PFYRevenueStatus
			//PFYRevenueStatusExplained - SubQ
			PFYheading.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(previousFinancialYear.RevenueStatus.HasValue ?
					previousFinancialYear.RevenueStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
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
			PFYheading.Sections.Add(new(FinancesReviewSectionViewModel.PFYCapitalCarryForward,
				previousFinancialYear.CapitalCarryForward.HasValue ?
					previousFinancialYear.CapitalCarryForward.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//PFYCapitalCarryForwardStatus
			//PFYCapitalCarryForwardExplained - SubQ
			PFYheading.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(previousFinancialYear.CapitalCarryForwardStatus.HasValue ?
					previousFinancialYear.CapitalCarryForwardStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
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

			return PFYheading;
	    }

	    private FinancesReviewHeadingViewModel PopulateCurrentFinancialYear(SchoolApplyingToConvert selectedSchool)
	    {
		    // current financial year - heading2
		    var currentFinancialYear = selectedSchool.CurrentFinancialYear;
		    FinancesReviewHeadingViewModel CFYheading = new(FinancesReviewHeadingViewModel.HeadingCurrentFinancialYear,
			    "/school/CurrentFinancialYear")
		    {
			    Status = currentFinancialYear.FinancialYearEndDate.HasValue ?
				    SchoolConversionComponentStatus.Complete
				    : SchoolConversionComponentStatus.NotStarted
		    };
		    
		    CFYheading.Sections.Add(new(FinancesReviewSectionViewModel.CFYEndDate,
			    currentFinancialYear.FinancialYearEndDate.HasValue ?
				    currentFinancialYear.FinancialYearEndDate.Value.ToShortDateString() : QuestionAndAnswerConstants.NoInfoAnswer)
		    );

		    CFYheading.Sections.Add(new(FinancesReviewSectionViewModel.CFYRevenue,
			    currentFinancialYear.Revenue.HasValue ?
				    currentFinancialYear.Revenue.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
		    );

		    CFYheading.Sections.Add(new(
			    FinancesReviewSectionViewModel.Status,
			    (currentFinancialYear.RevenueStatus.HasValue ?
				    currentFinancialYear.RevenueStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
		    )
		    {
			    SubQuestionAndAnswers = new()
			    {
				    new FinancesReviewSectionViewModel(
					    FinancesReviewSectionViewModel.PFYRevenueStatusExplained,
					    currentFinancialYear.RevenueStatusExplained ??
					    QuestionAndAnswerConstants.NoAnswer
				    )
			    }
		    });

		    CFYheading.Sections.Add(new(FinancesReviewSectionViewModel.CFYCapitalCarryForward,
			    currentFinancialYear.CapitalCarryForward.HasValue ?
				    currentFinancialYear.CapitalCarryForward.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
		    );

		    CFYheading.Sections.Add(new(
			    FinancesReviewSectionViewModel.Status,
			    (currentFinancialYear.CapitalCarryForwardStatus.HasValue ?
				    currentFinancialYear.CapitalCarryForwardStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
		    )
		    {
			    SubQuestionAndAnswers = new()
			    {
				    new FinancesReviewSectionViewModel(
					    FinancesReviewSectionViewModel.CFYCapitalCarryForwardExplained,
					    currentFinancialYear.CapitalCarryForwardExplained ??
					    QuestionAndAnswerConstants.NoAnswer
				    )
			    }
		    });
		    return CFYheading;
	    }

		private FinancesReviewHeadingViewModel PopulateNextFinancialYear(SchoolApplyingToConvert selectedSchool)
		{
			var nextFinancialYear = selectedSchool.CurrentFinancialYear;
			FinancesReviewHeadingViewModel NFYheading = new(FinancesReviewHeadingViewModel.HeadingNextFinancialYear,
				"/school/NextFinancialYear")
			{
				Status = nextFinancialYear.FinancialYearEndDate.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			// NFYEndDate
			NFYheading.Sections.Add(new(FinancesReviewSectionViewModel.NFYEndDate,
				nextFinancialYear.FinancialYearEndDate.HasValue ?
					nextFinancialYear.FinancialYearEndDate.Value.ToShortDateString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//NFYRevenue
			NFYheading.Sections.Add(new(FinancesReviewSectionViewModel.NFYRevenue,
				nextFinancialYear.Revenue.HasValue ?
					nextFinancialYear.Revenue.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//NFYRevenueStatus
			//NFYRevenueStatusExplained - SubQ
			NFYheading.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(nextFinancialYear.RevenueStatus.HasValue ?
					nextFinancialYear.RevenueStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.NFYRevenueStatusExplained,
						nextFinancialYear.RevenueStatusExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});
			//NFYCapitalCarryForward
			NFYheading.Sections.Add(new(FinancesReviewSectionViewModel.NFYCapitalCarryForward,
				nextFinancialYear.CapitalCarryForward.HasValue ?
					nextFinancialYear.CapitalCarryForward.Value.ToString() : QuestionAndAnswerConstants.NoInfoAnswer)
			);
			//NFYCapitalCarryForwardStatus
			//NFYCapitalCarryForwardExplained - SubQ
			NFYheading.Sections.Add(new(
				FinancesReviewSectionViewModel.Status,
				(nextFinancialYear.CapitalCarryForwardStatus.HasValue ?
					nextFinancialYear.CapitalCarryForwardStatus.Value.GetDescription() : QuestionAndAnswerConstants.NoInfoAnswer)
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.NFYCapitalCarryForwardExplained,
						nextFinancialYear.CapitalCarryForwardExplained ??
						QuestionAndAnswerConstants.NoAnswer
					)
				}
			});

			return NFYheading;
		}

		private FinancesReviewHeadingViewModel PopulateLoansHeading(SchoolApplyingToConvert selectedSchool)
		{
			
			FinancesReviewHeadingViewModel loansHeading = new(FinancesReviewHeadingViewModel.HeadingLoans,
				"/school/Loans")
			{
				Status = SchoolConversionComponentStatus.NotStarted
			};
			var isStarted = loansHeading.Status != SchoolConversionComponentStatus.NotStarted;

			loansHeading.Sections.Add(new(
				FinancesReviewSectionViewModel.LExistingLoans,
					isStarted
					? selectedSchool.Loans.Any() ? "Yes" : "No"
					: QuestionAndAnswerConstants.NoInfoAnswer)
			);
			
				var subQuestionAndAnswers = new List<SchoolQuestionAndAnswerViewModel>();
				
				for (int i = 0; i < selectedSchool.Loans.Count; i++)
				{
					subQuestionAndAnswers.AddRange(new List<SchoolQuestionAndAnswerViewModel>
					{
						new FinancesReviewSectionViewModel($"Loan {i+1}", ""),
						new FinancesReviewSectionViewModel("Total amount", $"£{selectedSchool.Loans.ElementAt(i).Amount}"),
						new FinancesReviewSectionViewModel("Purpose of loan", $"{selectedSchool.Loans.ElementAt(i).Purpose}"),
						new FinancesReviewSectionViewModel("Loan provider", $"{selectedSchool.Loans.ElementAt(i).Provider}"),
						new FinancesReviewSectionViewModel("Interest rate", $"{selectedSchool.Loans.ElementAt(i).InterestRate}"),
						new FinancesReviewSectionViewModel("Schedule of repayment", $"{selectedSchool.Loans.ElementAt(i).Schedule}")
					});
				}
				selectedSchool?.Loans.ForEach(x =>
				{
					
				});
				
				if(selectedSchool.Loans.Any())
					loansHeading.Sections.Add(new(FinancesReviewSectionViewModel.LExistingLoans, "Yes")
						{
							SubQuestionAndAnswers = subQuestionAndAnswers
						});

			return loansHeading;
		}

		private FinancesReviewHeadingViewModel PopulateLeasesHeading(SchoolApplyingToConvert selectedSchool)
		{
			var lease = selectedSchool.Leases.FirstOrDefault();
			FinancesReviewHeadingViewModel leasesHeading = new(FinancesReviewHeadingViewModel.HeadingLeases,
				"/school/Leases")
			{
				Status = SchoolConversionComponentStatus.NotStarted
			};

			// TODO:- API not done - 27/09/2022

			return leasesHeading;
		}

		private FinancesReviewHeadingViewModel PopulateFinancialInvestigationsHeading(SchoolApplyingToConvert selectedSchool)
		{
			FinancesReviewHeadingViewModel financialInvestigationsHeading = new(FinancesReviewHeadingViewModel.HeadingFinancialInvestigations,
				"/school/FinancialInvestigations")
			{
				Status = SchoolConversionComponentStatus.NotStarted
			};

			// TODO:- API not done - 27/09/2022

			financialInvestigationsHeading.Sections.Add(new(
				FinancesReviewSectionViewModel.FinanceOngoingInvestigations,
				(QuestionAndAnswerConstants.NoInfoAnswer)
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.FinancialInvestigationsExplain,
						QuestionAndAnswerConstants.NoAnswer
					),
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.FinancialInvestigationsTrustAware,
						QuestionAndAnswerConstants.NoAnswer
					),
				}
			});

			return financialInvestigationsHeading;
		}

		private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	    {
		    SchoolName = selectedSchool.SchoolName;
		    var PFYheading = PopulatePreviousFinancialYear(selectedSchool);
		    var CFYheading = PopulateCurrentFinancialYear(selectedSchool);
			var NFYheading = PopulateNextFinancialYear(selectedSchool);

			// loans - heading4
			var loansHeading = PopulateLoansHeading(selectedSchool);

			// leases - heading5
			var leasesHeading = PopulateLeasesHeading(selectedSchool);

			// financial investigations - heading6
			var financialInvestigationsHeading = PopulateFinancialInvestigationsHeading(selectedSchool);

			var vm = new List<FinancesReviewHeadingViewModel> { PFYheading, CFYheading, NFYheading, loansHeading, leasesHeading, financialInvestigationsHeading };

			ViewModel = vm;
	    }
	}
}
