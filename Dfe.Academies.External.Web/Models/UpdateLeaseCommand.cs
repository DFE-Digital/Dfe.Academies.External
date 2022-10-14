using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models;

public class UpdateLeaseCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	[DataMember]
	public int SchoolId { get; set; }
	[DataMember]
	public int LeaseId { get; set; }
	[DataMember]
	public int LeaseTerm { get; set; }
	[DataMember]
	public decimal RepaymentAmount { get; set; }
	[DataMember]
	public decimal InterestRate { get; set; }
	[DataMember]
	public decimal PaymentsToDate { get; set; }
	[DataMember]
	public string Purpose { get; set; }
	[DataMember]
	public string ValueOfAssets { get; set; }
	[DataMember]
	public string ResponsibleForAssets { get; set; }
}
