using Dfe.Academies.External.Web.Enums;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

//// See:- https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-6.0

namespace Dfe.Academies.External.Web.TagHelpers;

/// <summary>
/// Helper func to spit out the class name:-
/// govuk-tag = completed background colour
/// govuk-tag--blue = in progress
/// govuk-tag--grey = not started
/// govuk-tag--grey = cannot start yet
/// </summary>
[HtmlTargetElement("status")]
public class CustomStatusTagHelper : TagHelper
{
	[HtmlAttributeName("enumvalue")]
	public string StatusEnumValue { get; set; } = string.Empty;

	public override void Init(TagHelperContext context)
	{
	}

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "strong";
		output.TagMode = TagMode.StartTagAndEndTag;

		var statusEnum = (Status)Convert.ToInt16(StatusEnumValue);

		switch (statusEnum)
		{
			case Status.NotStarted:
				output.AddClass("govuk-tag", HtmlEncoder.Default);
				output.AddClass("app-task-list__tag", HtmlEncoder.Default);
				output.AddClass("govuk-tag--grey", HtmlEncoder.Default);
				output.Content.Append("Not Started");
				return;
			case Status.InProgress:
				//// output.AddClass("govuk-tag app-task-list__tag govuk-tag--blue govuk-!-text-align-right", HtmlEncoder.Default);
				output.AddClass("govuk-tag", HtmlEncoder.Default);
				output.AddClass("app-task-list__tag", HtmlEncoder.Default);
				output.AddClass("govuk-tag--blue", HtmlEncoder.Default);
				output.Content.Append("In Progress");
				return;
			case Status.Completed:
				output.AddClass("govuk-tag", HtmlEncoder.Default);
				output.AddClass("app-task-list__tag", HtmlEncoder.Default);
				output.Content.Append("Completed");
				return;
			default:
				return;
		}

		//var sb = new StringBuilder();
		//sb.AppendFormat("<span>Hi! {0}</span>", this.Name);

		//output.PreContent.SetHtmlContent(sb.ToString());
	}
}
