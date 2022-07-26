using Dfe.Academies.External.Web.CustomValidators;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.CustomValidators;

internal sealed class ConfirmSelectionAttributeTests
{
	[Test]
	public void ConfirmSelectionAttribute___Success___ReturnsTrue()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmSelectionAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(true);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(true));
	}

	[Test]
	public void ConfirmSelectionAttribute___Success___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmSelectionAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(false);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void ConfirmSelectionAttribute___NonBoolean___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new ConfirmSelectionAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(null);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}
}