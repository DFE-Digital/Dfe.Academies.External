using System.ComponentModel;

namespace Dfe.Academies.External.Web.Enums;

public enum ApplicationTypes : int
{
    [Description("Join a multi-academy trust")]
    JoinMat = 1,
    [Description("Form a new multi-academy trust")]
    FormNewMat = 2,
    [Description("Form new single academy trust")]
    FormNewSingleAcademyTrust = 3
}

public enum SchoolRoles : int
{
    [Description("The chair of the school's governors")]
    Chair = 1,
    [Description("Something Else")]
    Other = 2
}