namespace Dfe.Academies.External.Web.Models;

public class SchoolReligiousEducation
{
    public int Id { get; set; }

    public int SchoolId { get; set; }

    public string FaithDioceseName { get; set; }

    public string DiocesePermissionEvidenceDocumentLink { get; set; }

    // TODO:- other props from A2C-SIP - ???
}