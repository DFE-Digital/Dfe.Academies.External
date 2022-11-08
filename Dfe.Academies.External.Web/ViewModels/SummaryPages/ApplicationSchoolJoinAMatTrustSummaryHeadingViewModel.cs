namespace Dfe.Academies.External.Web.ViewModels.SummaryPages
{
	public sealed class ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel : SchoolQuestionAndAnswerSectionViewModel
	{
		public const string Heading = "..";
		public const string HeadingTrustSchoolIsJoining = "The trust the school is joining";
		public const string HeadingChangeTrustDetails = "Details";

		public ApplicationSchoolJoinAMatTrustSummaryHeadingViewModel(string title, string uRi) : base(title, uRi)
		{
			Sections = new();
		}

		public List<ApplicationSchoolJoinAMatTrustSummarySectionViewModel> Sections { get; set; }
	}
}
