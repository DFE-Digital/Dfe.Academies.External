using System.ComponentModel;

namespace Dfe.Academies.External.Web.Enums;

public enum ApplicationTypes 
{
    [Description("Join a multi-academy trust")]
    JoinMat = 1,
    [Description("Form a new multi-academy trust")]
    FormNewMat = 2,
    [Description("Form new single academy trust")]
    FormNewSingleAcademyTrust = 3
}

public enum Status 
{
    [Description("Cannot Start Yet")]
    CannotStartYet = 1,
    [Description("Not Started")]
    NotStarted = 2,
    [Description("In Progress")]
    InProgress = 3,
    [Description("Completed")]
    Completed = 4
}

public enum SchoolRoles
{
    [Description("The chair of the school's governors")]
    Chair = 1,
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
	No = 2
}

public enum SchoolConversionComponentStatus
{
	Ignore = -1,
	NotStarted = 0,
	Incomplete = 1,
	Complete = 2
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
	Trust = 2,
}
