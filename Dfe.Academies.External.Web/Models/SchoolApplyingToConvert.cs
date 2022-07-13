namespace Dfe.Academies.External.Web.Models
{
    public class SchoolApplyingToConvert
    {
	    public SchoolApplyingToConvert(string schoolName)
	    {
		    SchoolName = schoolName;
	    }

	    public int Id { get; set; }

	    /// <summary>
	    /// This would be existing Id from GIAS (?). 6 digit URN?
	    /// </summary>
	    public int SchoolId { get; set; }

	    public string SchoolName { get; set; }

	    public List<ConversionApplicationComponent> SchoolApplicationComponents { get; set; } = new();
    }
}
