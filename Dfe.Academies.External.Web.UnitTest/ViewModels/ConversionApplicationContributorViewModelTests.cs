using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ConversionApplicationContributorViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void ConversionApplicationContributorViewModel___PropertyCheck___Success()
	{
		// arrange
		string fullname = Fixture.Create<string>();
		SchoolRoles schoolRole = Fixture.Create<SchoolRoles>();
		string otherRoleNotListed = Fixture.Create<string>();

		var conversionApplicationAuditEntry = new ConversionApplicationContributorViewModel(fullname, schoolRole)
		{
			OtherRoleNotListed = otherRoleNotListed
		};

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.FullName, Is.EqualTo(fullname));
		Assert.That(conversionApplicationAuditEntry.Role, Is.EqualTo(schoolRole));
		Assert.That(conversionApplicationAuditEntry.OtherRoleNotListed, Is.EqualTo(otherRoleNotListed));
	}
}