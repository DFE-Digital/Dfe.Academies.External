using AutoFixture;
using Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class IfdDataFactory
	{
		private static readonly Fixture Fixture = new();

		public static IfdDataDto BuildIfdDataDto()
		{
			return new IfdDataDto(
				Fixture.Create<string>(),
				Fixture.Create<string>(),
				GroupContactAddressFactory.BuildGroupContactAddressDto());
		}
	}
}