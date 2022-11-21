using System;
using System.Collections.Generic;
using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Web.UnitTest.Factories;

internal static class ConversionApplicationTestDataFactory
{
	private static readonly Fixture Fixture = new();

	public static ConversionApplication BuildMinimalFormAMatConversionApplicationNoContributors()
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = int.MaxValue,
			ApplicationType = ApplicationTypes.FormAMat,
			ApplicationStatus = ApplicationStatus.InProgress
		};
	}

	public static ConversionApplication BuildMinimalFormAMatConversionApplicationWithContributor()
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = int.MaxValue,
			ApplicationType = ApplicationTypes.FormAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			}
		};
	}

	public static ConversionApplication BuildFormAMatConversionApplicationWithContributorWithSchool()
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = int.MaxValue,
			ApplicationType = ApplicationTypes.FormAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			},
			Schools = new ()
			{
				new SchoolApplyingToConvert(Fixture.Create<string>(), Int32.MaxValue, null)
			}
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationNoRoles()
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = int.MaxValue,
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithOtherRole()
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

	public static ConversionApplication BuildJoinAMatConversionApplicationWithContributorWithSchool()
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
			},
			Schools = new()
			{
				new SchoolApplyingToConvert(Fixture.Create<string>(), Int32.MaxValue, null)
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

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithMinimalJoinTrustDetails()
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
			},
			JoinTrustDetails = new ExistingTrust(int.MaxValue, Fixture.Create<string>(), Fixture.Create<int>())
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails()
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
			},
			JoinTrustDetails = new ExistingTrust(int.MaxValue,
				Fixture.Create<string>(),
				Fixture.Create<int>(),
				TrustChange.No,
				null)
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithMinimalAndChangesToLaGovernanceJoinTrustDetails()
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
			},
			JoinTrustDetails = new ExistingTrust(int.MaxValue, 
				Fixture.Create<string>(),
				Fixture.Create<int>(),
				ChangesToTrust: null,
				ChangesToTrustExplained:null,
				ChangesToLaGovernance: false,
				ChangesToLaGovernanceExplained: null
				)
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails()
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
			},
			JoinTrustDetails = new ExistingTrust(int.MaxValue,
				Fixture.Create<string>(),
				Fixture.Create<int>(),
				ChangesToTrust: TrustChange.No,
				ChangesToTrustExplained: null,
				ChangesToLaGovernance: false,
				ChangesToLaGovernanceExplained: null
			)
		};
	}
}
