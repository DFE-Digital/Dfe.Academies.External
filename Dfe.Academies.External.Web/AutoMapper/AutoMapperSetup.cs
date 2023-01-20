using AutoMapper;
using Dfe.Academies.External.Web.Dtos;

namespace Dfe.Academies.External.Web.AutoMapper;

public static class AutoMapperSetup
{
	public static void AddMappings(Profile profile)
	{
		profile.CreateMap<EmailVariablesDto, Dictionary<string, dynamic>>()
			.ConvertUsing(x => new Dictionary<string, dynamic>
			{
				{"invitee_name", x.ContributorName},
				{"school", x.SchoolName},
				{"inviting_contributor", x.InvitingUsername}
			});
	}
}
