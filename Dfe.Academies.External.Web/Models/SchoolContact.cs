namespace Dfe.Academies.External.Web.Models;

public class SchoolContact
{
    public SchoolContact(int schoolId, string firstName, string surname)
    {
        SchoolId = schoolId;
        Person = new Person
        {
            FirstName = firstName,
            Surname = surname
        };
    }


    public int Id { get; set; }

    /// <summary>
    /// This would be existing Id from GIAS (?). 6 digit URN?
    /// </summary>
    public int SchoolId { get; set; }

    public bool IsMainContact { get; set; } = false;

    public bool IsContributor { get; set; } = false;

    public int PersonId { get; set; }
    public Person Person { get; set; }
}