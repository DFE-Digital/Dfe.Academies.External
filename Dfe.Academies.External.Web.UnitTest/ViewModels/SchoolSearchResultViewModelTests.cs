using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class SchoolSearchResultViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolSearchResultViewModel___PropertyCheck___Success()
	{
		// arrange
		string schoolName = Fixture.Create<string>();
		int urn = Fixture.Create<int>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();

		var schoolSearchResultViewModel = new SchoolSearchResultViewModel(schoolName, urn, street, town, fullUkPostcode)
		{
			Address3 = "address3",
			CountyDescription = "county",
			EstablishmentNumber = "7083",
			Locality = "local",
			UKPRN = "GAT00123"
		};

		// act
		// nothing!

		// assert
		Assert.That(schoolSearchResultViewModel, Is.Not.Null);
		Assert.That(schoolSearchResultViewModel.SchoolName, Is.EqualTo(schoolName));
		Assert.That(schoolSearchResultViewModel.URN, Is.EqualTo(urn));
		Assert.That(schoolSearchResultViewModel.Street, Is.EqualTo(street));
		Assert.That(schoolSearchResultViewModel.Town, Is.EqualTo(town));
		Assert.That(schoolSearchResultViewModel.FullUkPostcode, Is.EqualTo(fullUkPostcode));
		Assert.That(schoolSearchResultViewModel.DisplayName, Is.EqualTo($"{schoolName} ({urn})"));
	}
}