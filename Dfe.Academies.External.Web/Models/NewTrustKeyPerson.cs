using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class NewTrustKeyPerson
{
	public NewTrustKeyPerson(string name, DateTime dateOfBirth, string biography, List<NewTrustKeyPersonRole> roles)
	{
		Name = name;
		DateOfBirth = dateOfBirth;
		Biography = biography;
		Roles = roles;
	}

	public int ApplicationId { get; set; }

	public string Name { get; set; }
	public List<NewTrustKeyPersonRole> Roles { get; }

	public DateTime DateOfBirth { get; set; }

	// MR:- Biography - KeyPersonBiography within A2C-SIP - ApplicationKeyPersons object
	public string Biography { get; set; } = string.Empty;
}
