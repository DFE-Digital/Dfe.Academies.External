using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.Trust.JoinAMat;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using GovUK.Dfe.CoreLibs.SharePoint.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.JoinAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationSchoolTrustConsentTests
{
	[Test]
	public async Task OnGetRemoveFileAsync_CallsDeleteFileAndRedirects()
	{
		const int appId = 5;
		const int urn = 100;
		var entityId = Guid.NewGuid().ToString();
		var applicationReference = "APP-001";
		var section = "consent";
		var fileName = "consent.pdf";
		var folderPath = FileUploadConstants.FormatSharepointApplicationDirectory(applicationReference, entityId);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock
			.Setup(x => x.DeleteFileAsync(folderPath, fileName))
			.Returns(Task.CompletedTask);

		var pageModel = SetupApplicationSchoolTrustConsentModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var result = await pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName);

		sharePointMock.Verify(
			x => x.DeleteFileAsync(folderPath, fileName),
			Times.Once);
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.PageName, Is.EqualTo("ApplicationSchoolTrustConsent"));
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
		var section = "consent";
		var fileName = "consent.pdf";
		var folderPath = FileUploadConstants.FormatSharepointApplicationDirectory(applicationReference, entityId);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock
			.Setup(x => x.DeleteFileAsync(folderPath, fileName))
			.ThrowsAsync(new Exception("SharePoint delete failed"));

		var pageModel = SetupApplicationSchoolTrustConsentModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var exception = Assert.ThrowsAsync<Exception>(
			() => pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName));

		Assert.That(exception!.Message, Is.EqualTo("SharePoint delete failed"));
		sharePointMock.Verify(
			x => x.DeleteFileAsync(folderPath, fileName),
			Times.Once);
	}

	[Test]
	public async Task OnGetRemoveFileAsync_WithEmptyFileName_StillCallsDeleteAndRedirects()
	{
		const int appId = 5;
		const int urn = 100;
		var entityId = Guid.NewGuid().ToString();
		var applicationReference = "APP-001";
		var section = "consent";
		var fileName = "";
		var folderPath = FileUploadConstants.FormatSharepointApplicationDirectory(applicationReference, entityId);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock
			.Setup(x => x.DeleteFileAsync(folderPath, fileName))
			.Returns(Task.CompletedTask);

		var pageModel = SetupApplicationSchoolTrustConsentModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var result = await pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName);

		sharePointMock.Verify(
			x => x.DeleteFileAsync(folderPath, fileName),
			Times.Once);
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
	}

	[Test]
	public async Task OnGetAsync_WhenApplicationExists_ReturnsPageResult()
	{
		// Arrange
		const int appId = 5;
		const int urn = 100;
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<SchoolApplyingToConvert>
		{
			new("Test School", urn, null) { EntityId = Guid.NewGuid() }
		};

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock.Setup(x => x.ListFilesAsync(It.IsAny<string>()))
			.ReturnsAsync(new List<GovUK.Dfe.CoreLibs.SharePoint.Models.SharePointFileInfo>());

		var pageModel = SetupApplicationSchoolTrustConsentModel(
			sharePointMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		// Act
		var result = await pageModel.OnGetAsync(urn, appId);

		// Assert
		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ApplicationId, Is.EqualTo(appId));
		Assert.That(pageModel.Urn, Is.EqualTo(urn));
	}

	private static ApplicationSchoolTrustConsent SetupApplicationSchoolTrustConsentModel(
		ISharePointService sharePointService,
		IConversionApplicationRetrievalService conversionApplicationRetrievalService,
		IReferenceDataRetrievalService referenceDataRetrievalService,
		IConversionApplicationService conversionApplicationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationSchoolTrustConsent(
			conversionApplicationRetrievalService,
			referenceDataRetrievalService,
			conversionApplicationService,
			sharePointService,
			Mock.Of<ILogger<ApplicationSchoolTrustConsent>>())
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
