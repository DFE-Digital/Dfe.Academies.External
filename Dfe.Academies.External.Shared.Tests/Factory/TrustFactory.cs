using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class TrustFactory
	{
		public static IList<TrustSearchDto> BuildListTrustSummaryDto()
		{
			Fixture fixture = new Fixture();
			return new List<TrustSearchDto>
			{
				new TrustSearchDto(
					fixture.Create<string>(), 
					fixture.Create<string>(), 
					fixture.Create<string>(), 
					fixture.Create<string>(),
					fixture.Create<string>(),
					GroupContactAddressFactory.BuildGroupContactAddressDto(),
					EstablishmentFactory.BuildListEstablishmentSummaryDto())
			};
		}
		
		public static TrustSearch BuildTrustSearch(string groupName = "", string ukprn = "", string companiesHouseNumber = "")
		{
			return new TrustSearch(groupName, ukprn, companiesHouseNumber);
		}

		public static TrustDetailsDto BuildTrustDetailsDto(string ukPrn = "trust-ukprn")
		{
			return new TrustDetailsDto(
				GiasDataFactory.BuildGiasDataDto(ukPrn),
				IfdDataFactory.BuildIfdDataDto(),
				EstablishmentFactory.BuildListEstablishmentDto());
		}
	}
}