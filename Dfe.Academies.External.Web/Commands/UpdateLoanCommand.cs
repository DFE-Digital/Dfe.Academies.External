using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Commands;

public class UpdateLoanCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	[DataMember]
	public int SchoolId { get; set; }
	[DataMember]
	public int LoanId { get; set; }
	[DataMember]
	public decimal Amount { get; set; }
	[DataMember]
	public string Purpose { get; set; }
	[DataMember]
	public string Provider { get; set; }
	[DataMember]
	public decimal InterestRate { get; set; }
	[DataMember]
	public string Schedule { get; set; }
		
}
