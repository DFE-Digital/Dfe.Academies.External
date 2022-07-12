namespace Dfe.Academies.External.Web.Models;

public class SchoolFinances
{
    public int Id { get; set; }
    public int SchoolId { get; set; }

    //// MR:- below props from
    //// https://github.com/DFE-Digital/trams-data-api/blob/main/TramsDataApi/DatabaseModels/A2BApplicationApplyingSchool.cs
    public decimal? SchoolPFYRevenue { get; set; }
    public bool? SchoolPFYRevenueIsDeficit { get; set; }
    public string SchoolPFYRevenueStatusExplained { get; set; }

    public decimal? SchoolPFYCapitalForward { get; set; }
    public bool? SchoolPFYCapitalIsDeficit { get; set; }
    public string SchoolPFYCapitalForwardStatusExplained { get; set; }
    public DateTime? SchoolPFYEndDate { get; set; }

    public decimal? SchoolCFYRevenue { get; set; }
    public bool? SchoolCFYRevenueIsDeficit { get; set; }
    public string SchoolCFYRevenueStatusExplained { get; set; }

    public decimal? SchoolCFYCapitalForward { get; set; }
    public bool? SchoolCFYCapitalIsDeficit { get; set; }
    public string SchoolCFYCapitalForwardStatusExplained { get; set; }
    public DateTime? SchoolCFYEndDate { get; set; }

    public decimal? SchoolNFYRevenue { get; set; }
    public bool? SchoolNFYRevenueIsDeficit { get; set; }
    public string SchoolNFYRevenueStatusExplained { get; set; }

    public decimal? SchoolNFYCapitalForward { get; set; }
    public bool? SchoolNFYCapitalIsDeficit { get; set; }
    public string SchoolNFYCapitalForwardStatusExplained { get; set; }
    public DateTime? SchoolNFYEndDate { get; set; }
    public bool? SchoolFinancialInvestigations { get; set; } // int?
    public string SchoolFinancialInvestigationsExplain { get; set; }
    public bool? SchoolFinancialInvestigationsTrustAware { get; set; }

    //// MR:- below props from
    //// https://github.com/DFE-Digital/trams-data-api/blob/main/TramsDataApi/DatabaseModels/AcademyConversionProject.cs
    public decimal? RevenueCarryForwardAtEndMarchCurrentYear { get; set; }
    public decimal? ProjectedRevenueBalanceAtEndMarchNextYear { get; set; }
    public decimal? CapitalCarryForwardAtEndMarchCurrentYear { get; set; }
    public decimal? CapitalCarryForwardAtEndMarchNextYear { get; set; }
    public string SchoolBudgetInformationAdditionalInformation { get; set; }
    public bool? SchoolBudgetInformationSectionComplete { get; set; }
}