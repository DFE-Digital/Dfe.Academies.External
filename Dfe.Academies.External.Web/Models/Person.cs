namespace Dfe.Academies.External.Web.Models;

public class Person
{
	public Person(string firstName, string surname)
	{
		FirstName = firstName;
		Surname = surname;
	}

	public int PersonId { get; set; }

	/// <summary>
	///  TODO MR:- grab name from DfE sign in ??
	/// </summary>
	public string FirstName { get; set; }

	public string Surname { get; set; }

	public string? ContactEmailAddress { get; set; }

	// TODO MR:- add ContactTelephone ?
}