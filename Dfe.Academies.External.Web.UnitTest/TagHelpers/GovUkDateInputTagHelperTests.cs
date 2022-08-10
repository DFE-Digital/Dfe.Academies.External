using Dfe.Academies.External.Web.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.TagHelpers;

internal sealed class GovUkDateInputTagHelperTests
{
	[Test]
	public void Process___ValidDateStrings___ReturnsHtml()
	{
		// arrange
		string expectedHtml = 
			"<div class='govuk-date-input' id='govuk-date'><div class='govuk-date-input__item'><input id='SchoolConversionTargetDate' name='SchoolConversionTargetDate' type='hidden' value=''><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-day'>Day</label><input id='SchoolConversionTargetDate-day' name='SchoolConversionTargetDate-day' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-month'>Month</label><input id='SchoolConversionTargetDate-month' name='SchoolConversionTargetDate-month' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-year'>Year</label><input id='SchoolConversionTargetDate-year' name='SchoolConversionTargetDate-year' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-4' pattern='[1-9][0-9]{3}' value='2022'></div></div></div>";
		GovUkDateInputTagHelper tagHelper = new GovUkDateInputTagHelper
		{
			FieldName = "SchoolConversionTargetDate",
			FieldData = "TargetDate",
			FieldDay = "01",
			FieldMonth = "01",
			FieldYear = "2022"
		};

		var tagHelperContext = new TagHelperContext(
			new TagHelperAttributeList(),
			new Dictionary<object, object>(),
			Guid.NewGuid().ToString("N"));

		var tagHelperOutput = new TagHelperOutput("markdown",
			new TagHelperAttributeList(),
			(result, encoder) =>
			{
				var tagHelperContent = new DefaultTagHelperContent();
				tagHelperContent.SetHtmlContent(string.Empty);
				return Task.FromResult<TagHelperContent>(tagHelperContent);
			});

		// act
		tagHelper.Process(tagHelperContext, tagHelperOutput);

		// assert
		Assert.That(tagHelper, Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.EqualTo(string.Empty));

		Assert.AreEqual(expectedHtml, tagHelperOutput.PreContent.GetContent());
	}

	[Test]
	public void Process___FieldDayMissing___ReturnsHtml()
	{
		// arrange
		string expectedHtml =
			"<div class='govuk-date-input' id='govuk-date'><div class='govuk-date-input__item'><input id='SchoolConversionTargetDate' name='SchoolConversionTargetDate' type='hidden' value=''><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-day'>Day</label><input id='SchoolConversionTargetDate-day' name='SchoolConversionTargetDate-day' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value=''></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-month'>Month</label><input id='SchoolConversionTargetDate-month' name='SchoolConversionTargetDate-month' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-year'>Year</label><input id='SchoolConversionTargetDate-year' name='SchoolConversionTargetDate-year' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-4' pattern='[1-9][0-9]{3}' value='2022'></div></div></div>";
		GovUkDateInputTagHelper tagHelper = new GovUkDateInputTagHelper
		{
			FieldName = "SchoolConversionTargetDate",
			FieldData = "TargetDate",
			FieldMonth = "01",
			FieldYear = "2022"
		};

		var tagHelperContext = new TagHelperContext(
			new TagHelperAttributeList(),
			new Dictionary<object, object>(),
			Guid.NewGuid().ToString("N"));

		var tagHelperOutput = new TagHelperOutput("markdown",
			new TagHelperAttributeList(),
			(result, encoder) =>
			{
				var tagHelperContent = new DefaultTagHelperContent();
				tagHelperContent.SetHtmlContent(string.Empty);
				return Task.FromResult<TagHelperContent>(tagHelperContent);
			});

		// act
		tagHelper.Process(tagHelperContext, tagHelperOutput);

		// assert
		Assert.That(tagHelper, Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.EqualTo(string.Empty));

		Assert.AreEqual(expectedHtml, tagHelperOutput.PreContent.GetContent());
	}

	[Test]
	public void Process___FieldMonthMissing___ReturnsHtml()
	{
		// arrange
		string expectedHtml =
			"<div class='govuk-date-input' id='govuk-date'><div class='govuk-date-input__item'><input id='SchoolConversionTargetDate' name='SchoolConversionTargetDate' type='hidden' value=''><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-day'>Day</label><input id='SchoolConversionTargetDate-day' name='SchoolConversionTargetDate-day' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-month'>Month</label><input id='SchoolConversionTargetDate-month' name='SchoolConversionTargetDate-month' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value=''></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-year'>Year</label><input id='SchoolConversionTargetDate-year' name='SchoolConversionTargetDate-year' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-4' pattern='[1-9][0-9]{3}' value='2022'></div></div></div>";
		GovUkDateInputTagHelper tagHelper = new GovUkDateInputTagHelper
		{
			FieldName = "SchoolConversionTargetDate",
			FieldData = "TargetDate",
			FieldDay  = "01",
			FieldYear = "2022"
		};

		var tagHelperContext = new TagHelperContext(
			new TagHelperAttributeList(),
			new Dictionary<object, object>(),
			Guid.NewGuid().ToString("N"));

		var tagHelperOutput = new TagHelperOutput("markdown",
			new TagHelperAttributeList(),
			(result, encoder) =>
			{
				var tagHelperContent = new DefaultTagHelperContent();
				tagHelperContent.SetHtmlContent(string.Empty);
				return Task.FromResult<TagHelperContent>(tagHelperContent);
			});

		// act
		tagHelper.Process(tagHelperContext, tagHelperOutput);

		// assert
		Assert.That(tagHelper, Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.EqualTo(string.Empty));

		Assert.AreEqual(expectedHtml, tagHelperOutput.PreContent.GetContent());
	}

	[Test]
	public void Process___FieldYearMissing___ReturnsHtml()
	{
		// arrange
		string expectedHtml =
			"<div class='govuk-date-input' id='govuk-date'><div class='govuk-date-input__item'><input id='SchoolConversionTargetDate' name='SchoolConversionTargetDate' type='hidden' value=''><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-day'>Day</label><input id='SchoolConversionTargetDate-day' name='SchoolConversionTargetDate-day' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-month'>Month</label><input id='SchoolConversionTargetDate-month' name='SchoolConversionTargetDate-month' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-2' pattern='[0-9]{1,2}' value='01'></div></div><div class='govuk-date-input__item'><div class='govuk-form-group'><label class='govuk-label govuk-date-input__label' for='SchoolConversionTargetDate-year'>Year</label><input id='SchoolConversionTargetDate-year' name='SchoolConversionTargetDate-year' type='text' inputmode='numeric' class='govuk-input govuk-date-input__input govuk-input--width-4' pattern='[1-9][0-9]{3}' value=''></div></div></div>";
		GovUkDateInputTagHelper tagHelper = new GovUkDateInputTagHelper
		{
			FieldName = "SchoolConversionTargetDate",
			FieldData = "TargetDate",
			FieldDay = "01",
			FieldMonth = "01"
		};

		var tagHelperContext = new TagHelperContext(
			new TagHelperAttributeList(),
			new Dictionary<object, object>(),
			Guid.NewGuid().ToString("N"));

		var tagHelperOutput = new TagHelperOutput("markdown",
			new TagHelperAttributeList(),
			(result, encoder) =>
			{
				var tagHelperContent = new DefaultTagHelperContent();
				tagHelperContent.SetHtmlContent(string.Empty);
				return Task.FromResult<TagHelperContent>(tagHelperContent);
			});

		// act
		tagHelper.Process(tagHelperContext, tagHelperOutput);

		// assert
		Assert.That(tagHelper, Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.Null);
		Assert.That(tagHelperOutput.PreContent.GetContent(), Is.Not.EqualTo(string.Empty));

		Assert.AreEqual(expectedHtml, tagHelperOutput.PreContent.GetContent());
	}
}