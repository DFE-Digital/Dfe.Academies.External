using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class EstablishmentFactory
	{
		private static readonly Fixture Fixture = new();
		
		public static EstablishmentSummaryDto BuildEstablishmentSummaryDto()
		{
			return new EstablishmentSummaryDto(Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>());
		}

		public static List<EstablishmentSummaryDto> BuildListEstablishmentSummaryDto()
		{
			return new List<EstablishmentSummaryDto> { new (Fixture.Create<string>(), Fixture.Create<string>(), Fixture.Create<string>()) };
		}

		public static EstablishmentDto BuildEstablishmentDto()
		{
			return new EstablishmentDto(
				Fixture.Create<string>(),
				Fixture.Create<string>(),
				Fixture.Create<string>(),
				Fixture.Create<EstablishmentTypeDto>()
			);
		}

		public static List<EstablishmentDto> BuildListEstablishmentDto()
		{
			return new List<EstablishmentDto>
			{
				BuildEstablishmentDto()
			};
		}
	}
}