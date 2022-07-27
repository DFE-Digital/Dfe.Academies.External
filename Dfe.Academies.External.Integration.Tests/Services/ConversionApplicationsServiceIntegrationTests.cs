using Dfe.Academies.External.Shared.Tests.Factory;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Academies.External.Integration.Tests.Services
{
	internal sealed class ConversionApplicationsServiceIntegrationTests : BaseIntegrationTest
	{
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_factory.Dispose();
		}

		//[Test]
		public async Task CreateNewApplication_Success()
		{
			// arrange
			var conversionApplicationCreationService = _factory.Services.GetRequiredService<IConversionApplicationCreationService>();
			const ApplicationTypes applicationType = ApplicationTypes.JoinMat;

			// act
			var conversionApplication = await conversionApplicationCreationService.CreateNewApplication(
						ConversionApplicationFactory.BuildConversionApplication(applicationType)
				);

			// assert
			Assert.That(conversionApplication, Is.Not.Null);
		}

		// TODO MR:- UpdateDraftApplication()

		// TODO MR:- AddSchoolToApplication()
	}
}