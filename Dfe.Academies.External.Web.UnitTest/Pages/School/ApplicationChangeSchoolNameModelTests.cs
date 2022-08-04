using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationChangeSchoolNameModelTests
{
	/// <summary>
	/// "draftConversionApplication" in temp storage
	/// from previous step in the new application wizard
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task ApplicationChangeSchoolNameModel___OnGetAsync___Valid()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
		var mockLogger = new Mock<ILogger<ApplicationChangeSchoolNameModel>>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationChangeSchoolNameModel(mockLogger.Object,
															mockConversionApplicationRetrievalService.Object,
															mockReferenceDataRetrievalService.Object,
															mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO MR:- ApplicationChangeSchoolNameModel___OnPostAsync___ModelIsValid___InValid

	// TODO MR:- ApplicationChangeSchoolNameModel___OnPostAsync___ModelIsValid___Valid

	private static ApplicationChangeSchoolNameModel SetupApplicationChangeSchoolNameModel(
		ILogger<ApplicationChangeSchoolNameModel> mockLogger,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationCreationService academisationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationChangeSchoolNameModel(mockLogger,
													mockConversionApplicationRetrievalService,
													mockReferenceDataRetrievalService,
													academisationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}