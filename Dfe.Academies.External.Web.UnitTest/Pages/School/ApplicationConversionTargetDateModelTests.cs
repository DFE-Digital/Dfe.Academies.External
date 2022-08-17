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

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class ApplicationConversionTargetDateModelTests
	{
		// MR:- test calculated props

		[Test]
		public async Task HasError_TargetDateExplainedError_Property__True()
		{
			// arrange
			var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
			var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
			var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
			var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
			var mockLogger = new Mock<ILogger<ApplicationConversionTargetDateModel>>();
			int urn = 101934;
			int applicationId = int.MaxValue;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

			// act
			var pageModel = SetupApplicationConversionTargetDateModel(mockLogger.Object,
				mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);

			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);
			pageModel.ModelState.AddModelError("TargetDateExplainedNotEntered", "You must provide details");

			// assert
			Assert.That(pageModel.TargetDateExplainedError, Is.EqualTo(true));
			Assert.That(pageModel.HasError, Is.EqualTo(true));
		}

		[Test]
		public async Task HasError_SchoolConversionTargetDateError_Property__True()
		{
			// arrange
			var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
			var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
			var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
			var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
			var mockLogger = new Mock<ILogger<ApplicationConversionTargetDateModel>>();
			int urn = 101934;
			int applicationId = int.MaxValue;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

			// act
			var pageModel = SetupApplicationConversionTargetDateModel(mockLogger.Object,
				mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);

			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);
			pageModel.ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must provide details");

			// assert
			Assert.That(pageModel.SchoolConversionTargetDateError, Is.EqualTo(true));
			Assert.That(pageModel.HasError, Is.EqualTo(true));
		}

		/// <summary>
		/// "draftConversionApplication" in temp storage
		/// from previous step in the new application wizard
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task OnGetAsync___Valid___NullErrors()
		{
			// arrange
			var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
			var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
			var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
			var mockConversionApplicationCreationService = new Mock<IConversionApplicationCreationService>();
			var mockLogger = new Mock<ILogger<ApplicationConversionTargetDateModel>>();
			int urn = 101934;
			int applicationId = int.MaxValue;

			var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

			// act
			var pageModel = SetupApplicationConversionTargetDateModel(mockLogger.Object,
				mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);
			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);

			// assert
			Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
		}

		// TODO MR:- OnPostAsync___ModelIsValid___Invalid
		// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

		// TODO MR:- OnPostAsync___ModelIsValid___Valid
		// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

		private static ApplicationConversionTargetDateModel SetupApplicationConversionTargetDateModel(
			ILogger<ApplicationConversionTargetDateModel> mockLogger,
			IConversionApplicationCreationService mockConversionApplicationCreationService,
			IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
			IReferenceDataRetrievalService mockReferenceDataRetrievalService,
			bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new ApplicationConversionTargetDateModel(mockLogger, mockConversionApplicationRetrievalService,
				mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}
