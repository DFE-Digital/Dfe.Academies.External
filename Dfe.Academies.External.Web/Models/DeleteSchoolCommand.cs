using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models;

public class DeleteSchoolCommand
{
	[DataMember]
	public int ApplicationId { get; set; }

	[DataMember]
	public int Urn { get; set; }
}
