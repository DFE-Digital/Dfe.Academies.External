using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationComponentViewModel
{
	public ApplicationComponentViewModel(string name, string uri)
	{
		Name = name;
		URI = uri;
	}

	public string Name { get; set; }

	public string URI { get; set; }

	public Status Status { get; set; }
}