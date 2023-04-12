using System.ComponentModel;

namespace Dfe.Academies.External.Web.Enums;

public enum HelpTypes
{
	[Description("I want to report a problem with this form")]
	Problem,
	
	[Description("I need help with an application")]
	ApplicationHelp,

	[Description("I have a comment or want to to suggest something")]
	Feedback 
}