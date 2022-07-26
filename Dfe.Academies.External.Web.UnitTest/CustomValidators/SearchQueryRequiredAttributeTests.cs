using Dfe.Academies.External.Web.CustomValidators;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.CustomValidators;

internal sealed class SearchQueryRequiredAttributeTests
{
    [Test]
    public void SearchQueryRequiredAttribute___NullCheck___ReturnsFalse()
    {
	    // arrange
	    var searchQueryRequiredAttribute = new SearchQueryRequiredAttribute();

	    // act
		var result = searchQueryRequiredAttribute.IsValid(null);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
	    Assert.That(result, Is.EqualTo(false));
    }
	
	[Test]
	public void SearchQueryRequiredAttribute___EmptyCheck___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new SearchQueryRequiredAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid(string.Empty);

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void SearchQueryRequiredAttribute___LessThan4CharactersCheck___ReturnsFalse()
	{
		// arrange
		var searchQueryRequiredAttribute = new SearchQueryRequiredAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid("iso");

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
	}

	[Test]
	public void SearchQueryRequiredAttribute___MoreThan4CharactersCheck___ReturnsTrue()
	{
		// arrange
		var searchQueryRequiredAttribute = new SearchQueryRequiredAttribute();

		// act
		var result = searchQueryRequiredAttribute.IsValid("isosceles");

		// assert
		Assert.That(searchQueryRequiredAttribute, Is.Not.Null);
		Assert.That(result, Is.EqualTo(true));
	}
}