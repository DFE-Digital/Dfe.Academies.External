using AutoFixture;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolConversionComponentSectionViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		string name = Fixture.Create<string>();
		string answer = Fixture.Create<string>();

		var conversionApplicationAuditEntry = new SchoolConversionComponentSectionViewModel(name, answer);

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.Name, Is.EqualTo(name));
		Assert.That(conversionApplicationAuditEntry.Answer, Is.EqualTo(answer));
	}
}