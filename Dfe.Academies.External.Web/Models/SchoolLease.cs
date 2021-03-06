namespace Dfe.Academies.External.Web.Models;

public class SchoolLease
{
	public SchoolLease(string purpose)
	{
		Purpose = purpose;
	}

    public int Id { get; set; }
    public int SchoolId { get; set; }

    //// MR:- below props from A2C-SIP - SchoolLease object

    /// <summary>
    /// months or years ?
    /// </summary>
    public short LeaseTerm { get; set; }

    public decimal? RepaymentAmount { get; set; }

    public decimal? InterestRate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Purpose { get; set; }
    
    public decimal? ValueOfAssets { get; set; }

    public string? ResponsibleForAssets { get; set; }

    public string? Provider { get; set; }
}