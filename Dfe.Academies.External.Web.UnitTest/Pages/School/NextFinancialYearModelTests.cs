using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class NextFinancialYearModelTests
{
	[Test]
	public void RunUiValidation_ForecastedRevenueFileTooLarge_AddsModelError()
	{
		// Arrange
		var fileUploadServiceMock = new Mock<IFileUploadService>();
		var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
		var conversionAppServiceMock = new Mock<IConversionApplicationService>();

		var pageModel = SetupNextFinancialYearModel(
			fileUploadServiceMock.Object,
			conversionAppServiceMock.Object,
			conversionAppRetrievalServiceMock.Object,
			referenceDataRetrievalServiceMock.Object
		);

		// Mock a file that is too large
		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
		fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

		pageModel.ForecastedRevenueFiles = new List<IFormFile> { fileMock.Object };
		pageModel.NFYFinancialEndDateLocal = DateTime.Now;

		// Act
		var isValid = pageModel.RunUiValidation();

		// Assert
		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("ForecastedRevenueFileSizeError"), Is.True);
	}

	[Test]
	public void RunUiValidation_ForecastedCapitalFileTooLarge_AddsModelError()
	{
		// Arrange
		var fileUploadServiceMock = new Mock<IFileUploadService>();
		var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
		var conversionAppServiceMock = new Mock<IConversionApplicationService>();

		var pageModel = SetupNextFinancialYearModel(
			fileUploadServiceMock.Object,
			conversionAppServiceMock.Object,
			conversionAppRetrievalServiceMock.Object,
			referenceDataRetrievalServiceMock.Object
		);

		// Mock a file that is too large
		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
		fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

		pageModel.ForecastedCapitalFiles = new List<IFormFile> { fileMock.Object };
		pageModel.NFYFinancialEndDateLocal = DateTime.Now;

		// Act
		var isValid = pageModel.RunUiValidation();

		// Assert
		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("ForecastedCapitalFileSizeError"), Is.True);
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
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockFileUploadService = new Mock<IFileUploadService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(conversionApplication);
		// act
		var pageModel = SetupNextFinancialYearModel(mockFileUploadService.Object, mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}


	private static NextFinancialYearModel SetupNextFinancialYearModel(
		IFileUploadService mockFileUploadService,
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new NextFinancialYearModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService, mockFileUploadService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
