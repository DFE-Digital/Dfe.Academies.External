using Dfe.Academies.External.Web.CustomValidators;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.CustomValidators;

internal sealed class ConfirmTrueAttributeTests
{
	[Test]
	public void ConfirmTrueAttribute___Success___ReturnsTrue()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmTrueAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(true);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(true));
	}

	[Test]
	public void ConfirmTrueAttribute___Success___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmTrueAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(false);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void ConfirmTrueAttribute___NonBoolean___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmTrueAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(null);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}
}