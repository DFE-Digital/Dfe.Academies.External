using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class NewTrustKeyPerson : Person
{
    public NewTrustKeyPerson(string firstName, string surname, KeyPersonRole role)
    {
        FirstName = firstName;
        Surname = surname;
        Role = role;
    }

    public int Id { get; set; }

    /// <summary>
    /// Below replaces A2C-SIP bools
    /// </summary>
    public KeyPersonRole Role { get; set; }

    /// <summary>
    /// Taken from A2C-SIP - ApplicationKeyPersons object
    /// </summary>
    public string TimeInRole { get; set; }

    // TODO MR:- others ???
}