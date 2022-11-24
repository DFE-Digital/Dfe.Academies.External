using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ConversionApplicationContributorViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void RoleName___Valid___OtherRoleName()
	{
		// arrange
		int contributorId = Fixture.Create<int>();
		int applicationId = Fixture.Create<int>();
		string fullname = Fixture.Create<string>();
		SchoolRoles schoolRole = SchoolRoles.Other;
		string otherRoleNotListed = Fixture.Create<string>();
		string emailAddress = Fixture.Create<string>();

		var contributor = new ConversionApplicationContributorViewModel(contributorId, applicationId, 
			fullname, schoolRole, otherRoleNotListed, emailAddress);

		// act
		// nothing!

		// assert
		Assert.That(contributor, Is.Not.Null);
		Assert.That(contributor.ContributorId, Is.EqualTo(contributorId));
		Assert.That(contributor.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(contributor.FullName, Is.EqualTo(fullname));
		Assert.That(contributor.RoleName, Is.EqualTo($"{otherRoleNotListed}"));
		Assert.That(contributor.EmailAddress, Is.EqualTo(emailAddress));
	}

	[Test]
	public void RoleName___Valid___ChairOfGovernors()
	{
		// arrange
		int contributorId = Fixture.Create<int>();
		int applicationId = Fixture.Create<int>();
		string fullname = Fixture.Create<string>();
		SchoolRoles schoolRole = SchoolRoles.ChairOfGovernors;
		string emailAddress = Fixture.Create<string>();

		var contributor = new ConversionApplicationContributorViewModel(contributorId, applicationId, 
			fullname, schoolRole, null, emailAddress);

		// act
		// nothing!

		// assert
		Assert.That(contributor, Is.Not.Null);
		Assert.That(contributor.ContributorId, Is.EqualTo(contributorId));
		Assert.That(contributor.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(contributor.FullName, Is.EqualTo(fullname));
		Assert.That(contributor.RoleName, Is.EqualTo("The chair of the school's governors"));
		Assert.That(contributor.EmailAddress, Is.EqualTo(emailAddress));
	}
}
