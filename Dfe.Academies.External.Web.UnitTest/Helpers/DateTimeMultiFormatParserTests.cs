using Dfe.Academies.External.Web.Helpers;
using NUnit.Framework;
using System;

namespace Dfe.Academies.External.Web.UnitTest.Helpers;

[Parallelizable(ParallelScope.All)]
internal sealed class DateTimeMultiFormatParserTests
{
	[Test]
	public void TryParse___InvalidDateString___ReturnsFalseAndEmptyDate()
	{
		// arrange
		string dateValue = "31/02/2022";

		// act
		var result = DateTimeMultiFormatParser.TryParse(dateValue, out var date);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(date, Is.Not.Null);
		Assert.That(result, Is.EqualTo(false));
		Assert.That(date, Is.EqualTo(DateTime.MinValue));
	}

	[Test]
	public void TryParse___ValidDateString___ReturnsTrueAndDate()
	{
		// arrange
		string dateValue = "01/02/2022";

		// act
		var result = DateTimeMultiFormatParser.TryParse(dateValue, out var date);

		// assert
		Assert.That(result, Is.Not.Null);
		Assert.That(date, Is.Not.Null);
		Assert.That(result, Is.EqualTo(true));
		Assert.That(date, Is.EqualTo(new DateTime(2022,2,1)));
	}
}