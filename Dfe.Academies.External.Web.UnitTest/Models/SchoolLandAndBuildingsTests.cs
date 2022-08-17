using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolLandAndBuildingsTests
{
	[Test]
	public void SchoolLandAndBuildings___PropertyCheck___Success()
	{
		// arrange
		var applicationComponent = new SchoolLandAndBuildings
		{
			ApplicationId = int.MaxValue,
			SchoolBuildLandPFIScheme = false
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.ApplicationId, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.SchoolBuildLandPFIScheme, Is.EqualTo(false));
	}
}