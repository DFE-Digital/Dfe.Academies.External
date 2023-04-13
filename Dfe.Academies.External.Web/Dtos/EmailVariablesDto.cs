namespace Dfe.Academies.External.Web.Dtos;

public record EmailVariablesDto
{
	public string ContributorName { get; set; }
	public string SchoolName { get; set; }
	public string InvitingUsername { get; set; }
}
