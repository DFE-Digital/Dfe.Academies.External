using AutoFixture;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;

namespace Dfe.Academies.External.Shared.Tests.Factory;

public static class ConversionApplicationFactory
{
	private static readonly Fixture Fixture = new();

	public static ConversionApplication BuildConversionApplication(ApplicationTypes applicationType)
	{
		return new ConversionApplication() {ApplicationType = applicationType };
	}
}