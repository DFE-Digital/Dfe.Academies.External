using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class NewTrustKeyPerson : Person
{
    public NewTrustKeyPerson(string firstName, string surname, SchoolRoles role)
    {
        FirstName = firstName;
        Surname = surname;
        Role = role;
    }

    public int Id { get; set; }

    public SchoolRoles Role { get; set; }
}