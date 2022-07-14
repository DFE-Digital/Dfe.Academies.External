namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolCacheValuesViewModel
{
	public SchoolCacheValuesViewModel(int schoolId, string schoolName)
	{
		SchoolId = schoolId;
		SchoolName = schoolName;
	}

	public int SchoolId { get; set; }

	public string SchoolName { get; set; }
}