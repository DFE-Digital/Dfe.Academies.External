using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplication
{
    public long Id { get; set; }

    public ApplicationTypes ApplicationType { get; set; }

    public string? UserEmail { get; set; }
    public string? Application { get; set; }
    public string? TrustName { get; set; }
    
}