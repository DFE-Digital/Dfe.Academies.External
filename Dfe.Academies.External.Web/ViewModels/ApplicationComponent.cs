using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public class ApplicationComponent
{
    public string Name { get; set; }

    public string URI { get; set; }

    public ApplicationComponentsStatus ApplicationComponentStatus { get; set; }
}