using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
namespace Dfe.Academies.External.Web.Pages.School
{
    public class FinancesReviewModel : BaseSchoolSummaryPageModel
	{
	    //// MR:- VM props to show school conversion data
	    public List<FinancesReviewHeadingViewModel> ViewModel { get; set; } = new();

	    public FinancesReviewModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		    IReferenceDataRetrievalService referenceDataRetrievalService)
		    : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
	    {
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
			var nextFinancialYear = selectedSchool.NextFinancialYear;
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
			// work out whether section started or not !
			//var isStarted = loansHeading.Status != SchoolConversionComponentStatus.NotStarted;
			bool? isStarted = selectedSchool.HasLoans;
			
			FinancesReviewHeadingViewModel loansHeading = new(FinancesReviewHeadingViewModel.HeadingLoans,
				"/school/Loans")
			{
				Status = isStarted.HasValue ? SchoolConversionComponentStatus.Complete : SchoolConversionComponentStatus.NotStarted
			};
			
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

			var financesReviewHeading = new FinancesReviewSectionViewModel(
					FinancesReviewSectionViewModel.ExistingLoans,
					selectedSchool.Loans.Any() ? "Yes" : "No");

			if (selectedSchool.Loans.Any())
				financesReviewHeading.SubQuestionAndAnswers = subQuestionAndAnswers;
				
			loansHeading.Sections.Add(financesReviewHeading);

			return loansHeading;
		}

		private FinancesReviewHeadingViewModel PopulateLeasesHeading(SchoolApplyingToConvert selectedSchool)
		{
			// work out whether section started or not !
			//var isStarted = leasesHeading.Status != SchoolConversionComponentStatus.NotStarted;
			bool? isStarted = selectedSchool.HasLeases;

			FinancesReviewHeadingViewModel leasesHeading = new(FinancesReviewHeadingViewModel.HeadingLeases,
				"/school/Leases")
			{
				Status = isStarted.HasValue ? SchoolConversionComponentStatus.Complete : SchoolConversionComponentStatus.NotStarted
			};
			
			var subQuestionAndAnswers = new List<SchoolQuestionAndAnswerViewModel>();
				
			for (int i = 0; i < selectedSchool.Leases.Count; i++)
			{
				subQuestionAndAnswers.AddRange(new List<SchoolQuestionAndAnswerViewModel>
				{
					new FinancesReviewSectionViewModel($"Lease {i+1}", ""),
					new FinancesReviewSectionViewModel("Details of the term of the finance lease agreement", $"{selectedSchool.Leases.ElementAt(i).LeaseTerm}"),
					new FinancesReviewSectionViewModel("Confirmation of the repayment value", $"{selectedSchool.Leases.ElementAt(i).RepaymentAmount}"),
					new FinancesReviewSectionViewModel("Confirmation of the interest rate chargeable", $"{selectedSchool.Leases.ElementAt(i).InterestRate}"),
					new FinancesReviewSectionViewModel("Details of the value of payments made to date", $"£{selectedSchool.Leases.ElementAt(i).PaymentsToDate}"),
					new FinancesReviewSectionViewModel("Details of the purpose of the finance lease", $"{selectedSchool.Leases.ElementAt(i).Purpose}"),
					new FinancesReviewSectionViewModel("Confirmation of the value of the assets at the initiation of the finance lease agreement", $"{selectedSchool.Leases.ElementAt(i).ValueOfAssets}"),
					new FinancesReviewSectionViewModel("Confirmation of who is responsible for the finance lease agreement", $"{selectedSchool.Leases.ElementAt(i).ResponsibleForAssets}")
				});
			}

			var financesReviewHeading = new FinancesReviewSectionViewModel(
				FinancesReviewSectionViewModel.ExistingLeases,
				selectedSchool.Leases.Any() ? "Yes" : "No");
			
			if (selectedSchool.Leases.Any())
				financesReviewHeading.SubQuestionAndAnswers = subQuestionAndAnswers;
				
			leasesHeading.Sections.Add(financesReviewHeading);

			return leasesHeading;
		}

		private FinancesReviewHeadingViewModel PopulateFinancialInvestigationsHeading(SchoolApplyingToConvert selectedSchool)
		{
			FinancesReviewHeadingViewModel financialInvestigationsHeading = new(FinancesReviewHeadingViewModel.HeadingFinancialInvestigations,
				"/school/FinancialInvestigations")
			{
				Status = selectedSchool.FinanceOngoingInvestigations.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			financialInvestigationsHeading.Sections.Add(new(
				FinancesReviewSectionViewModel.FinanceOngoingInvestigations,
				selectedSchool.FinanceOngoingInvestigations.GetStringDescription()
			)
			{
				SubQuestionAndAnswers = new()
				{
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.FinancialInvestigationsExplain,
						(!string.IsNullOrWhiteSpace(selectedSchool.FinancialInvestigationsExplain) ?
							selectedSchool.FinancialInvestigationsExplain :
							QuestionAndAnswerConstants.NoAnswer)
					),
					new FinancesReviewSectionViewModel(
						FinancesReviewSectionViewModel.FinancialInvestigationsTrustAware,
						selectedSchool.FinancialInvestigationsTrustAware.HasValue ?
							selectedSchool.FinancialInvestigationsTrustAware.GetStringDescription() :
							QuestionAndAnswerConstants.NoAnswer
					),
				}
			});

			return financialInvestigationsHeading;
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
	    {
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
