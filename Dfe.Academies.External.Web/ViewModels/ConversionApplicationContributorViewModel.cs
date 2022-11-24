using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web.ViewModels;

public sealed class ConversionApplicationContributorViewModel
{
	public ConversionApplicationContributorViewModel(int contributorId, int applicationId, 
		string fullName, SchoolRoles role, string? otherRole, string emailAddress)
	{
		ContributorId = contributorId;
		ApplicationId = applicationId;
		FullName = fullName;
		Role = role;
		OtherRoleNotListed = otherRole;
		EmailAddress = emailAddress;
	}

	public int ContributorId { get; set; }

	public int ApplicationId { get; set; }

	public string FullName { get; }

	public string EmailAddress { get; }

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
