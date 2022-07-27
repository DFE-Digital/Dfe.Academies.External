using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class TrustFactory
	{
		public static IList<TrustSearchDto> BuildListTrustSummaryDto()
		{
			Fixture fixture = new();
			return new List<TrustSearchDto>
			{
				new()
				{
					UkPrn = fixture.Create<string>(),
					Urn = fixture.Create<string>(),
					GroupName = fixture.Create<string>(),
					CompaniesHouseNumber = fixture.Create<string>(),
					TrustType = fixture.Create<string>(),
					GroupContactAddress = GroupContactAddressFactory.BuildGroupContactAddressDto(),
					Establishments = EstablishmentFactory.BuildListEstablishmentSummaryDto()
				}
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