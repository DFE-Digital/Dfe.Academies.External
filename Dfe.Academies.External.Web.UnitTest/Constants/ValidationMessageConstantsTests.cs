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
			Assert.Equal("you must provide target date different details", ValidationMessageConstants.TargetDateDifferentDetails);
			Assert.Equal("Other contact email is not a valid e-mail address", ValidationMessageConstants.OtherContactInvalidEmail);
			Assert.Equal("You must provide the other contact’s email", ValidationMessageConstants.MustHaveOtherContactEmail);
		}
	}
}
