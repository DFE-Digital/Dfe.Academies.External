using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class SchoolDetailsViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string schoolName = Fixture.Create<string>();
		int urn = Fixture.Create<int>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();

		var schoolDetailsViewModel = new SchoolDetailsViewModel(schoolName, urn, street, town, fullUkPostcode)
			{
				Address3 = "address3",
				CountyDescription = "county",
				EstablishmentNumber = "7083"
			};

		// act
		// nothing!

		// assert
		Assert.That(schoolDetailsViewModel, Is.Not.Null);
		Assert.That(schoolDetailsViewModel.SchoolName, Is.EqualTo(schoolName));
		Assert.That(schoolDetailsViewModel.URN, Is.EqualTo(urn));
		Assert.That(schoolDetailsViewModel.Street, Is.EqualTo(street));
		Assert.That(schoolDetailsViewModel.Town, Is.EqualTo(town));
		Assert.That(schoolDetailsViewModel.FullUkPostcode, Is.EqualTo(fullUkPostcode));
	}
}