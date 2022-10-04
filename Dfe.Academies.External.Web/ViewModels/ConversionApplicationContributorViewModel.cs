using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ConversionApplicationContributorViewModel
{
	public ConversionApplicationContributorViewModel(string fullName, SchoolRoles role, string? otherRole)
	{
		FullName = fullName;
		Role = role;
		OtherRoleNotListed = otherRole;
	}

	public string FullName { get; }

	private SchoolRoles Role { get; }

	private string? OtherRoleNotListed { get; }

	public string RoleName
	{
		get
		{
			if (Role == SchoolRoles.Other)
			{
				return $"{OtherRoleNotListed ?? string.Empty}";
			}
			else
			{
				return Role.GetDescription();
			}
		}
	}
}
