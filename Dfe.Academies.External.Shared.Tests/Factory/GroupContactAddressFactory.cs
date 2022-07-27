using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory;

public static class GroupContactAddressFactory
{
	private static readonly Fixture Fixture = new();
	
	public static GroupContactAddressDto BuildGroupContactAddressDto()
	{
		return new GroupContactAddressDto(
			Fixture.Create<string>(), 
		Fixture.Create<string>(), 
		Fixture.Create<string>(), 
		Fixture.Create<string>(), 
		Fixture.Create<string>(), 
		Fixture.Create<string>());
	}
}
