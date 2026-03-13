using Dfe.Academies.External.Web.Constants;
using Xunit;

namespace Dfe.Academies.External.Web.UnitTest.Constants
{
	public class ValidationMessageConstantsTests
	{
		[Fact]
		public void AllConstants_HaveExpectedValues()
		{
			Assert.Equal("You must provide trust benefit details", ValidationMessageConstants.TrustBenefitDetails);
			Assert.Equal("You must provide ofsted inspected details", ValidationMessageConstants.OfstedInspectedDetails);
			Assert.Equal("You must provide diocese name details", ValidationMessageConstants.DioceseNameDetails);
			Assert.Equal("You must provide safeguarding investigations details", ValidationMessageConstants.SafeguardingInvestigationsDetails);
			Assert.Equal("You must provide local authority reorganisation details", ValidationMessageConstants.LocalAuthorityReorganisationDetails);
			Assert.Equal("You must provide local authority closure plan details", ValidationMessageConstants.LocalAuthorityClosurePlanDetails);
			Assert.Equal("You must provide linked to diocese details", ValidationMessageConstants.LinkedToDioceseDetails);
			Assert.Equal("You must provide part of federation details", ValidationMessageConstants.PartOfFederationDetails);
			Assert.Equal("You must provide supported by foundation trust or body details", ValidationMessageConstants.SupportedByFoundationTrustOrBodyDetails);
			Assert.Equal("You must provide exemption from SACRE details", ValidationMessageConstants.ExemptionFromSACREDetails);
			Assert.Equal("You must provide equality assessment details", ValidationMessageConstants.EqualityAssessmentDetails);
			Assert.Equal("You must provide further information details", ValidationMessageConstants.FurtherInformationDetails);
			Assert.Equal("You must enter a conversion date", ValidationMessageConstants.MustHaveConversionDate);
			Assert.Equal("You must explain why you want to convert on this date", ValidationMessageConstants.MustHaveConversionDateExplained);
			Assert.Equal("You must provide main feeder school details", ValidationMessageConstants.MainFeederSchoolDetails);
			Assert.Equal("You must provide details when the governing body plans to consult.", ValidationMessageConstants.SchoolConsultationStakeholdersConsultDetails);
			Assert.Equal("Please select an option for the conversion date", ValidationMessageConstants.TargetDateDifferentDetails);
			Assert.Equal("Other contact email is not a valid e-mail address", ValidationMessageConstants.OtherContactInvalidEmail);
			Assert.Equal("You must provide the other contact's email", ValidationMessageConstants.MustHaveOtherContactEmail);
			Assert.Equal("You must provide current or planned building works details", ValidationMessageConstants.CurrentOrPlannedBuldingWorksDetails);
			Assert.Equal("You must provide the details of the owner or legal holder of the school's land and buildings", ValidationMessageConstants.SchoolBuildLandOwnerDetails);
			Assert.Equal("You must provide shared facilities on site details", ValidationMessageConstants.SchoolSharedFacilitiesDetails);
			Assert.Equal("You must provide any school building and land grant details", ValidationMessageConstants.SchoolBuildLandGrantsDetails);
			Assert.Equal("You must provide school PFI scheme details", ValidationMessageConstants.SchoolBuildLandPFISchemeDetails);
			Assert.Equal("You must provide priority school building programme details", ValidationMessageConstants.SchoolBuildLandPriorityBuildingProgrammeDetails);
			Assert.Equal("You must provide school's future programme details", ValidationMessageConstants.SchoolBuildLandFutureProgrammeDetails);
			Assert.Equal("Please select an option for existing loans", ValidationMessageConstants.MustChooseExistingLoanOption);
			Assert.Equal("You must provide existing loans details", ValidationMessageConstants.AddLoanDetails);
			Assert.Equal("Please choose otpion if the school will change its name", ValidationMessageConstants.MustChooseChangingSchoolNameOption);
			Assert.Equal("You must provide reasons for joining the trust", ValidationMessageConstants.ApplicationJoinTrustReasonsDetails);
		}
	}
}
