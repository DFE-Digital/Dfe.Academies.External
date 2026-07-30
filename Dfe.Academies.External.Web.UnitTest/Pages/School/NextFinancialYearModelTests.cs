using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
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
using Microsoft.Extensions.Primitives;
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
		var sharePointServiceMock = new Mock<ISharePointService>();
		var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
		var conversionAppServiceMock = new Mock<IConversionApplicationService>();

		var pageModel = SetupNextFinancialYearModel(
			sharePointServiceMock.Object,
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
		var sharePointServiceMock = new Mock<ISharePointService>();
		var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
		var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
		var conversionAppServiceMock = new Mock<IConversionApplicationService>();

		var pageModel = SetupNextFinancialYearModel(
			sharePointServiceMock.Object,
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
		var mockSharePointService = new Mock<ISharePointService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(applicationId))
			.ReturnsAsync(conversionApplication);
		// act
		var pageModel = SetupNextFinancialYearModel(mockSharePointService.Object, mockConversionApplicationCreationService.Object,
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

		var pageModel = SetupNextFinancialYearModel(
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
		Assert.That(redirect.PageName, Is.EqualTo("NextFinancialYear"));
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

		var pageModel = SetupNextFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var exception = Assert.ThrowsAsync<Exception>(
			() => pageModel.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName));

		Assert.That(exception!.Message, Is.EqualTo("SharePoint delete failed"));
	}

	[Test]
	public async Task OnGetAsync_WhenApplicationNotFound_ThrowsNullReferenceException()
	{
		const int appId = 10;
		const int urn = 200;

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync((Dfe.Academies.External.Web.Dtos.ConversionApplication?)null);

		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, new Dfe.Academies.External.Web.Dtos.ConversionApplication());

		// The code tries to access ApplicationReference on a null applicationDetails object
		Assert.ThrowsAsync<NullReferenceException>(async () => await pageModel.OnGetAsync(urn, appId));
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

		var pageModel = SetupNextFinancialYearModel(
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

	[Test]
	public void RunUiValidation_WhenModelStateInvalid_ReturnsFalse()
	{
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ModelState.AddModelError("Revenue", "Revenue is required");

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
	}

	[Test]
	public void RunUiValidation_WhenNFYFinancialEndDateIsMinValue_AddsModelError()
	{
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.NFYFinancialEndDateLocal = DateTime.MinValue;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("NFYFinancialEndDateNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenNFYRevenueDeficitWithoutExplanationOrFiles_AddsModelError()
	{
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.NFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.NFYRevenueStatusExplained = "";
		pageModel.ForecastedRevenueFiles = new List<IFormFile>();
		pageModel.ForecastedRevenueFileNames = new List<string>();
		pageModel.NFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("NFYRevenueStatusExplainedNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenNFYRevenueDeficitWithExplanation_ReturnsTrue()
	{
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.NFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.NFYRevenueStatusExplained = "Some explanation";
		pageModel.ForecastedRevenueFiles = new List<IFormFile>();
		pageModel.ForecastedRevenueFileNames = new List<string>();
		pageModel.NFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void RunUiValidation_WhenNFYRevenueDeficitWithFiles_ReturnsTrue()
	{
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.FileName).Returns("revenue.pdf");

		pageModel.NFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.NFYRevenueStatusExplained = "";
		pageModel.ForecastedRevenueFiles = new List<IFormFile> { fileMock.Object };
		pageModel.ForecastedRevenueFileNames = new List<string>();
		pageModel.NFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public async Task OnPostAsync_WhenValidationFails_ReturnsPageWithErrorState()
	{
		var entityId = Guid.NewGuid();
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<Dfe.Academies.External.Web.Dtos.SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { EntityId = entityId }
		};

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(It.IsAny<int>())).ReturnsAsync(application);

		var formMock = new Mock<IFormCollection>();
		formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		pageModel.EntityId = entityId;
		pageModel.NFYFinancialEndDateLocal = DateTime.MinValue; // This will cause validation to fail
		pageModel.Request.Form = formMock.Object;
		pageModel.ForecastedRevenueFileNames = new List<string>();
		pageModel.ForecastedCapitalFileNames = new List<string>();

		var result = await pageModel.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ModelState.ContainsKey("NFYFinancialEndDateNotEntered"), Is.True);
	}

	[Test]
	public async Task OnPostAsync_WhenUploadFilesFails_ReturnsPageWithError()
	{
		var entityId = Guid.NewGuid();
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<Dfe.Academies.External.Web.Dtos.SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { EntityId = entityId }
		};

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(It.IsAny<int>())).ReturnsAsync(application);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock.Setup(x => x.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
			.ThrowsAsync(new Dfe.Academies.External.Web.Exceptions.FileUploadException("Upload failed"));

		var formMock = new Mock<IFormCollection>();
		formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.FileName).Returns("revenue.pdf");
		fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

		var pageModel = SetupNextFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		SetupValidNextFinancialYearModel(pageModel, entityId);
		pageModel.ForecastedRevenueFiles = new List<IFormFile> { fileMock.Object };

		var result = await pageModel.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ModelState.ContainsKey(nameof(pageModel.SchoolRevenueFileGenericError)), Is.True);
	}

	[Test]
	public async Task OnPostAsync_WhenValid_SucceedsAndRedirects()
	{
		var entityId = Guid.NewGuid();
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<Dfe.Academies.External.Web.Dtos.SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { EntityId = entityId }
		};

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(It.IsAny<int>())).ReturnsAsync(application);

		var sharePointMock = new Mock<ISharePointService>();
		sharePointMock.Setup(x => x.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
			.Returns(Task.CompletedTask);

		var conversionAppServiceMock = new Mock<IConversionApplicationService>();
		conversionAppServiceMock.Setup(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()))
			.Returns(Task.CompletedTask);

		var pageModel = SetupNextFinancialYearModel(
			sharePointMock.Object,
			conversionAppServiceMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		SetupValidNextFinancialYearModel(pageModel, entityId);

		var result = await pageModel.OnPostAsync();


		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
	}

	private static void SetupValidNextFinancialYearModel(NextFinancialYearModel pageModel, Guid entityId)
	{
		var formMock = new Mock<IFormCollection>();
		// Setup proper date form values
		formMock.Setup(x => x.TryGetValue("sip_nfyenddate-day", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("31");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_nfyenddate-month", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("03");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_nfyenddate-year", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("2025");
				return true;
			});
		// Let other form values return false by default

		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		pageModel.EntityId = entityId;
		pageModel.ApplicationReference = "APP-REF";
		pageModel.NFYEndDateFormInputName = "sip_nfyenddate";
		pageModel.Request.Form = formMock.Object;
		pageModel.Revenue = 100000m;
		pageModel.NFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Surplus;
		pageModel.CapitalCarryForward = 50000m;
		pageModel.NFYCapitalCarryForwardStatus = Dfe.Academies.External.Web.Enums.RevenueType.Surplus;
		pageModel.ForecastedRevenueFiles = new List<IFormFile>();
		pageModel.ForecastedCapitalFiles = new List<IFormFile>();
		pageModel.ForecastedRevenueFileNames = new List<string>();
		pageModel.ForecastedCapitalFileNames = new List<string>();
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, new Dfe.Academies.External.Web.Dtos.ConversionApplication());
	}


	private static NextFinancialYearModel SetupNextFinancialYearModel(
		ISharePointService mockSharePointService,
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new NextFinancialYearModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService, mockSharePointService,
			Mock.Of<ILogger<NextFinancialYearModel>>())
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNextFinancialYearData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var endDate = new DateTime(2025, 7, 31);
		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			NextFinancialYear = new SchoolFinancialYear(
				FinancialYearEndDate: endDate,
				Revenue: 500000.50m,
				RevenueStatus: RevenueType.Surplus,
				RevenueStatusExplained: "Revenue explanation",
				CapitalCarryForward: 75000.25m,
				CapitalCarryForwardStatus: RevenueType.Surplus,
				CapitalCarryForwardExplained: "Capital explanation"
			)
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.NFYEndDate, Is.EqualTo("31/07/2025"));
		Assert.That(pageModel.Revenue, Is.EqualTo(500000.50m));
		Assert.That(pageModel.NFYRevenueStatus, Is.EqualTo(RevenueType.Surplus));
		Assert.That(pageModel.NFYRevenueStatusExplained, Is.EqualTo("Revenue explanation"));
		Assert.That(pageModel.CapitalCarryForward, Is.EqualTo(75000.25m));
		Assert.That(pageModel.NFYCapitalCarryForwardStatus, Is.EqualTo(RevenueType.Surplus));
		Assert.That(pageModel.NFYCapitalCarryForwardExplained, Is.EqualTo("Capital explanation"));
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoNextFinancialYearData_SetsDefaults()
	{
		// Arrange
		var pageModel = SetupNextFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			NextFinancialYear = new SchoolFinancialYear()
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.NFYEndDate, Is.EqualTo(string.Empty));
		Assert.That(pageModel.Revenue, Is.EqualTo(0m));
		Assert.That(pageModel.NFYRevenueStatus, Is.EqualTo((RevenueType)0)); // Default enum value
		Assert.That(pageModel.NFYRevenueStatusExplained, Is.Null);
		Assert.That(pageModel.CapitalCarryForward, Is.EqualTo(0m));
		Assert.That(pageModel.NFYCapitalCarryForwardStatus, Is.EqualTo((RevenueType)0)); // Default enum value
		Assert.That(pageModel.NFYCapitalCarryForwardExplained, Is.Null);
	}
}
