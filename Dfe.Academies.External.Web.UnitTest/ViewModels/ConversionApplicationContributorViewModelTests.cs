using System;
using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;
//using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ConversionApplicationContributorViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void RoleName___Valid___OtherRoleName()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		string fullname = Fixture.Create<string>();
		SchoolRoles schoolRole = SchoolRoles.Other;
		string otherRoleNotListed = Fixture.Create<string>();

		var contributor = new ConversionApplicationContributorViewModel(applicationId, fullname, schoolRole, otherRoleNotListed);

		// act
		// nothing!

		// assert
		Assert.That(contributor, Is.Not.Null);
		Assert.That(contributor.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(contributor.FullName, Is.EqualTo(fullname));
		Assert.That(contributor.RoleName, Is.EqualTo($"{otherRoleNotListed}"));
	}

	[Test]
	public void RoleName___Valid___ChairOfGovernors()
	{
		// arrange
		int applicationId = Fixture.Create<int>();
		string fullname = Fixture.Create<string>();
		SchoolRoles schoolRole = SchoolRoles.ChairOfGovernors;

		var contributor = new ConversionApplicationContributorViewModel(applicationId, fullname, schoolRole, null);

		// act
		// nothing!

		// assert
		Assert.That(contributor, Is.Not.Null);
		Assert.That(contributor.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(contributor.FullName, Is.EqualTo(fullname));
		Assert.That(contributor.RoleName, Is.EqualTo("The chair of the school's governors"));
	}
}
