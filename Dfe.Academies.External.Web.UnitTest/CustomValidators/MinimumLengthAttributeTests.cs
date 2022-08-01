using Dfe.Academies.External.Web.CustomValidators;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.CustomValidators;

internal sealed class MinimumLengthAttributeTests
{
    [Test]
    public void MinimumLengthAttribute___NullCheck___ReturnsFalse()
    {
	    // arrange
	    var minimumLengthAttribute = new MinimumLengthAttribute();

	    // act
		var result = minimumLengthAttribute.IsValid(null);

		// assert
		Assert.That(minimumLengthAttribute, Is.Not.Null);
	    Assert.That(result, Is.EqualTo(false));
    }
	
	[Test]
	public void MinimumLengthAttribute___EmptyCheck___ReturnsFalse()
	{
		// arrange
		var minimumLengthAttribute = new MinimumLengthAttribute();

		// act
		var result = minimumLengthAttribute.IsValid(string.Empty);

		// assert
		Assert.That(minimumLengthAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void MinimumLengthAttribute___LessThan4CharactersCheck___ReturnsFalse()
	{
		// arrange
		var minimumLengthAttribute = new MinimumLengthAttribute();

		// act
		var result = minimumLengthAttribute.IsValid("iso");

		// assert
		Assert.That(minimumLengthAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void MinimumLengthAttribute___MoreThan4CharactersCheck___ReturnsTrue()
	{
		// arrange
		var minimumLengthAttribute = new MinimumLengthAttribute();

		// act
		var result = minimumLengthAttribute.IsValid("isosceles");

		// assert
		Assert.That(minimumLengthAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(true));
	}
}