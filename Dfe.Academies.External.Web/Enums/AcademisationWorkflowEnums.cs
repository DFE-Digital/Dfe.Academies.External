using System.ComponentModel;

namespace Dfe.Academies.External.Web.Enums;

public enum ApplicationTypes 
{
    [Description("Join a multi-academy trust")]
    JoinAMat,
    [Description("Form a new multi-academy trust")]
    FormAMat,
    [Description("Form new single academy trust")]
    FormASat
}

public enum Status 
{
    [Description("Cannot Start Yet")]
    CannotStartYet,
    [Description("Not Started")]
    NotStarted,
    [Description("In Progress")]
    InProgress,
    [Description("Completed")]
    Completed
}

public enum SchoolRoles
{
    [Description("The chair of the school's governors")]
    ChairOfGovernors = 1,
    [Description("Something else")]
    Other = 2
}

public enum KeyPersonRole
{
    [Description("CEO")]
    CEO = 1,
    [Description("The chair of the trust")]
    Chair = 2,
    [Description("Financial director")]
    FinancialDirector = 3,
    [Description("Trustee")]
    Trustee = 4,
    [Description("Other")]
    Other = 5
}

public enum SelectOption
{
	[Description("Yes")]
	Yes = 1,
	[Description("No")]
	No = 0
}

public enum SchoolConversionComponentStatus
{
	Ignore = -1,
	[Description("Not Started")]
	NotStarted,
	[Description("Incomplete")]
	Incomplete = 1,
	[Description("Complete")]
	Complete
}

public enum MainConversionContact
{
	[Description("The headteacher")]
	HeadTeacher = 1,
	[Description("The chair of the governing body")]
	ChairOfGoverningBody = 2,
	[Description("Someone else")]
	Other = 3
}

public enum PayFundsTo
{
	[Description("To the school")]
	School = 1,
	[Description("To the trust the school is joining")]
	Trust = 2
}

public enum ApplicationStatus
{
	[Description("In Progress")]
	InProgress,
	[Description("Submitted")]
	Submitted
}

public enum EqualityImpact
{
	[Description("Considered likely")]
	ConsideredUnlikely,
	[Description("Considered will not")]
	ConsideredWillNot,
	[Description("Not considered")]
	NotConsidered
}

public enum RevenueType
{
	[Description("Surplus")]
	Surplus = 1,
	[Description("Deficit")]
	Deficit = 2
}

