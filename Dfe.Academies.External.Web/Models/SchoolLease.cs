namespace Dfe.Academies.External.Web.Models;

public class SchoolLease
{
	public SchoolLease(int leaseId,
		string leaseTerm, 
		decimal repaymentAmount,
		decimal interestRate, 
		decimal paymentsToDate, 
		string purpose, 
		string valueOfAssets,
		string responsibleForAssets)
	{
		LeaseId = leaseId;
		LeaseTerm = leaseTerm;
		RepaymentAmount = repaymentAmount;
		InterestRate = interestRate;
		PaymentsToDate = paymentsToDate;
		Purpose = purpose;
		ValueOfAssets = valueOfAssets;
		ResponsibleForAssets = responsibleForAssets;
	}

	public int LeaseId { get; set; }

	//// MR:- below props from A2C-SIP - SchoolLease object

	/// <summary>
	/// months or years ?
	/// </summary>
	public string LeaseTerm { get; set; }

	public decimal RepaymentAmount { get; set; }

	public decimal InterestRate { get; set; }
	public decimal PaymentsToDate { get; set; }

	public string Purpose { get; set; }

	public string ValueOfAssets { get; set; }

	public string ResponsibleForAssets { get; set; }

}
