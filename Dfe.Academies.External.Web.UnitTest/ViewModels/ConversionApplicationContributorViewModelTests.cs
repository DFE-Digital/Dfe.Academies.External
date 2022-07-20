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

		var conversionApplicationContributorViewModel = new ConversionApplicationContributorViewModel(fullname, schoolRole)
		{
			OtherRoleNotListed = otherRoleNotListed
		};

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationContributorViewModel, Is.Not.Null);
		Assert.That(conversionApplicationContributorViewModel.FullName, Is.EqualTo(fullname));
		Assert.That(conversionApplicationContributorViewModel.Role, Is.EqualTo(schoolRole));
		Assert.That(conversionApplicationContributorViewModel.OtherRoleNotListed, Is.EqualTo(otherRoleNotListed));
	}
}