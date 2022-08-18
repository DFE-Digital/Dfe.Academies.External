namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolLandAndBuildingsSummarySectionViewModel : SchoolQuestionAndAnswerViewModel
{
	public const string LandOwnership = "As far as you're aware, who owns or holds the school's buildings and land?";
	public const string PlannedBuildingWorks = "Are there any current or planned building works?";
	public const string PlannedBuildingWorksDetails = "Provide details of the works, how they’ll be funded and whether the funding will be affected by the conversion";
	public const string PlannedBuildingWorksWhen = "When is the scheduled completion date?";

	public const string SharedFacilities = "Are there any shared facilities on site?";
	public const string SharedFacilitiesList = "List these facilities and the school’s plan for them after converting";

	public const string Grants = "Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation??";
	public const string GrantBodies = "Which bodies awarded the grants and what facilities did they fund?";

	public const string PFI = "Is the school part of a Private Finance Initiative (PFI) scheme?";
	public const string PFIKind = "What kind of a PFI scheme is your school part of?";

	public const string PrioritySchoolBuildingProgram = "Is the school part of the Priority School Building Programme?";

	public const string BuildingSchoolsForTheFuture = "Is the school part of the Building Schools for the Future Programme?";

	public SchoolLandAndBuildingsSummarySectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}