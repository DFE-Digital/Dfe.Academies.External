using System.Runtime.Serialization;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.Commands;

public class UpdateTrustKeyPersonCommand
{
	[DataMember]
	public int ApplicationId { get; set; }
	[DataMember]
	public int KeyPersonId { get; set; }
	[DataMember]
	public string Name { get; set; }
	[DataMember]
	public DateTime DateOfBirth { get; set; }
	[DataMember]
	public string Biography { get; set; }
	[DataMember]
	public IEnumerable<NewTrustKeyPersonRole> Roles  { get; set; }

}
