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
	internal sealed class AdditionalDetailsTests
    {
		[Test]
		public void RunUiValidation_ResolutionConsentFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("ResolutionConsentFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_FoundationConsentFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { };
			model.FoundationConsentFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("FoundationConsentFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_DioceseFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { };
			model.FoundationConsentFiles = new List<IFormFile> { };
			model.DioceseFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("DioceseFileSizeError"), Is.True);
		}

		private static AdditionalDetails SetupAdditionalDetails(
			IConversionApplicationRetrievalService conversionAppRetrievalServiceMock,
			IReferenceDataRetrievalService referenceDataRetrievalServiceMock,
			IConversionApplicationService conversionAppServiceMock,
			IFileUploadService fileUploadServiceMock,
			bool isAuthenticated = false
		)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(false);

			var model = new AdditionalDetails(
				fileUploadServiceMock,
				conversionAppRetrievalServiceMock,
				referenceDataRetrievalServiceMock,
				conversionAppServiceMock
				
			)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};

			return model;
		}
	}
}
