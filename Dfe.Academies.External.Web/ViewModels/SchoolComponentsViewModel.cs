namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolComponentsViewModel
{
	public SchoolComponentsViewModel()
	{
		SchoolComponents = new List<ApplicationComponentViewModel>();
	}

	public int ApplicationId { get; set; }

	public int URN { get; set; }

	public List<ApplicationComponentViewModel> SchoolComponents { get; set; }
}