using System.Collections.Generic;
using Dfe.Academies.External.Web.Enums;
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

		[Test]
		public void RunUiValidation_OfstedInspectedYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.OfstedInspected = SelectOption.Yes;
			model.OfstedInspectionDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("OfstedInspectionDetailsNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_LocalAuthorityReorganisationYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.LocalAuthorityReorganisation = SelectOption.Yes;
			model.LocalAuthorityReorganisationDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("LocalAuthorityReorganisationDetailsNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_LinkedToDioceseYesAndDioceseNameEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.LinkedToDiocese = SelectOption.Yes;
			model.DioceseName = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("DioceseNameNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_SupportedByFoundationYesAndNameEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.SupportedByFoundationTrustOrBody = SelectOption.Yes;
			model.FoundationTrustOrBodyName = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("FoundationTrustOrBodyNameNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_ExemptionFromSACREYesAndNoDate_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ExemptionFromSACRE = SelectOption.Yes;
			model.ExemptionEndDate = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("exemptionFromSACREEndDateNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_EqualityAssessmentYesAndNoCharacteristicsSelected_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.EqualityAssessment = SelectOption.Yes;
			model.DisproportionateProtectedCharacteristics = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("equalitiesImpactAssessmentOptionNoOptionSelected"), Is.True);
		}

		[Test]
		public void RunUiValidation_FurtherInformationYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.FurtherInformation = SelectOption.Yes;
			model.FurtherInformationDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("furtherInformationDetailsNotAdded"), Is.True);
		}

		[Test]
		public void HasError_WhenTrustBenefitDetailsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("TrustBenefitDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.TrustBenefitDetailsError, Is.True);
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
