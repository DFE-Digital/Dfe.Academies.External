using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationCacheValuesViewModelTests
{
	private static readonly Fixture Fixture = new();

	[Test]
	public void Constructor___PropertiesSet()
	{
		// arrange
		int applicationId = int.MaxValue;
		ApplicationTypes applicationType = ApplicationTypes.FormAMat;
		string applicationReference = Fixture.Create<string>();

		var conversionApplicationAuditEntry = new ApplicationCacheValuesViewModel(applicationId, applicationType, applicationReference);

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(conversionApplicationAuditEntry.ApplicationType, Is.EqualTo(applicationType));
		Assert.That(conversionApplicationAuditEntry.ApplicationReference, Is.EqualTo(applicationReference));
	}
}
