using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class ConversionApplicationTestDataFactory
{
    private static readonly Fixture Fixture = new();
    private static readonly UniqueRecordIdentifierGenerator UniqueRecordIdentifierGenerator = new();

    public static ConversionApplication BuildNewConversionApplicationNoRoles()
    {
        return new ConversionApplication
        {
            UserEmail = Fixture.Create<string>(),
            ApplicationId = int.MaxValue,
            ApplicationType = ApplicationTypes.JoinAMat,
            ApplicationStatus = ApplicationStatus.InProgress
		};
    }

    public static ConversionApplication BuildNewConversionApplicationWithOtherRole()
    {
        return new ConversionApplication
        {
            UserEmail = Fixture.Create<string>(),
            ApplicationId = int.MaxValue,
            ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
            Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			}
        };
    }

    public static ConversionApplication BuildNewConversionApplicationWithChairRole()
    {
        return new ConversionApplication
        {
            UserEmail = Fixture.Create<string>(),
            ApplicationId = int.MaxValue,
            ApplicationType = ApplicationTypes.JoinAMat,
            ApplicationStatus = ApplicationStatus.InProgress,
			Contributors = new()
            {
	            new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(), SchoolRoles.ChairOfGovernors, null)
	        }
		};
    }
}
