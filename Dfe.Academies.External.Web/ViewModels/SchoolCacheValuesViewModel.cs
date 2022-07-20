namespace Dfe.Academies.External.Web.ViewModels;

public sealed class SchoolCacheValuesViewModel
{
	public SchoolCacheValuesViewModel()
	{

	}

	public SchoolCacheValuesViewModel(int schoolId, string schoolName)
	{
		SchoolId = schoolId;
		SchoolName = schoolName;
	}

	public int SchoolId { get; set; }

	/// <summary>
	/// Another unique school Id (6 digit number) e.g. 587634
	/// </summary>
	public int URN { get; set; }

	public string SchoolName { get; set; }
}