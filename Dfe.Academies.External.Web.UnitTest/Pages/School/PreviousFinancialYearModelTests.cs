using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class PreviousFinancialYearModelTests
{
	[Test]
    public void RunUiValidation_FileTooLargeInSchoolPFYRevenueStatusFiles_ReturnsError()
    {
        // Arrange
        var mockSharePointService = new Mock<ISharePointService>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        var pageModel = SetupPreviousFinancialYearModel(mockSharePointService.Object, mockConversionApplicationCreationService.Object,
            mockConversionApplicationRetrievalService.Object,
            mockReferenceDataRetrievalService.Object);
        TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, conversionApplication);

        // Create a mock file with size >= MaxFileUploadSizeInBytes
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
        fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

        pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile> { fileMock.Object };
        pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
        pageModel.PFYFinancialEndDateLocal = DateTime.Now;

        // ModelState must be valid before file size check
        pageModel.ModelState.Clear();

        // Act
        var isValid = pageModel.RunUiValidation();

        // Assert
        Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("SchoolPFYRevenueFileSizeError"), Is.True);
	}

	[Test]
	public void RunUiValidation_FileTooLargeInSchoolPFYCapitalForwardStatusFiles_ReturnsError()
	{
		// Arrange
		var mockSharePointService = new Mock<ISharePointService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		var pageModel = SetupPreviousFinancialYearModel(mockSharePointService.Object, mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, conversionApplication);

		// Create a mock file with size >= MaxFileUploadSizeInBytes
		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
		fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

		pageModel.SchoolPFYCapitalForwardStatusFiles = new List<IFormFile> { fileMock.Object };
		pageModel.SchoolPFYCapitalForwardStatusFileNames = new List<string>();
		pageModel.PFYFinancialEndDateLocal = DateTime.Now;

		// ModelState must be valid before file size check
		pageModel.ModelState.Clear();

		// Act
		var isValid = pageModel.RunUiValidation();

		// Assert
		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("SchoolPFYCapitalFileSizeError"), Is.True);
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
		var mockSharePointService = new Mock<ISharePointService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupPreviousFinancialYearModel(mockSharePointService.Object, mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task OnGetRemoveFileAsync_CallsDeleteFileAndRedirects()
	{
		const int appId = 5;
		const int urn = 100;
		var entityId = Guid.NewGuid().ToString();
		var applicationReference = "APP-001";
		var section = "revenue";
		var fileName = "revenue.pdf";
		var folderPath = FileUploadConstants.FormatSharepointSchoolDirectory(applicationReference, entityId);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock
			.Setup(x => x.DeleteFileAsync(folderPath, fileName))
			.Returns(Task.CompletedTask);

		var pageModel = SetupPreviousFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var result = await pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName);

		sharePointMock.Verify(
			x => x.DeleteFileAsync(folderPath, fileName),
			Times.Once);
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.PageName, Is.EqualTo("PreviousFinancialYear"));
		var routeValues = redirect.RouteValues!;
		Assert.That(routeValues["Urn"], Is.Not.Null);
		Assert.That(routeValues["Urn"], Is.EqualTo(urn));
		Assert.That(routeValues["AppId"], Is.EqualTo(appId));
	}

	[Test]
	public async Task OnGetRemoveFileAsync_WhenSharePointDeleteFails_ExceptionPropagates()
	{
		const int appId = 5;
		const int urn = 100;
		var entityId = Guid.NewGuid().ToString();
		var applicationReference = "APP-001";
		var section = "capital";
		var fileName = "capital.pdf";

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock
			.Setup(x => x.DeleteFileAsync(It.IsAny<string>(), fileName))
			.ThrowsAsync(new Exception("SharePoint delete failed"));

		var pageModel = SetupPreviousFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var exception = Assert.ThrowsAsync<Exception>(
			() => pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName));

		Assert.That(exception!.Message, Is.EqualTo("SharePoint delete failed"));
	}

	[Test]
	public async Task OnGetAsync_WhenSharePointThrowsException_ContinuesExecution()
	{
		const int appId = 10;
		const int urn = 200;
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ThrowsAsync(new Exception("SharePoint error"));

		var pageModel = SetupPreviousFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, application);

		var result = await pageModel.OnGetAsync(urn, appId);

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ApplicationId, Is.EqualTo(appId));
		Assert.That(pageModel.Urn, Is.EqualTo(urn));
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static PreviousFinancialYearModel SetupPreviousFinancialYearModel(
		ISharePointService mockSharePointService,
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new PreviousFinancialYearModel(mockSharePointService, Mock.Of<ILogger<PreviousFinancialYearModel>>(), mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
