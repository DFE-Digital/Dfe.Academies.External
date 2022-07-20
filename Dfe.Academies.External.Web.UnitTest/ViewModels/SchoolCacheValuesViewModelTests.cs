using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class SchoolCacheValuesViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void SchoolCacheValuesViewModel___PropertyCheck___Success()
	{
		// arrange
		int schoolId = Fixture.Create<int>();
		string schoolName = Fixture.Create<string>();
		int urn = Fixture.Create<int>();

		var schoolCacheValuesViewModel = new SchoolCacheValuesViewModel(schoolId, schoolName)
		{
			URN = urn
		};

		// act
		// nothing!

		// assert
		Assert.That(schoolCacheValuesViewModel, Is.Not.Null);
		Assert.That(schoolCacheValuesViewModel.SchoolId, Is.EqualTo(schoolId));
		Assert.That(schoolCacheValuesViewModel.SchoolName, Is.EqualTo(schoolName));
		Assert.That(schoolCacheValuesViewModel.URN, Is.EqualTo(urn));
	}
}