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
    [Description("Something Else")]
    Other = 2
}
