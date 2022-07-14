namespace Dfe.Academies.External.Web.Models;

public class ExistingTrust
{
    public ExistingTrust(string trustName)
    {
        TrustName = trustName;
    }

    public int Id { get; set; }

    /// <summary>
    /// This would be existing Id from KIM (?)
    /// </summary>
    public int TrustId { get; set; }

    public string TrustName { get; set; }

    // TODO:- other questions specific to trust
}