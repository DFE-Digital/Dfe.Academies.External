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
		string name = Fixture.Create<string>();
		int urn = Fixture.Create<int>();
		string street = Fixture.Create<string>();
		string town = Fixture.Create<string>();
		string fullUkPostcode = Fixture.Create<string>();
		string UKPRN = "GAT00123";

		var schoolSearchResultViewModel = new SchoolSearchResultViewModel(urn, name, UKPRN)
		{
			Street = street,
			Town = town,
			FullUkPostcode = fullUkPostcode,
			Address3 = "address3",
			CountyDescription = "county",
			EstablishmentNumber = "7083",
			Locality = "local"
		};

		// act
		// nothing!

		// assert
		Assert.That(schoolSearchResultViewModel, Is.Not.Null);
		Assert.That(schoolSearchResultViewModel.SchoolName, Is.EqualTo(name));
		Assert.That(schoolSearchResultViewModel.URN, Is.EqualTo(urn));
		Assert.That(schoolSearchResultViewModel.Street, Is.EqualTo(street));
		Assert.That(schoolSearchResultViewModel.Town, Is.EqualTo(town));
		Assert.That(schoolSearchResultViewModel.FullUkPostcode, Is.EqualTo(fullUkPostcode));
		Assert.That(schoolSearchResultViewModel.DisplayName, Is.EqualTo($"{name} ({urn})"));
	}
}