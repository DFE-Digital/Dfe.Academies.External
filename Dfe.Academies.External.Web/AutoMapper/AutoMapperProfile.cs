using AutoMapper;

namespace Dfe.Academies.External.Web.AutoMapper;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile() => AutoMapperSetup.AddMappings(this);
}
