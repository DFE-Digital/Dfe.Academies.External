namespace Dfe.Academies.External.Web.Models;

public class SchoolLoan
{
    public int Id { get; set; }

    public int SchoolId { get; set; }

    //// MR:- below props from A2C-SIP - ApplyingSchool object

    /// <summary>
    /// months or years ?
    /// </summary>
    public short LeaseTerm { get; set; }

    public decimal? RepaymentAmount { get; set; }

    public string Purpose { get; set; }

    public string Provider { get; set; }

    public decimal? InterestRate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Schedule { get; set; }
}