using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Commands;

public class DeleteLeaseCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	[DataMember]
	public int SchoolId { get; set; }
	[DataMember]
	public int LeaseId { get; set; }
}
