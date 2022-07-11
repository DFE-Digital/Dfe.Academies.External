namespace Dfe.Academies.External.Web.Models;

public class NewTrust
{
    public int Id { get; set; }
    public string ProposedTrustName { get; set; }

    public DateTime? ProposedTrustOpeningDate { get; set; }

    public List<NewTrustKeyPerson> NewTrustKeyPersons { get; set; } = new();
}