using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;
using System.Text;

namespace Dfe.Academies.External.Web.TagHelpers;

[HtmlTargetElement("a2c-date")]
public class A2CDateInputTagHelper : TagHelper
{
	/// <summary>
	/// The field name to which the data is bound
	/// </summary>
	public string FieldName { get; set; }

	/// <summary>
	/// The field data for displaying on view
	/// </summary>
	public string FieldData { get; set; }

	public string FieldDay { get; set; }

	public string FieldMonth { get; set; }

	public string FieldYear { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "";

		var hiddenDate = string.Empty;
		var day = string.Empty;
		var month = string.Empty;
		var year = string.Empty;


		if (!string.IsNullOrEmpty(this.FieldDay) || !string.IsNullOrEmpty(this.FieldMonth) || !string.IsNullOrEmpty(this.FieldYear))
		{
			day = this.FieldDay;
			month = this.FieldMonth;
			year = this.FieldYear;
		}
		else
		{
			if (!string.IsNullOrEmpty(this.FieldData))
			{
				DateTimeMultiFormatParser.TryParse(FieldData, out var date);
				
				day = date.Day.ToString();
				month = date.Month.ToString();
				year = date.Year.ToString();

				hiddenDate = date.ToString();
			}
		}

		var sb = new StringBuilder();
		sb.AppendFormat(CultureInfo.CurrentCulture,
			"<div class='govuk-date-input' id='conversion-date-different-date'>" +
			"<div class='govuk-date-input__item'>" +
			"<input id='{0}' name='{0}' type='hidden' value='{1}'>" +
			"<div class='govuk-form-group'>" +
			"<label class='govuk-label govuk-date-input__label' for='{0}-day'>Day</label>" +
			"<input name='{0}-day' class='govuk-input govuk-date-input__input govuk-input--width-2' id='{0}-day' type='text' pattern='[0-9]{{1,2}}' value='{2}'>" +
			"</div>" +
			"</div>" +
			"<div class='govuk-date-input__item'>" +
			"<div class='govuk-form-group'>" +
			"<label class='govuk-label govuk-date-input__label' for='{0}-month'>Month</label>" +
			"<input name='{0}-month' class='govuk-input govuk-date-input__input govuk-input--width-2' id='{0}-month' type='text' pattern='[0-9]{{1,2}}' value='{3}'>" +
			"</div>" +
			"</div>" +
			"<div class='govuk-date-input__item'>" +
			"<div class='govuk-form-group'>" +
			"<label class='govuk-label govuk-date-input__label' for='{0}-year'>Year</label>" +
			"<input name='{0}-year' class='govuk-input govuk-date-input__input govuk-input--width-4' id='{0}-year' type='text' pattern='[1-9][0-9]{{3}}' value='{4}'>" +
			"</div>" +
			"</div>" +
			"</div>", this.FieldName, hiddenDate, day, month, year);
		output.PreContent.SetHtmlContent(sb.ToString());
	}
}