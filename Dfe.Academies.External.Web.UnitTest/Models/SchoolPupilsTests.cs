using Dfe.Academies.External.Web.Models;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Models;

internal sealed class SchoolPupilsTests
{
	[Test]
	public void SchoolPupils___PropertyCheck___Success()
	{
		// arrange
		int projectedPupilNumbersYear1 = 99;
		int projectedPupilNumbersYear2 = 101;
		int projectedPupilNumbersYear3 = 111;

		var applicationComponent = new SchoolPupils
		{
			Id = int.MaxValue,
			ProjectedPupilNumbersYear1 = projectedPupilNumbersYear1,
			ProjectedPupilNumbersYear2 = projectedPupilNumbersYear2,
			ProjectedPupilNumbersYear3 = projectedPupilNumbersYear3
		};

		// act
		// nothing!

		// assert
		Assert.That(applicationComponent, Is.Not.Null);
		Assert.That(applicationComponent.Id, Is.EqualTo(int.MaxValue));
		Assert.That(applicationComponent.ProjectedPupilNumbersYear1, Is.EqualTo(projectedPupilNumbersYear1));
		Assert.That(applicationComponent.ProjectedPupilNumbersYear2, Is.EqualTo(projectedPupilNumbersYear2));
		Assert.That(applicationComponent.ProjectedPupilNumbersYear3, Is.EqualTo(projectedPupilNumbersYear3));
	}
}