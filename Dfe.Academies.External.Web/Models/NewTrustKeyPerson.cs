using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class NewTrustKeyPerson
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SchoolRoles Role { get; set; }
}