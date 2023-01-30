using System;
using AutoFixture;
using Dfe.Academies.External.Web.Dtos;
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
			ApplicationStatus = ApplicationStatus.InProgress,
			ApplicationReference = $"A2B_{int.MaxValue}"
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
			ApplicationReference = $"A2B_{int.MaxValue}",
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
			ApplicationReference = $"A2B_{int.MaxValue}",
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
			ApplicationStatus = ApplicationStatus.InProgress,
			ApplicationReference = $"A2B_{int.MaxValue}"
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
			ApplicationReference = $"A2B_{int.MaxValue}",
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			}
		};
	}

	public static ConversionApplication BuildJoinAMatConversionApplicationWithContributorWithSchool(int? applicationId)
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = applicationId.HasValue ? applicationId.Value : int.MaxValue,
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			ApplicationReference = $"A2B_{int.MaxValue}",
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			},
			Schools = new()
			{
				new SchoolApplyingToConvert(Fixture.Create<string>(), int.MaxValue, null)
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
			ApplicationReference = $"A2B_{int.MaxValue}",
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
			ApplicationReference = $"A2B_{int.MaxValue}",
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			},
			JoinTrustDetails = new ExistingTrust(int.MaxValue, Fixture.Create<string>(), Fixture.Create<int>())
		};
	}

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithMinimalAndTrustChangesJoinTrustDetails(int? applicationId)
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = applicationId.HasValue ? applicationId.Value : int.MaxValue,
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			ApplicationReference = $"A2B_{int.MaxValue}",
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			},
			JoinTrustDetails = new ExistingTrust(applicationId.HasValue ? applicationId.Value : int.MaxValue,
				Fixture.Create<string>(),
				Fixture.Create<int>(),
				TrustChange.No,
				null),
			Schools = new()
			{
				new SchoolApplyingToConvert(Fixture.Create<string>(), int.MaxValue, null)
			}
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
			ApplicationReference = $"A2B_{int.MaxValue}",
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

	public static ConversionApplication BuildNewJoinAMatConversionApplicationWithCompleteJoinTrustDetails(int? applicationId)
	{
		return new ConversionApplication
		{
			UserEmail = Fixture.Create<string>(),
			ApplicationId = applicationId.HasValue ? applicationId.Value : int.MaxValue,
			ApplicationType = ApplicationTypes.JoinAMat,
			ApplicationStatus = ApplicationStatus.InProgress,
			ApplicationReference = $"A2B_{int.MaxValue}",
			Contributors = new()
			{
				new ConversionApplicationContributor(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>(),SchoolRoles.Other, Fixture.Create<string>())
			},
			JoinTrustDetails = new ExistingTrust(applicationId.HasValue ? applicationId.Value : int.MaxValue,
				Fixture.Create<string>(),
				Fixture.Create<int>(),
				ChangesToTrust: TrustChange.No,
				ChangesToTrustExplained: null,
				ChangesToLaGovernance: false,
				ChangesToLaGovernanceExplained: null
			),
			Schools = new()
			{
				new SchoolApplyingToConvert(Fixture.Create<string>(), int.MaxValue, null)
			}
		};
	}
}
