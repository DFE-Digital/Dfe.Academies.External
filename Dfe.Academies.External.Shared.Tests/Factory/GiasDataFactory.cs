using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class GiasDataFactory
	{
		private static readonly Fixture Fixture = new();
		
		public static GiasDataDto BuildGiasDataDto(string ukPrn = "trust-ukprn")
		{
			return new GiasDataDto(ukPrn, 
				Fixture.Create<string>(), 
				Fixture.Create<string>(), 
				Fixture.Create<string>(), 
				Fixture.Create<string>(),
				GroupContactAddressFactory.BuildGroupContactAddressDto());
		}
	}
}