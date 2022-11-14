namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationMilestoneViewModel
{
	public ApplicationMilestoneViewModel(string sectionName, string sectionDisplayName)
	{
		SectionName = sectionName;
		SectionDisplayName = sectionDisplayName;
	}

	public string SectionName { get; set; }

	public string SectionDisplayName { get; set; }
}
