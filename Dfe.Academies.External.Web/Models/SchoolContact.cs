namespace Dfe.Academies.External.Web.Models;

public class SchoolContact
{
    public int Id { get; set; }

    /// <summary>
    /// This would be existing Id from GIAS (?). 6 digit URN?
    /// </summary>
    public int SchoolId { get; set; }

    public int ContactId { get; set; }

    public bool IsMainContact { get; set; } = false;

    public bool IsContributor { get; set; } = false;

    public int PersonId { get; set; }
    public Person Person { get; set; }
}