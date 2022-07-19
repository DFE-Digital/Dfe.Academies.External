using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolSelectorViewModel
{
	[BindProperty]
	public int ApplicationId { get; set; }

	[BindProperty]
	public string? SchoolName { get; set; }

	[BindProperty]
	public bool? CorrectSchoolConfirmation { get; set; }

	/// <summary>
	/// Below contains name / URN / address
	/// </summary>
	public SchoolDetailsViewModel SelectedSchool { get; set; }
}