using System.ComponentModel;

namespace Dfe.Academies.External.Web.Enums;

public enum HelpTypes
{
	[Description("I want to report a problem with this form")]
	ProblemWithForm = 0,
	
	[Description("I need help with an application")]
	HelpWithApplication = 1,

	[Description("I have a comment or want to to suggest something")]
	CommentOrSuggestion = 2
}