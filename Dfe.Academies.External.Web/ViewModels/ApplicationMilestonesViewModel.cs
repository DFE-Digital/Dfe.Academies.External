namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ApplicationMilestonesViewModel
{
	public ApplicationMilestonesViewModel()
	{
		ApplicationMilestones = new();

		// TODO:- setup each milestone here as they are hard coded !!!!
		//ApplicationMilestones.Add(new ApplicationMilestoneViewModel());

		//'Sections':[

		//			{
		//	'SectionName':'ApplicationSubmission',
		//                      'SectionDisplayName':'Application submission',
		//			},
		//                  {
		//	'SectionName':'RSCDecision',
		//                      'SectionDisplayName':'Regional schools commissioner makes a decision with advice from the Headteacher Board',
		//			},
		//                  {
		//	'SectionName':'AcademyOrder',
		//                      'SectionDisplayName':'Academy order is issued',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'SubmitLandQuestionnaire',
		//                      'SectionDisplayName':'School’s solicitor submits a land questionnaire, including site plan',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'SubmitDraftFundingAgreement',
		//                      'SectionDisplayName':'School’s solicitor submits draft funding agreement (and memorandum and articles of association for new trusts)',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'ConfirmCTA',
		//                      'SectionDisplayName':'School’s solicitor confirms that the commercial transfer agreement (CTA) and land arrangements are agreed',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'SignFundingAgreement',
		//                      'SectionDisplayName':'School signs and submits the funding agreement',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'ConfirmTUPE',
		//                      'SectionDisplayName':'School’s solicitor confirms TUPE and stakeholder consultation are complete',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'SubmitBankDetails',
		//                      'SectionDisplayName':'School submits the academy bank details to ESFA',
		//                      'DynamicsFields':''

		//			},
		//                  {
		//	'SectionName':'AcademyOpens',
		//                      'SectionDisplayName':'Academy opens',
		//                      'DynamicsFields':''

		//			}]
	}

	public List<ApplicationMilestoneViewModel> ApplicationMilestones { get; set; }
}
