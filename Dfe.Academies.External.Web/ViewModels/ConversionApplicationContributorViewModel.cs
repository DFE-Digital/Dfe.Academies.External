using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ConversionApplicationContributorViewModel
{
	public ConversionApplicationContributorViewModel(string fullName, SchoolRoles role)
	{
		FullName = fullName;
		Role = role;
	}

	public string FullName { get; set; }

	public SchoolRoles Role { get; set; }

	public string? OtherRoleNotListed { get; set; }
}