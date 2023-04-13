using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Commands;

public class DeleteSchoolCommand
{
	[DataMember]
	public int ApplicationId { get; set; }

	[DataMember]
	public int Urn { get; set; }
}
