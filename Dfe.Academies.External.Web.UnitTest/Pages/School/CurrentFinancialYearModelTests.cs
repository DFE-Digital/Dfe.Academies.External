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
using Microsoft.Extensions.Primitives;
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

	[Test]
	public void RunUiValidation_WhenModelStateInvalid_ReturnsFalse()
	{
		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		model.ModelState.AddModelError("Revenue", "Revenue is required");

		var isValid = model.RunUiValidation();

		Assert.That(isValid, Is.False);
	}

	[Test]
	public void RunUiValidation_WhenCFYFinancialEndDateIsMinValue_AddsModelError()
	{
		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		model.CFYFinancialEndDateLocal = DateTime.MinValue;
		model.ModelState.Clear();

		var isValid = model.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(model.ModelState.ContainsKey("CFYFinancialEndDateNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenCFYRevenueDeficitWithoutExplanationOrFiles_AddsModelError()
	{
		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		model.CFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		model.CFYRevenueStatusExplained = "";
		model.SchoolCfyRevenueStatusFiles = new List<IFormFile>();
		model.SchoolCFYRevenueStatusFileNames = new List<string>();
		model.CFYFinancialEndDateLocal = DateTime.Now;
		model.ModelState.Clear();

		var isValid = model.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(model.ModelState.ContainsKey("CFYRevenueStatusExplainedNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_WhenCFYCapitalDeficitWithoutExplanationOrFiles_AddsModelError()
	{
		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		model.CFYCapitalCarryForwardStatus = Dfe.Academies.External.Web.Enums.RevenueType.Deficit;
		model.CFYCapitalCarryForwardExplained = "";
		model.SchoolCFYCapitalForwardFiles = new List<IFormFile>();
		model.SchoolCFYCapitalForwardFileNames = new List<string>();
		model.CFYFinancialEndDateLocal = DateTime.Now;
		model.ModelState.Clear();

		var isValid = model.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(model.ModelState.ContainsKey("PFYCapitalCarryForwardExplainedNotEntered"), Is.True);
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

		var model = SetupCurrentFinancialYearModel(
			Mock.Of<ISharePointService>(),
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		model.ApplicationId = 1;
		model.Urn = 100;
		model.EntityId = entityId;
		model.CFYFinancialEndDateLocal = DateTime.MinValue; // This will cause validation to fail
		model.Request.Form = formMock.Object;
		model.SchoolCFYRevenueStatusFileNames = new List<string>();
		model.SchoolCFYCapitalForwardFileNames = new List<string>();

		var result = await model.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(model.ModelState.ContainsKey("CFYFinancialEndDateNotEntered"), Is.True);
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

		var conversionAppServiceMock = new Mock<IConversionApplicationService>();
		conversionAppServiceMock.Setup(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()))
			.Returns(Task.CompletedTask);

		var formMock = new Mock<IFormCollection>();
		formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

		var fileMock = new Mock<IFormFile>();
		fileMock.Setup(f => f.FileName).Returns("revenue.pdf");
		fileMock.Setup(f => f.OpenReadStream()).Returns(new System.IO.MemoryStream());

		var model = SetupCurrentFinancialYearModel(
			sharePointMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			conversionAppServiceMock.Object);

		SetupValidCurrentFinancialYearModel(model, entityId);
		model.SchoolCfyRevenueStatusFiles = new List<IFormFile> { fileMock.Object };

		var result = await model.OnPostAsync();

		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(model.ModelState.ContainsKey(nameof(model.SchoolCFYRevenueFileGenericError)), Is.True);
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

		var model = SetupCurrentFinancialYearModel(
			sharePointMock.Object,
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			conversionAppServiceMock.Object);

		SetupValidCurrentFinancialYearModel(model, entityId);

		var result = await model.OnPostAsync();

		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
	}

	private static void SetupValidCurrentFinancialYearModel(CurrentFinancialYearModel model, Guid entityId)
	{
		var formMock = new Mock<IFormCollection>();
		// Setup proper date form values
		formMock.Setup(x => x.TryGetValue("sip_cfyenddate-day", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("31");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_cfyenddate-month", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("03");
				return true;
			});
		formMock.Setup(x => x.TryGetValue("sip_cfyenddate-year", out It.Ref<StringValues>.IsAny!))
			.Returns((string key, out StringValues values) => {
				values = new StringValues("2025");
				return true;
			});

		model.ApplicationId = 1;
		model.Urn = 100;
		model.EntityId = entityId;
		model.ApplicationReference = "APP-REF";
		model.CFYEndDateFormInputName = "sip_cfyenddate";
		model.Request.Form = formMock.Object;
		model.Revenue = 100000m;
		model.CFYRevenueStatus = Dfe.Academies.External.Web.Enums.RevenueType.Surplus;
		model.CapitalCarryForward = 50000m;
		model.CFYCapitalCarryForwardStatus = Dfe.Academies.External.Web.Enums.RevenueType.Surplus;
		model.SchoolCfyRevenueStatusFiles = new List<IFormFile>();
		model.SchoolCFYCapitalForwardFiles = new List<IFormFile>();
		model.SchoolCFYRevenueStatusFileNames = new List<string>();
		model.SchoolCFYCapitalForwardFileNames = new List<string>();
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new Dfe.Academies.External.Web.Dtos.ConversionApplication());
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
