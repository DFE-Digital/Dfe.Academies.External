using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationComponentViewModel
{
	public ApplicationComponentViewModel(string name, string uri, Status status)
	{
		Name = name;
		URI = uri;
		Status = status;
	}

	public string Name { get; set; }

	public string URI { get; set; }

	public Status Status { get; set; }
}
