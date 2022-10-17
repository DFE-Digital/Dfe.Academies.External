using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models;

public class DeleteLoanCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	[DataMember]
	public int SchoolId { get; set; }
	[DataMember]
	public int LoanId { get; set; }
}
