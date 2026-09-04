using System;
using System.Collections.Generic;
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

	[Test]
	public void RunUiValidation_WhenModelStateInvalid_ReturnsFalse()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ModelState.AddModelError("Revenue", "Revenue is required");

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
	}

	[Test]
	public void RunUiValidation_WhenPFYFinancialEndDateIsMinValue_AddsModelError()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.PFYFinancialEndDateLocal = DateTime.MinValue;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("PFYFinancialEndDateNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenPFYRevenueDeficitWithoutExplanationOrFiles_AddsModelError()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.PFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.PFYRevenueStatusExplained = "";
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile>();
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
		pageModel.PFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("PFYRevenueStatusExplainedNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenPFYRevenueDeficitWithExplanation_ReturnsTrue()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.PFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.PFYRevenueStatusExplained = "Some explanation";
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile>();
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
		pageModel.PFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void RunUiValidation_WhenPFYRevenueDeficitWithFiles_ReturnsTrue()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.FileName).Returns("revenue.pdf");

		pageModel.PFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.PFYRevenueStatusExplained = "";
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile> { fileMock.Object };
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
		pageModel.PFYFinancialEndDateLocal = DateTime.Now;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void RunUiValidation_WhenPFYRevenueDeficitWithFileNames_ReturnsTrue()
	{
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.PFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		pageModel.PFYRevenueStatusExplained = "";
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile>();
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string> { "existing_file.pdf" };
		pageModel.PFYFinancialEndDateLocal = DateTime.Now;
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

		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		pageModel.EntityId = entityId;
		pageModel.PFYFinancialEndDateLocal = DateTime.MinValue; // This will cause validation to fail
		pageModel.Request.Form = formMock.Object;
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
		pageModel.SchoolPFYCapitalForwardStatusFileNames = new List<string>();

		var result = await pageModel.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ModelState.ContainsKey("PFYFinancialEndDateNotEntered"), Is.True);
	}

	[Test]
	public async Task OnPostAsync_WhenRevenueFileUploadFails_ReturnsPageWithError()
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
		sharePointMock.Setup(x => x.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.Stream>()))
			.ThrowsAsync(new Dfe.Academies.External.Web.Exceptions.FileUploadException("Upload failed"));

		var formMock = new Mock<IFormCollection>();
		formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.FileName).Returns("revenue.pdf");
		fileMock.Setup(f => f.OpenReadStream()).Returns(new System.IO.MemoryStream());

		var pageModel = SetupPreviousFinancialYearModel(
			sharePointMock.Object,
			Mock.Of<IConversionApplicationService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		SetupValidPreviousFinancialYearModel(pageModel, entityId);
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile> { fileMock.Object };

		var result = await pageModel.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.ModelState.ContainsKey(nameof(pageModel.SchoolPFYRevenueFileGenericError)), Is.True);
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
		sharePointMock.Setup(x => x.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.Stream>()))
			.Returns(Task.CompletedTask);

		var conversionAppServiceMock = new Mock<IConversionApplicationService>();
		conversionAppServiceMock.Setup(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()))
			.Returns(Task.CompletedTask);

		var formMock = new Mock<IFormCollection>();
		formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

		var pageModel = SetupPreviousFinancialYearModel(
			sharePointMock.Object,
			conversionAppServiceMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>());

		SetupValidPreviousFinancialYearModel(pageModel, entityId);

		var result = await pageModel.OnPostAsync();

		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
	}

	private static void SetupValidPreviousFinancialYearModel(PreviousFinancialYearModel pageModel, Guid entityId)
	{
		var formMock = new Mock<IFormCollection>();
		// Setup proper date form values
		formMock.Setup(x => x.TryGetValue("sip_pfyenddate-day", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("31");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_pfyenddate-month", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("03");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_pfyenddate-year", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("2024");
				return true;
			});
		// Let other form values return false by default

		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		pageModel.EntityId = entityId;
		pageModel.ApplicationReference = "APP-REF";
		pageModel.PFYEndDateFormInputName = "sip_pfyenddate";
		pageModel.Request.Form = formMock.Object;
		pageModel.Revenue = 100000m;
		pageModel.PFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Surplus;
		pageModel.CapitalCarryForward = 50000m;
		pageModel.SchoolPFYRevenueStatusFiles = new List<IFormFile>();
		pageModel.SchoolPFYCapitalForwardStatusFiles = new List<IFormFile>();
		pageModel.SchoolPFYRevenueStatusFileNames = new List<string>();
		pageModel.SchoolPFYCapitalForwardStatusFileNames = new List<string>();
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, new Dfe.Academies.External.Web.Dtos.ConversionApplication());
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

	[Test]
	public void PopulateUiModel_WhenSchoolHasPreviousFinancialYearData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var endDate = new DateTime(2024, 7, 31);
		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			PreviousFinancialYear = new SchoolFinancialYear(
				FinancialYearEndDate: endDate,
				Revenue: 450000.75m,
				RevenueStatus: RevenueType.Deficit,
				RevenueStatusExplained: "Previous revenue explanation",
				CapitalCarryForward: 25000.50m,
				CapitalCarryForwardStatus: RevenueType.Deficit,
				CapitalCarryForwardExplained: "Previous capital explanation"
			)
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.PFYEndDate, Is.EqualTo("31/07/2024"));
		Assert.That(pageModel.Revenue, Is.EqualTo(450000.75m));
		Assert.That(pageModel.PFYRevenueStatus, Is.EqualTo(RevenueType.Deficit));
		Assert.That(pageModel.PFYRevenueStatusExplained, Is.EqualTo("Previous revenue explanation"));
		Assert.That(pageModel.CapitalCarryForward, Is.EqualTo(25000.50m));
		Assert.That(pageModel.PFYCapitalCarryForwardStatus, Is.EqualTo(RevenueType.Deficit));
		Assert.That(pageModel.PFYCapitalCarryForwardExplained, Is.EqualTo("Previous capital explanation"));
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoPreviousFinancialYearData_SetsDefaults()
	{
		// Arrange
		var pageModel = SetupPreviousFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			PreviousFinancialYear = new SchoolFinancialYear()
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.PFYEndDate, Is.EqualTo(string.Empty));
		Assert.That(pageModel.Revenue, Is.EqualTo(0m));
		Assert.That(pageModel.PFYRevenueStatus, Is.EqualTo((RevenueType)0)); // Default enum value
		Assert.That(pageModel.PFYRevenueStatusExplained, Is.Null);
		Assert.That(pageModel.CapitalCarryForward, Is.EqualTo(0m));
		Assert.That(pageModel.PFYCapitalCarryForwardStatus, Is.EqualTo((RevenueType)0)); // Default enum value
		Assert.That(pageModel.PFYCapitalCarryForwardExplained, Is.Null);
	}
}
