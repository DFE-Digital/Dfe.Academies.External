using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationComponent
{
    public ConversionApplicationComponent(string name)
    {
        Name = name;
    }

    public long? Id { get; set; }

    public ApplicationComponentsStatus Status { get; set; }

    /// <summary>
    /// E.g. Contact Details / performance and safeguarding / pupil numbers / finances etc...
    /// </summary>
    public string Name { get; set; }

    // TODO:- link to other object to capture details e.g.
}