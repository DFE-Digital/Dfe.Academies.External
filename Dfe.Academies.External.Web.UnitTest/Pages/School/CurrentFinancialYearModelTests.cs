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

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class CurrentFinancialYearModelTests
	{
		[Test]
		public void RunUiValidation_ForecastedRevenueFileTooLarge_AddsModelError()
		{
			// Arrange
			var sharePointServiceMock = new Mock<ISharePointService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupCurrentFinancialYearModel(
				sharePointServiceMock.Object,
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.SchoolCfyRevenueStatusFiles = new List<IFormFile> { fileMock.Object };
			model.CFYFinancialEndDateLocal = DateTime.Now;

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("SchoolCFYRevenueFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_ForecastedCapitalFileTooLarge_AddsModelError()
		{
			// Arrange
			var sharePointServiceMock = new Mock<ISharePointService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupCurrentFinancialYearModel(
				sharePointServiceMock.Object,
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.SchoolCFYCapitalForwardFiles = new List<IFormFile> { fileMock.Object };
			model.CFYFinancialEndDateLocal = DateTime.Now;

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

		// Assert
		Assert.That(isValid, Is.False);
		Assert.That(model.ModelState.ContainsKey("SchoolCFYCapitalFileSizeError"), Is.True);
	}

	[Test]
	public async Task OnGetAsync_WhenApplicationNotFound_ThrowsNullReferenceException()
	{
		const int appId = 10;
		const int urn = 200;

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync((Dfe.Academies.External.Web.Dtos.ConversionApplication?)null);

		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new Dfe.Academies.External.Web.Dtos.ConversionApplication());

		// The code tries to access ApplicationReference on a null applicationDetails object  
		Assert.ThrowsAsync<NullReferenceException>(async () => await model.OnGetAsync(urn, appId));
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

		var model = SetupCurrentFinancialYearModel(
			sharePointMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, application);

		var result = await model.OnGetAsync(urn, appId);

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(model.ApplicationId, Is.EqualTo(appId));
		Assert.That(model.Urn, Is.EqualTo(urn));
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

		var model = SetupCurrentFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var result = await model.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName);

		sharePointMock.Verify(
			x => x.DeleteFileAsync(folderPath, fileName),
			Times.Once);
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.PageName, Is.EqualTo("CurrentFinancialYear"));
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

		var model = SetupCurrentFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var exception = Assert.ThrowsAsync<Exception>(
			() => model.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName));

		Assert.That(exception!.Message, Is.EqualTo("SharePoint delete failed"));
	}

	private static CurrentFinancialYearModel SetupCurrentFinancialYearModel(
			ISharePointService mockSharePointService,
			IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationCreationService,
			bool isAuthenticated = false
		)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new CurrentFinancialYearModel(
				Mock.Of<ILogger<CurrentFinancialYearModel>>(),
				mockSharePointService,
				mockConversionApplicationRetrievalService,
				referenceDataRetrievalService,
				conversionApplicationCreationService
			)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}
