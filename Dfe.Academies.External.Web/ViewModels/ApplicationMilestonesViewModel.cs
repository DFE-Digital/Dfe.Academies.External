namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationMilestonesViewModel
{
	public ApplicationMilestonesViewModel()
	{
		ApplicationMilestones = new();
	}

	public List<ApplicationMilestoneViewModel> ApplicationMilestones { get; set; }
}
