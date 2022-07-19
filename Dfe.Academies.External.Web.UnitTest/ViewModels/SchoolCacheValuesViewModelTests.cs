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

		var conversionApplicationAuditEntry = new SchoolCacheValuesViewModel(schoolId, schoolName);

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.SchoolId, Is.EqualTo(schoolId));
		Assert.That(conversionApplicationAuditEntry.SchoolName, Is.EqualTo(schoolName));
	}
}