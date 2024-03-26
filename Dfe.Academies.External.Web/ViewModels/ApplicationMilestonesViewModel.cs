namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationMilestonesViewModel
{
	public ApplicationMilestonesViewModel()
	{
		ApplicationMilestones = new()
			{
				new ApplicationMilestoneViewModel("ApplicationSubmission", "Application submission"),
				new ApplicationMilestoneViewModel("RSCDecision","Regional director makes a decision with advice from the Advisory Board"),
				new ApplicationMilestoneViewModel("AcademyOrder","Academy order is issued"),
				new ApplicationMilestoneViewModel("SubmitLandQuestionnaire","School’s solicitor submits a land questionnaire, including site plan"),
				new ApplicationMilestoneViewModel("SubmitDraftFundingAgreement","School’s solicitor submits draft funding agreement (and memorandum and articles of association for new trusts)"),
				new ApplicationMilestoneViewModel("ConfirmCTA","School’s solicitor confirms that the commercial transfer agreement (CTA) and land arrangements are agreed"),
				new ApplicationMilestoneViewModel("SignFundingAgreement","School signs and submits the funding agreement"),
				new ApplicationMilestoneViewModel("ConfirmTUPE","School’s solicitor confirms TUPE and stakeholder consultation are complete"),
				new ApplicationMilestoneViewModel("SubmitBankDetails","School submits the academy bank details to ESFA"),
				new ApplicationMilestoneViewModel("AcademyOpens","Academy Opens")
			};
	}

	public List<ApplicationMilestoneViewModel> ApplicationMilestones { get; set; }
}
