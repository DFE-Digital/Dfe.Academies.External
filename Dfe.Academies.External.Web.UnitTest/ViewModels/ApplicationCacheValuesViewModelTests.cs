using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.ViewModels;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.ViewModels;

internal sealed class ApplicationCacheValuesViewModelTests
{
	[Test]
	public void ApplicationCacheValuesViewModel___PropertyCheck___Success()
	{
		// arrange
		int applicationId = int.MaxValue;
		ApplicationTypes applicationType = ApplicationTypes.FormNewMat;

		var conversionApplicationAuditEntry = new ApplicationCacheValuesViewModel(applicationId, applicationType);

		// act
		// nothing!

		// assert
		Assert.That(conversionApplicationAuditEntry, Is.Not.Null);
		Assert.That(conversionApplicationAuditEntry.ApplicationId, Is.EqualTo(applicationId));
		Assert.That(conversionApplicationAuditEntry.ApplicationType, Is.EqualTo(applicationType));
	}
}