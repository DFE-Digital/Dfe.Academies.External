using System;
using System.Collections.Generic;
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

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class CurrentFinancialYearModelTests
	{
		[Test]
		public void RunUiValidation_ForecastedRevenueFileTooLarge_AddsModelError()
		{
			// Arrange
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupCurrentFinancialYearModel(
				fileUploadServiceMock.Object,
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
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupCurrentFinancialYearModel(
				fileUploadServiceMock.Object,
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

		private static CurrentFinancialYearModel SetupCurrentFinancialYearModel(
			IFileUploadService mockFileUploadService,
			IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationCreationService,
			bool isAuthenticated = false
		)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new CurrentFinancialYearModel(
				mockFileUploadService,
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
