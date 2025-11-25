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
using NSubstitute;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class PreviousFinancialYearModelTests
{
	[Test]
    public void RunUiValidation_FileTooLargeInSchoolPFYRevenueStatusFiles_ReturnsError()
    {
        // Arrange
        var mockFileUploadService = new Mock<IFileUploadService>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        var pageModel = SetupPreviousFinancialYearModel(mockFileUploadService.Object, mockConversionApplicationCreationService.Object,
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
		var mockFileUploadService = new Mock<IFileUploadService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		var pageModel = SetupPreviousFinancialYearModel(mockFileUploadService.Object, mockConversionApplicationCreationService.Object,
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
		var mockFileUploadService = new Mock<IFileUploadService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupPreviousFinancialYearModel(mockFileUploadService.Object, mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static PreviousFinancialYearModel SetupPreviousFinancialYearModel(
		IFileUploadService mockFileUploadService,
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new PreviousFinancialYearModel(mockFileUploadService, mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
