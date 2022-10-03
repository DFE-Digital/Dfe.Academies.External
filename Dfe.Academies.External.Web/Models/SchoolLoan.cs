namespace Dfe.Academies.External.Web.Models;

public class SchoolLoan
{
	public SchoolLoan(decimal amount, string purpose, string provider, decimal interestRate, string schedule)
	{
		Amount = amount;
		Purpose = purpose;
		Provider = provider;
		InterestRate = interestRate;
		Schedule = schedule;
	}

	public int LoanId { get; set; }

	public int SchoolId { get; set; }

	//// MR:- below props from A2C-SIP - SchoolLoan object

	public decimal Amount { get; set; }

	public string Purpose { get; set; }

	public string Provider { get; set; }

	public decimal InterestRate { get; set; }

	public string Schedule { get; set; }

	public DateTime? EndDate { get; set; }

	/// <summary>
	/// months or years ?
	/// </summary>
	public short LeaseTerm { get; set; }
}
