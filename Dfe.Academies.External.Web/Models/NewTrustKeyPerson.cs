using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class NewTrustKeyPerson
{
	public NewTrustKeyPerson(string firstName, string surname, KeyPersonRole role)
	{
		Role = role;
		Person = new Person(firstName, surname);
	}

	public int Id { get; set; }

	public int TrustId { get; set; }

	/// <summary>
	/// Below replaces A2C-SIP bools
	/// </summary>
	public KeyPersonRole Role { get; set; }

	/// <summary>
	/// Taken from A2C-SIP - ApplicationKeyPersons object
	/// </summary>
	public string TimeInRole { get; set; } = string.Empty;

	public int PersonId { get; set; }
	public Person Person { get; set; }

	public DateTime? DateOfBirth { get; set; }

	// MR:- Biography - KeyPersonBiography within A2C-SIP - ApplicationKeyPersons object
	public string Biography { get; set; } = string.Empty;
}