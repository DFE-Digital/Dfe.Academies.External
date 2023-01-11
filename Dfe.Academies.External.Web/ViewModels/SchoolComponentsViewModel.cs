using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolComponentsViewModel
{
	public SchoolComponentsViewModel()
	{
		SchoolComponents = new List<ApplicationComponentViewModel>();
	}

	public SchoolComponentsViewModel(int applicationId, int urn, string name, Status status, List<ApplicationComponentViewModel> schoolComponents)
	{
		ApplicationId = applicationId;
		URN = urn;
		Name = name;
		Status = status;
		SchoolComponents = schoolComponents;
	}

	public int ApplicationId { get; set; }

	public int URN { get; set; }
	public string Name { get; set; }
	public Status Status { get; set; }

	public List<ApplicationComponentViewModel> SchoolComponents { get; set; }
}
